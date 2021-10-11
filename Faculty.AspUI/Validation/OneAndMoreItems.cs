using System.Collections;
using Faculty.AspUI.Localization;
using Microsoft.Extensions.Localization;
using System.ComponentModel.DataAnnotations;

namespace Faculty.AspUI.Validation
{
    public class OneAndMoreItems : ValidationAttribute
    {
        private readonly IStringLocalizer _stringLocalizer = new ServerErrorLocalizer();

        public override bool IsValid(object value)
        {
            if (ErrorMessage != null) ErrorMessage = _stringLocalizer[ErrorMessage];
            if (value is IList list)
            {
                return list.Count > 0;
            }
            return false;
        }
    }
}
