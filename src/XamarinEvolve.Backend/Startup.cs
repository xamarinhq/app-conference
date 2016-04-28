using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(XamarinEvolve.Backend.Startup))]

namespace XamarinEvolve.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}