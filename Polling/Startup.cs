using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Polling.Startup))]
namespace Polling
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
