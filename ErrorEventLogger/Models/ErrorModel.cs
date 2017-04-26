using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ErrorEventLogger.Models
{
    public class ErrorModel
    {
        public string StatusCode { get; set; }
        public string Description { get; set; }
        public string Message { get; set; }
        public DateTime DateTime { get; set; }
    }
}