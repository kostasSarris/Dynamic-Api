using System;

namespace TPDMS.RestApi.CustomExceptions
{
    public class UnsupportedOperationException : Exception
    {
        public UnsupportedOperationException(string description)
        {
            Description = description;
        }

        public string Description { get; private set; }

        public override string ToString()
        {
            return "Operation requested is not supported. " + Environment.NewLine + $"friendly error: {Description}";
        }
    }
}