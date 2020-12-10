using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(crUmbraco.Startup))]
namespace crUmbraco
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
