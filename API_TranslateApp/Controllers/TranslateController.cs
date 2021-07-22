﻿using API_TranslateApp.Models;
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
            return View(new TranslateModel());
        }
        public IActionResult Languages()
        {
            return View();
        }
        public IActionResult Detect()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> GetTranslationAsync(TranslateModel model)
        {
            CountryCodes countryCodes = new CountryCodes();
            string text = model.text;
            model.source = Request.Form["source"].ToString();
            string sourceCountry = countryCodes.GetFullCountryName(model.source);
            model.result = Request.Form["result"].ToString();
            string resultCountry = countryCodes.GetFullCountryName(model.result);
            ApiController apiController = new ApiController();
            model.response = await apiController.GetApiTranslation(text, model.result, model.source);
            model.source += " - " + sourceCountry;
            model.result += " - " + resultCountry;
            return View("Translate", model);
        }

        [HttpPost]
        public async Task<ActionResult> DetectAsync(TranslateModel model)
        {
            CountryCodes countryCodes = new CountryCodes();
            List<string> resultArray;
            string text = model.text;
            ApiController apiController = new ApiController();
            resultArray = await apiController.DetectLanguage(text);
            model.detectResult = "Detected language: " + countryCodes.GetFullCountryName(resultArray[0]) + ", Confidence: " + resultArray[1];
            return View("Detect", model);
        }

        [HttpPost]
        public async Task<ActionResult> PassDropdownValue(TranslateModel model, string command)
        {
            CountryCodes countryCodes = new CountryCodes();
            string code = Request.Form["dowList"].ToString();
            string country = countryCodes.GetFullCountryName(code);
            if (command.Equals("source"))
            {
                model.source = code + " - " + country;
            }
            else
            {
                model.result = code + " - " + country;
            }
            return View("Translate", model);
        }

    }
}
