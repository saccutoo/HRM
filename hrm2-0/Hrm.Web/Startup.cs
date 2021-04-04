using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Hrm.Web.Startup))]
namespace Hrm.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}
