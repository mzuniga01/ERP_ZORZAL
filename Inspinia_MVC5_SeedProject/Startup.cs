using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ERP_GMEDINA.Startup))]
namespace ERP_GMEDINA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
