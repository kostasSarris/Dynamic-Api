using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TPDMS.DataLayer;
using TPDMS.RestApi.Models;


namespace TPDMS.RestApi.services
{
    public class Mapping : IMapping
    {
        public void DeclareObject(JObject entity, string user, string entityName = null, Error error = null)
        {
            var entryObject = EventGenerator.SetObject(entity, entityName, error);

            using (var dbContext = new TPDMSDbContext(WebApiConfig.Options))
            {
                {
                    // get type of object from json
                    var currentType = entryObject.GetType();
                    var props = new List<PropertyInfo>(currentType.GetProperties());
                    // find the properties you need to use
                    var propert1 = props.FirstOrDefault(x => x.Name == "Propert1");
                    var property2 = props.FirstOrDefault(x => x.Name == "Propert2");
                    var property3 = props.FirstOrDefault(x => x.Name == "Propert3");
                    var property4 = props.FirstOrDefault(x => x.Name == "Propert4");
                    var property5 = props.FirstOrDefault(x => x.Name == "Propert5");
                    var property6 = props.FirstOrDefault(x => x.Name == "Propert6");

                    var propert7 = props.FirstOrDefault(x => x.Name == "Propert7");
                    var property8 = props.FirstOrDefault(x => x.Name == "Propert8");
                    var propert7Value = propert7?.GetValue(propert7.GetGetMethod().IsStatic ? null : entryObject);
                    var property8Value = property8?.GetValue(property8.GetGetMethod().IsStatic ? null : entryObject);

                    // Handle duplicates for IEnumerable<object> with reflection
                    if (Duplicates.Handler(property8, propert7, aUIsValue, upUIsValue, entryObject))
                    {
                        propert7Value = property8?.GetValue(property8.GetGetMethod().IsStatic ? null : entryObject);
                        property8Value = propert7?.GetValue(propert7.GetGetMethod().IsStatic ? null : entryObject);
                    }

                    #region some business for specific entity

                    #endregion some business for specific entity

                    #region some business for specific entity

                    #endregion some business for specific entity

                    #region some business for specific entity

                    #endregion some business for specific entity

                    #region some business for specific entity

                    #endregion some business for specific entity
                }
            }
        }
    }
}
