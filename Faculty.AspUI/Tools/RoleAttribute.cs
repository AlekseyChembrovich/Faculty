using System.Collections;
using Faculty.AspUI.Controllers;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.Tools
{
    public class RoleAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var localizationService = (IStringLocalizer<UserController>)validationContext.GetService(typeof(IStringLocalizer<UserController>));
            var localizedError = localizationService[ErrorMessage];
            if (value is IList list)
            {
                return list.Count > 0 ? ValidationResult.Success : new ValidationResult(localizedError);
            }
            return new ValidationResult(localizedError);
        }
    }
}
