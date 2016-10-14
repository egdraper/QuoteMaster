using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuoteMaster.Startup))]
namespace QuoteMaster
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
