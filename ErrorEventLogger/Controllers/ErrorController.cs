using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using log4net;

namespace ErrorEventLogger.Controllers
{
    public class ErrorController : Controller
    {
        //private static readonly ILog log = 
        //    log4net.LogManager.GetLogger(System.Reflection.MethodBase
        //        .GetCurrentMethod().DeclaringType);

        public ActionResult Error(int statusCode, Exception exception)
        {
            Response.StatusCode = statusCode;

            var error = new Models.ErrorModel
            {
                StatusCode = statusCode.ToString(),
                Description = HttpWorkerRequest.GetStatusDescription(statusCode),
                Message = exception.Message,
                DateTime = DateTime.Now
            };

            return View(error);
        }
    }
}