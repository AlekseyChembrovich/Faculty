using System.Collections;
using Faculty.AspUI.Controllers;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.Tools
{
    /// <summary>
    /// Custom validation attribute. Count user roles should be one and more.
    /// </summary>
    public class RoleAttribute : ValidationAttribute
    {
        /// <summary>
        /// Execute validation.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the ValidationResult class.</returns>
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
