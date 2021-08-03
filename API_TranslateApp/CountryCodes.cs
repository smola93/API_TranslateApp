using System.Globalization;

namespace API_TranslateApp
{
    public class CountryCodes
    {
        public string GetFullCountryName(string code)
        {
            var cultureInfo = new CultureInfo(code);
            return cultureInfo.EnglishName;
        }
    }
}
