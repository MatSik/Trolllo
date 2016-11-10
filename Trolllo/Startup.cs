using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trolllo.Startup))]
namespace Trolllo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
