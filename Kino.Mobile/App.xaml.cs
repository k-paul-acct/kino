using Kino.Mobile.Pages;
using Microsoft.Maui.Controls.PlatformConfiguration;

namespace Kino.Mobile
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