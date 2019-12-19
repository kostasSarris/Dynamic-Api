using System;

namespace TPDMS.RestApi.CustomExceptions
{
    public class JsonSchemaException : Exception
    {
        public JsonSchemaException(string jsonError)
            : base(string.Format("Error: {0} ", jsonError))
        {
            json = jsonError;
        }

        public string json { get; set; }

        public override string ToString()
        {
            return $"{json}" + base.ToString();
        }
    }
}