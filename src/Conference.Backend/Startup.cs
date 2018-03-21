using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Conference.Backend.Startup))]

namespace Conference.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}