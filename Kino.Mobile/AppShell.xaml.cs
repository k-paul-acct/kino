using Kino.Mobile.Pages;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.Net;

namespace Kino.Mobile
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AuthorizationPage), typeof(AuthorizationPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
        }
    }
}