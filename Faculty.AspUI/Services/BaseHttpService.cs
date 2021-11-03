using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Faculty.AspUI.Services
{
    public class BaseHttpService
    {
        /// <summary>
        /// Http Client for sending request on authentication server.
        /// </summary>
        protected readonly HttpClient HttpClient;

        /// <summary>
        /// Constructor for init Http Client.
        /// </summary>
        /// <param name="httpClient">Http client.</param>
        public BaseHttpService(HttpClient httpClient)
        {
            HttpClient = httpClient;
        }

        /// <summary>
        /// Method for conversion Http response into instance type T.
        /// </summary>
        /// <typeparam name="T">Any type.</typeparam>
        /// <param name="response">An instance of the T type.</param>
        /// <returns></returns>
        protected static async Task<T> ConvertHttpResponseTo<T>(HttpResponseMessage response)
        {
            var modelJson = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<T>(modelJson);
            return model;
        }
    }
}
