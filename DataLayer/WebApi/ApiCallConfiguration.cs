using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace DataLayer.WebApi
{
    public class ApiCallConfiguration<T>
    {
        public string Path { get; set; }

        public string PathWithQueryStrings
        {
            get
            {
                if (QueryStrings.Count > 0)
                {
                    var keys = new List<string>(QueryStrings.Keys);
                    foreach (var k in keys)
                    {
                        if (QueryStrings[k] == null)
                            QueryStrings[k] = "";
                    }

                    return $"{Path}{QueryStrings}";
                }

                return Path;
            }
        }

        public Dictionary<string, string> QueryStrings { get; }

        public T Content { get; set; }

        public StringContent ContentJson
        {
            get
            {
                if (Content == null)
                    return null;

                return new StringContent(JsonConvert.SerializeObject(Content), Encoding.UTF8, "application/json");
            }
        }

        public ApiCallConfiguration()
        {
            QueryStrings = new Dictionary<string, string>();
        }
    }
}
