using Newtonsoft.Json.Linq;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TPDMS.BusinessLayer;
using TPDMS.DataLayer;
using TPDMS.RestApi.CustomExceptions;
using TPDMS.RestApi.Models;
using AppDomain = System.AppDomain;

namespace TPDMS.RestApi.services
{
    public static class EventGenerator
    {
        public static object SetObject(JObject entity, string entityName = null, Error error = null)
        {
            using (var dbContext = new TPDMSDbContext(WebApiConfig.Options))
            {
                var mapping = dbContext.Mappings.FirstOrDefault(x => x.Name == $"json{entityName}");
                var mappingField = dbContext.MappingFields.Where(x => x.MappingId == mapping.MappingId);
                var className = entityName;
                var assembly = AppDomain.CurrentDomain.GetAssemblies()
                                        .FirstOrDefault(t => t.GetName().Name == $"DataLayer");
                var type = assembly.GetType(className);
                var instance = Activator.CreateInstance(type);

                foreach (var field in mappingField)
                {
                    DataType dataType = null;
                    var childField = dbContext.EntityFields.Single(y => y.EntityFieldId.ToString() == field.SourceMapping);
                    dataType = dbContext.DataTypes.Single(y => y.DataTypeID == childField.DataTypeId);
                    dataType = dataType.BaseDataTypeId != null ? dbContext.DataTypes.Single(y => y.DataTypeID == dataType.BaseDataTypeId) : dataType;
                    var name = childField.Name;
                    switch (childField.Cardinality)
                    {
                        case "statement 1":
                            {
                                var prpName = field.TargetMapping.Remove(0, 2);
                                var typePerField = assembly.GetType($"DataLayer.{prpName}");
                                entity.TryGetValue(name, out var result);
                                if (result == null || result.ToString() == string.Empty)
                                {
                                    continue;
                                }
                                Type listType = null;
                                object instanceList = null;

                                foreach (var item in result?.Values())
                                {
                                    var stringItem = item?.ToString();
                                    if (childField.MaxLength < stringItem?.Length)
                                    {
                                        try
                                        {
                                            throw new FieldSizeViolationException(childField.Name, childField.MaxLength, true);
                                        }
                                        catch (FieldSizeViolationException ex)
                                        {
                                            error.HasWarning = true;
                                            error.WarningMessages = error.WarningMessages ?? new Dictionary<string, string>();
                                            error.WarningMessages.Add($"{ex?.GetType()?.ToString()}_{childField?.Name}", ex.Message);
                                        }
                                    }
                                    var instanceChild = Activator.CreateInstance(typePerField);
                                    var property = typePerField.GetProperty(name);
                                    var checkDataType = Helper.ConvertJToken(item, dataType, property.Name);
                                    if (checkDataType is string && (string)checkDataType == string.Empty)
                                    {
                                        property?.SetValue(instanceChild, null);
                                    }

                                    property.SetValue(instanceChild, checkDataType);
                                    var enumerables = Helper.GetCollections(type);
                                    foreach (var prp in enumerables.Where(p => p.PropertyType.GetGenericArguments().First() == typePerField))
                                    {
                                        listType = listType ?? prp.PropertyType.GetGenericArguments().First();
                                        instanceList = instanceList ?? Activator.CreateInstance(typeof(List<>).MakeGenericType(listType));
                                        instanceList.GetType().GetMethod("Add").Invoke(instanceList, new[] { instanceChild });
                                        prp?.SetValue(instance, instanceList, null);
                                        break;
                                    }
                                }

                                break;
                            }

                        case "Statement 2":
                            {
                                entity.TryGetValue(name, out var result);
                                var prpName = field.TargetMapping.Remove(0, 2);

                                if (result == null || result.ToString() == string.Empty)
                                {
                                    continue;
                                }
                                var stringResult = result?.ToString();
                                if (childField.MaxLength < stringResult?.Length)
                                {
                                    try
                                    {
                                        throw new FieldSizeViolationException(childField.Name, childField.MaxLength, true);
                                    }
                                    catch (FieldSizeViolationException ex)
                                    {
                                        error.HasWarning = true;
                                        error.WarningMessages = error.WarningMessages ?? new Dictionary<string, string>();
                                        error.WarningMessages.Add($"{ex?.GetType()?.ToString()}_{childField?.Name}", ex.Message);
                                    }
                                }
                                var checkDataTypeResult = Helper.ConvertJToken(result, dataType, prpName);
                                var property = instance.GetType().GetProperties().FirstOrDefault(p => p.Name == prpName);

                                if (checkDataTypeResult is string && (string)checkDataTypeResult == string.Empty)
                                {
                                    property?.SetValue(instance, null);
                                }
                                else
                                {
                                    property?.SetValue(instance, checkDataTypeResult);
                                }

                                break;
                            }
                    }
                }
                return instance;
            }
        }
    }
}