using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API_TranslateApp
{
    public class ApiController
    {
        private static readonly HttpClient client = new HttpClient();
        public async Task<List<string>> GetAllLanguages()
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
                var body = await response.Content.ReadAsStringAsync();
                AllLanguagesData result = JsonConvert.DeserializeObject<AllLanguagesData>(body);

                List<Language> listOfCountryCodesObj = result.languages.language;
                List<string> listOfCountryCodes = new List<string>();

                foreach (var item in listOfCountryCodesObj)
                {
                    listOfCountryCodes.Add(item.language);
                }

                return listOfCountryCodes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetApiTranslation(string message, string target, string source)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2"),
                    Headers =
    {
        { "x-rapidapi-key", "f36cc37f00msh60980c9dd08911bp1635d4jsnbb12aedaf508" },
        { "x-rapidapi-host", "google-translate1.p.rapidapi.com" },
    },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "q", message },
        { "target", target },
        { "source", source },
    }),
                };
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                TranslationData result = JsonConvert.DeserializeObject<TranslationData>(body);
                List<TranslatedText> translation = result.translations.translatedTexts;
                body = translation[0].translatedText;

                return body;
            }
            catch (Exception ex)
            {
                return "ops! something went wrong.\n"
                    + "Please be sure to use two different languages and have your message understandable.\n"
                    + ex.Message;
                throw;
            }
        }

        public async Task<List<string>> DetectLanguage(string message)
        {
            try
            {
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Post,
                    RequestUri = new Uri("https://google-translate1.p.rapidapi.com/language/translate/v2/detect"),
                    Headers =
    {
        { "x-rapidapi-key", "f36cc37f00msh60980c9dd08911bp1635d4jsnbb12aedaf508" },
        { "x-rapidapi-host", "google-translate1.p.rapidapi.com" },
    },
                    Content = new FormUrlEncodedContent(new Dictionary<string, string>
    {
        { "q", message },
    }),
                };
                HttpResponseMessage response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                DetectionData result = JsonConvert.DeserializeObject<DetectionData>(body);
                List<DetectionNodes> detectionNodes = result.detections.detectionList[0];
                List<string> resultList = new List<string>();
                resultList.Add(detectionNodes[0].language);
                resultList.Add(detectionNodes[0].confidence.ToString());

                return resultList;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    //Languages response
    public class AllLanguagesData
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
    //Translate response
    public class TranslationData
    {
        [JsonProperty("data")]
        public Translations translations { get; set; }
    }
    public class Translations
    {
        [JsonProperty("translations")]
        public List<TranslatedText> translatedTexts { get; set; }
    }
    public class TranslatedText
    {
        [JsonProperty("translatedText")]
        public string translatedText { get; set; }
    }
    //Detect response
    public class DetectionData
    {
        [JsonProperty("data")]
        public Detections detections { get; set; }
    }
    public class Detections
    {
        [JsonProperty("detections")]
        public List<List<DetectionNodes>> detectionList { get; set; }
    }
    public class DetectionNodes
    {
        public bool isReliable { get; set; }
        public int confidence { get; set; }
        public string language { get; set; }
    }
}