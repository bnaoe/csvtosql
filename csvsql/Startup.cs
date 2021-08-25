using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(csvsql.Startup))]
namespace csvsql
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
