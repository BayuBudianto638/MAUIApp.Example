using MAUIApp.Example.Models;
using System.Text.Json;

namespace MAUIApp.Example
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
