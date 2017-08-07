using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LAIMIStock.Startup))]
namespace LAIMIStock
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
