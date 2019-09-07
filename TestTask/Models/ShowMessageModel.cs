using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class ShowMessageModel
    {
        public int SecondsToRedirect { get; set; } = 2;
        public string Message { get; set; } = "Перенаправление...";
        public string Title { get; set; } = "Перенаправление...";
        public string Url { get; set; } = "/";
        public bool Error { get; set; } = false;
    }
}
