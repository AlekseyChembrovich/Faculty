using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Faculty.AspUI
{
    public class AuthOptions
    {
        public string Issuer;
        public string Audience;
        public string Key;
        public int Lifetime;

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public AuthOptions(IConfiguration configuration)
        {
            Issuer = configuration.GetSection("AuthOptions").GetValue(typeof(string), "Issuer").ToString();
            Audience = configuration.GetSection("AuthOptions").GetValue(typeof(string), "Audience").ToString();
            Key = configuration.GetSection("AuthOptions").GetValue(typeof(string), "Key").ToString();
            Lifetime = int.Parse(configuration.GetSection("AuthOptions").GetValue(typeof(string), "Lifetime").ToString() ?? string.Empty);
        }
    }
}
