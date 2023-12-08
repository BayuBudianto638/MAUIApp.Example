using MAUIApp.Example.Views.EmployeeViews;
using MAUIApp.Example.Views.HomeViews;
using MAUIApp.Example.Views.LoginViews;

namespace MAUIApp.Example
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            //Routing.RegisterRoute("login", typeof(LoginViewPage));
            //Routing.RegisterRoute("home", typeof(HomeViewPage));
            //Routing.RegisterRoute("employee", typeof(EmployeeViewPage));
        }
    }
}
