using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookApplication.Startup))]
namespace BookApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
