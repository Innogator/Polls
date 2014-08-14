using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Polling.WebUI.Startup))]
namespace Polling.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
