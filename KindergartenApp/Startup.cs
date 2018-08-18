using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KindergartenApp.Startup))]
namespace KindergartenApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
