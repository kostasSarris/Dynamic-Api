using System;

namespace TPDMS.RestApi.CustomExceptions
{
    public class WrongTypeException : Exception
    {
        public WrongTypeException(string entityDescritpion)
            : base(string.Format("Error: {0} ", entityDescritpion))
        {
            entityDescritpion = entitydescritpion;
        }

        public string entitydescritpion { get; set; }
    }
}