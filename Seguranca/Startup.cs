using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Seguranca.Startup))]
namespace Seguranca
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
