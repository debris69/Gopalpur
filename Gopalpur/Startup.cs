using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Gopalpur.Startup))]
namespace Gopalpur
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
