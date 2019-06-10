using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DataManager.Startup))]
namespace DataManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
