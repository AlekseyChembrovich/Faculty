using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Faculty.ResourceServer.Tools
{
    /// <summary>
    /// Authentication options.
    /// </summary>
    public class AuthOptions
    {
        /// <summary>
        /// Token creator
        /// </summary>
        public string Issuer;
        /// <summary>
        /// Token consumer.
        /// </summary>
        public string Audience;
        /// <summary>
        /// Secret key.
        /// </summary>
        public string Key;
        /// <summary>
        /// Life time.
        /// </summary>
        public int Lifetime;

        /// <summary>
        /// Constructor for init options.
        /// </summary>
        /// <param name="configuration">Configuration with file app settings.</param>
        public AuthOptions(IConfiguration configuration)
        {
            Issuer = configuration["AuthOptions:Issuer"];
            Audience = configuration["AuthOptions:Audience"]; 
            Key = configuration["AuthOptions:Key"];
            Lifetime = int.Parse(configuration["AuthOptions:Lifetime"]);
        }

        /// <summary>
        /// Method for conversion secret key in flow bytes.
        /// </summary>
        /// <returns>An instance of the SymmetricSecurityKey class.</returns>
        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
