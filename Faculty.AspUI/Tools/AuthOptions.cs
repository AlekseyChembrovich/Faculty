using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace Faculty.AspUI.Tools
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
            Issuer = configuration["AuthOptions:Issuer"];
            Audience = configuration["AuthOptions:Audience"];
            Key = configuration["AuthOptions:Key"];
            Lifetime = int.Parse(configuration["AuthOptions:Lifetime"]);
        }
    }
}
