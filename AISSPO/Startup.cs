using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AISSPO.Startup))]
namespace AISSPO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
