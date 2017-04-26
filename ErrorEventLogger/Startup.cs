using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ErrorEventLogger.Startup))]
namespace ErrorEventLogger
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
