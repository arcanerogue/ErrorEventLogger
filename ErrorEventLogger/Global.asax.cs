using ErrorEventLogger.Controllers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using log4net;
using OneTrueError.Client;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

namespace ErrorEventLogger
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private static readonly ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase
                .GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            	//var url = new Uri(ConfigurationManager.AppSettings["OneTrueServerUrl"]);
	            //OneTrue.Configuration.Credentials(url, ConfigurationManager.AppSettings["OneTrueAppKey"],  ConfigurationManager.AppSettings["OneTrueSecretKey"]);
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();

            if(ex != null)
            {
                StringBuilder errorString = new StringBuilder();
                errorString.Append("An error was caught in the Application_Error event.\n");
                errorString.Append("Error in: " + (Context.Session == null ? string.Empty : Request.Url.ToString()));
                errorString.Append("\nError Message: " + ex.Message);

                if (ex.InnerException != null)
                    errorString.Append("\nInner Error Message: " + ex.InnerException.Message);

                errorString.Append("\n\nStack Trace: " + ex.StackTrace);

                Server.ClearError();

                if(Context.Session != null)
                {
                    //errorString.Append($"Session: Identity name:[{Thread.CurrentPrincipal.Identity.Name}] IsAuthenticated:{Thread.CurrentPrincipal.Identity.IsAuthenticated}");
                }
                
                log.Error(errorString.ToString());

                if (Context.Session != null)
                {
                    var routeData = new RouteData();
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "Error");
                    routeData.Values.Add("exception", ex);

                    if(ex.GetType() == typeof(HttpException))
                    {
                        routeData.Values.Add("statusCode", ((HttpException)ex).GetHttpCode());
                    }
                    else
                    {
                        routeData.Values.Add("statusCode", 500);
                    }

                    Response.TrySkipIisCustomErrors = true;
                    IController controller = new ErrorController();
                    controller.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
                    Response.End();
                }
            }
        }
    }
}
