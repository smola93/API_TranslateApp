using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_TranslateApp.Controllers
{
    public class TranslateController : Controller
    {
        public IActionResult Translate()
        {
            return View();
        }
        public IActionResult Languages()
        {
            return View();
        }
        public string GetTranslation()
        {
            return "GetTranslation method invoked!";
        }

        
    }
}
