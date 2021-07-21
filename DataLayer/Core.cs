using DataLayer.WebApi;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Core
    {
        private HttpClient client;
        private string baseUrl = ConfigurationManager.AppSettings["BaseUrl"].ToString();
        private string xRapidApiKey = ConfigurationManager.AppSettings["RapidApiKey"].ToString();

        public async Task<ApiResult<T>> Get<T>(Action<ApiCallConfiguration<T>> action)
        {
            HttpClient(baseUrl);
            try
            {
                var config = new ApiCallConfiguration<T>();
                action(config);
                var response = await client.GetAsync(config.PathWithQueryStrings);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResult<T>>(content);

                return result;
            }
            catch (Exception e)
            {
                //Register exception in database log
                return new ApiResult<T> { Message = e.Message };
            }
        }

        private HttpClient HttpClient(string baseUrl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Add("x-rapidapi-key", xRapidApiKey);
            client.Timeout = TimeSpan.FromMinutes(1);
            return client;
        }
    }
}
