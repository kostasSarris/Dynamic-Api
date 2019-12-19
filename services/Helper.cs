using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;
using SI.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using TPDMS.DataLayer;
using TPDMS.RestApi.CustomExceptions;
using TPDMS.RestApi.Extensions;

namespace TPDMS.RestApi.services
{
    public static class Helper
    {
        public static void ValidateMaxStringSize(string text, string fieldName, int? maxLength)
        {
            if (text?.Length > maxLength)
            {
                throw new FieldSizeViolationException(fieldName, maxLength, true);
            }
        }

        public static void ValidateMinStringSize(string text, string fieldName, int minLength)
        {
            if (text?.Length < minLength)
            {
                throw new FieldSizeViolationException(fieldName, minLength, false);
            }
        }

        public static void ValidateStringSize(string text, string fieldName, int minLength, int? maxLength)
        {
            ValidateMaxStringSize(text, fieldName, maxLength);
            ValidateMinStringSize(text, fieldName, minLength);
        }

        public static object ConvertJToken(JToken result, DataLayer.DataType type, string name)
        {
            switch (type.Name)
            {
                case "System.String":
                    var resultForCheck = result?.ToString();
                    var resultForString = resultForCheck.ValidateString() ? result?.ToString()
                        : throw new WrongTypeException($"The {name} has not a valid type of Data.You should put an appropriate String type. ");
                    return resultForString;

                case "System.Boolean":
                    bool resultForBoolean;
                    try
                    {
                        resultForBoolean = result.ToString().ToBoolean();
                    }
                    catch (Exception ex)
                    {
                        throw new WrongTypeException($"The {name} has not not a valid type of Data.You should put Boolean type. Exception: {ex.Message}");
                    }
                    return resultForBoolean;

                case "System.Decimal":
                    var resultForDecimal = decimal.TryParse(result?.ToString(), out decimal valueforDecimal) ? result?.ToDecimal()
                        : throw new WrongTypeException($"The {name} has not not a valid type of Data.You should put Int16 type. ");
                    return resultForDecimal;

                case "System.Int16":
                    var resultForInt16 = short.TryParse(result?.ToString(), out short valueforInt16) ? result?.ToInt16()
                        : throw new WrongTypeException($"The {name} has not not a valid type of Data.You should put Int16 type. ");
                    return resultForInt16;

                case "System.Int32":
                    var resultForInt32 = int.TryParse(result?.ToString(), out int valueforInt32) ? result?.ToInt32()
                        : throw new WrongTypeException($"The {name} has not not a valid type of Data.You should put Int32 type. ");
                    return resultForInt32;

                case "System.Int64":
                    var resultForInt64 = long.TryParse(result?.ToString(), out long valueforInt64) ? result?.ToInt64()
                        : throw new WrongTypeException($"The {name} has not not a valid type of Data.You should put Int64 type. ");
                    return resultForInt64;

                case "System.DateTime":
                    var resultForDateTime = DateTime.TryParse(result?.ToString(), out DateTime valueForDate) | valueForDate == default ? (DateTime?)result :
                        throw new WrongTypeException($"The {name} is not a valid type of Data.You should put datetime type. ");
                    return resultForDateTime;
            }
            return null;
        }

        public static IList<PropertyInfo> GetCollections(Type type)
        {
            IList<PropertyInfo> prps = new List<PropertyInfo>();

            foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                if (prop.PropertyType.GetTypeInfo().IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(IEnumerable<>))
                {
                    prps.Add(prop);
                }
            }
            return prps;
        }
    }
}