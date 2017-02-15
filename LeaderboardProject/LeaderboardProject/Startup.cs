using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LeaderboardProject.Startup))]
namespace LeaderboardProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
