using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MyJSGames.Startup))]
namespace MyJSGames
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
