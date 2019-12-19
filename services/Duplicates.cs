using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using TPDMS.RestApi.Extensions;

namespace TPDMS.RestApi.services
{
    public static class Duplicates
    {
        public static bool Handler(PropertyInfo aUIs, PropertyInfo upUIs, object aUIsValue, object upUIsValue, object entryObject)
        {
            var isDuplicatedValues = false;
            var typeaUIs = aUIsValue?.GetType().GetGenericArguments()[0];
            var typeupUIs = upUIsValue?.GetType().GetGenericArguments()[0];
            var enumerableaUIs = (IEnumerable<dynamic>)aUIsValue;
            var enumerableupUIs = (IEnumerable<dynamic>)upUIsValue;
            var checkaUIs = enumerableaUIs?.Select(c => c.GetType().GetProperty("specific property").GetValue(c, null));
            var checkupUIs = enumerableupUIs?.Select(c => c.GetType().GetProperty("another specific property").GetValue(c, null));
            if (checkaUIs != null)
            {
                if (!checkaUIs.ContainsDuplicates())
                {
                    dynamic distinctaUIs;
                    var auiKeys = ((IEnumerable<dynamic>)aUIsValue)?.GroupBy(c => c.GetType().GetProperty("specific property").GetValue(c, null))
                                                                    .Where(g => g.Count() > 1)
                                                                    .SelectMany(x => x.AsEnumerable().DistinctBy(p => p.GetType().GetProperty("specific property").GetValue(p, null)));
                    distinctaUIs = enumerableaUIs?.Except(auiKeys).ToList();
                    entryObject.GetType().GetProperty("specific property").SetValue(entryObject, null, null);
                    var IListRef = typeof(List<>);
                    Type[] IListParam = { typeaUIs ?? typeof(object) };
                    aUIsValue = Activator.CreateInstance(IListRef.MakeGenericType(IListParam));
                    foreach (var distinctaUI in distinctaUIs)
                    {
                        aUIsValue.GetType().GetMethod("Add").Invoke(aUIsValue, new[] { distinctaUI });
                    }
                    aUIs.SetValue(entryObject, aUIsValue);
                    isDuplicatedValues = true;
                }
            }

            if (checkupUIs != null)
            {
                if (!checkupUIs.ContainsDuplicates())
                {
                    dynamic distinctupUIs;
                    var upUIKeys = ((IEnumerable<dynamic>)upUIsValue)?.GroupBy(c => c.GetType().GetProperty("another specific property").GetValue(c, null))
                                                                      .Where(g => g.Count() > 1)
                                                                      .SelectMany(x => x.AsEnumerable().DistinctBy(p => p.GetType().GetProperty("another specific property").GetValue(p, null)));
                    distinctupUIs = enumerableupUIs?.Except(upUIKeys).ToList();
                    entryObject.GetType().GetProperty("another specific property").SetValue(entryObject, null, null);
                    var IListRef = typeof(List<>);
                    Type[] IListParam = { typeupUIs ?? typeof(object) };
                    upUIsValue = Activator.CreateInstance(IListRef.MakeGenericType(IListParam));
                    foreach (var distinctupUI in distinctupUIs)
                    {
                        upUIsValue.GetType().GetMethod("Add").Invoke(upUIsValue, new[] { distinctupUI });
                    }
                    upUIs.SetValue(entryObject, upUIsValue);
                    isDuplicatedValues = true;
                }
            }
            return isDuplicatedValues;
        }
    }
}