using System.ComponentModel.DataAnnotations;

namespace TPDMS.RestApi.CustomExceptions
{
    public static class Validations
    {
        public static bool IsModelValid(object model)
        {
            var validationContext = new ValidationContext(model, null, null);
            return Validator.TryValidateObject(model, validationContext, null, true);
        }
    }
}