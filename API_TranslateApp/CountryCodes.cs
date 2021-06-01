using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
