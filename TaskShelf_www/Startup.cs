using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskShelf_www.Startup))]
namespace TaskShelf_www
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
