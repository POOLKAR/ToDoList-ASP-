using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToDoList_Jefremov_.Startup))]
namespace ToDoList_Jefremov_
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
