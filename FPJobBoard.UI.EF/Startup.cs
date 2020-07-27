using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPJobBoard.UI.EF.Startup))]
namespace FPJobBoard.UI.EF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
