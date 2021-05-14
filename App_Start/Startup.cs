using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ChattingWithSignalR.App_Start.Startup))]
namespace ChattingWithSignalR.App_Start
{ 
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here  
            app.MapSignalR();
        }
    }
}