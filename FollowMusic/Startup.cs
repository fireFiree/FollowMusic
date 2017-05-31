using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FollowMusic.Startup))]
namespace FollowMusic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
