using System.Globalization;
using System.Collections.Generic;
using Microsoft.Extensions.Localization;

namespace Faculty.AspUI.Localization
{
    public class ServerErrorLocalizer : IStringLocalizer
    {
        private const string CommonError = "CommonError";
        private const string RoleOneAndMore = "RoleOneAndMore";
        private readonly Dictionary<string, List<LocalizedString>> _resources;

        public ServerErrorLocalizer()
        {
            var en = new List<LocalizedString>
            {
                new (CommonError, "Please, check your login and password."),
                new (RoleOneAndMore, "One or more roles must be specified.")
            };

            var ru = new List<LocalizedString>()
            {
                new(CommonError, "Пожалуйста, проверьте свой логин и пароль."),
                new (RoleOneAndMore, "Должна быть указана одна или больше ролей.")
            };

            _resources = new Dictionary<string, List<LocalizedString>>
            {
                { "en", en },
                { "ru", ru }
            };
        }

        public LocalizedString this[string name]
        {
            get
            {
                var culture = CultureInfo.CurrentUICulture;
                var cultureName = culture.Name;
                var listLocalizedStrings = _resources[cultureName];
                var findLocalizedString = listLocalizedStrings.Find(x => x.Name == name);
                return findLocalizedString ?? new LocalizedString(name, name);
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures) => throw new System.NotImplementedException();

        public LocalizedString this[string name, params object[] arguments] => throw new System.NotImplementedException();
    }
}
