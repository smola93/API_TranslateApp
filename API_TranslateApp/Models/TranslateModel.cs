using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_TranslateApp.Models
{
    public class TranslateModel
    {
        public string text { get; set; }
        public string response { get; set; }
        public string source { get; set; }
        public string result { get; set; }

        public TranslateModel()
        {

        }
    }
}
