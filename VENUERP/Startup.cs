using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
 

[assembly: OwinStartup(typeof(VENUERP.Startup))]

namespace VENUERP
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
        private void ConfigureAuth(IAppBuilder app)
        {
            
        }
    }
}
