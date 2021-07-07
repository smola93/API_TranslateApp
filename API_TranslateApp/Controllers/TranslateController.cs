using API_TranslateApp.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        
        [HttpPost]
        public async Task<ActionResult> GetTranslationAsync(TranslateModel model)
        {
            string text = model.text;
            ApiController apiController = new ApiController();
            model.response = await apiController.GetApiTranslation(text, "pl", "en");
            return View("Translate", model);
        }

        
    }
}
