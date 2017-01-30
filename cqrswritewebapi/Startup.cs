using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(cqrswritewebapi.Startup))]
namespace cqrswritewebapi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
