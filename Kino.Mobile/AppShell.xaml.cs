using Kino.Mobile.Models;
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

            if (Context.СurrentUser == null ) 
            {
                favorites.IsVisible = false;
            }
            Routing.RegisterRoute(nameof(AuthorizationPage), typeof(AuthorizationPage));
            Routing.RegisterRoute(nameof(RegistrationPage), typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(MoviePage), typeof(MoviePage));
        }
    }
}