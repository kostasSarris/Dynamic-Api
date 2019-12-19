using System;

namespace TPDMS.RestApi.CustomExceptions
{
    public class MandatoryArgumentException : Exception
    {
        public MandatoryArgumentException(string fieldName, string fieldValue, string errorDescription = "Your input data is not correct")
            : base(string.Format("Error: {2} ", fieldName, fieldValue, errorDescription))
        {
            ParameterName = fieldName;
            ParameterValue = fieldValue;
            ParameterErrorDescription = errorDescription;
        }

        public string ParameterName { get; set; }

        public string ParameterValue { get; set; }

        public string ParameterErrorDescription { get; set; }

        public override string ToString()
        {
            return $"Value: {ParameterValue} for parameter: {ParameterName} is not valid \n Description: {ParameterErrorDescription}" + Environment.NewLine + base.ToString();
        }
    }
}