using System;

namespace TPDMS.RestApi.CustomExceptions
{
    public class FieldSizeViolationException : Exception
    {
        public FieldSizeViolationException(string fieldName, int? length, bool exceedsLength)
            : base(string.Format("Error: {0} has greater size than " + length + " characters,please correct your input max length", fieldName, length))
        {
            FieldName = fieldName;
            Length = length;
            ExceedsLength = exceedsLength;
        }

        public string FieldName { get; set; }

        public int? Length { get; set; }

        public bool ExceedsLength { get; set; }

        public string ComparisonStr { get; set; }

        public override string ToString()
        {
            ComparisonStr = ExceedsLength ? " more " : " less ";
            return $"{FieldName} has {ComparisonStr} than {Length} characters." + base.ToString();
        }
    }
}