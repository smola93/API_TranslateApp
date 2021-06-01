using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace API_TranslateApp
{
    public class AllLanguages
    {
        protected static readonly HttpClient client = new HttpClient();
        public async Task<List<string>> GetResponse()
        {

            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://gogle-translate1.p.rapidapi.com/language/translate/v2/languages"),
                    Headers =
    {
        { "x-rapidapi-key", "f36cc37f00msh60980c9dd08911bp1635d4jsnbb12aedaf508" },
        { "x-rapidapi-host", "google-translate1.p.rapidapi.com" },
    },
                };

                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(content);
                Data result = JsonConvert.DeserializeObject<Data>(content);

                List<Language> listOfCountryCodesObj = result.languages.language;
                List<string> listOfCountryCodes = new List<string>();


                foreach (var item in listOfCountryCodesObj)
                {
                    listOfCountryCodes.Add(item.language);
                }

                return listOfCountryCodes;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //TODO Exception handling
                throw new Exception();
            }

        }

    }

    public class Data
    {
        [JsonProperty("data")]
        public Languages languages { get; set; }
    }

    public class Languages
    {
        [JsonProperty("languages")]
        public List<Language> language { get; set; }
    }
    public class Language
    {
        [JsonProperty("language")]
        public string language { get; set; }
    }


}
