using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fedisal_Becario.Startup))]
namespace Fedisal_Becario
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
