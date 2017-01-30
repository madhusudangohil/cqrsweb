using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cqrswebapi.Startup))]
namespace cqrswebapi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
