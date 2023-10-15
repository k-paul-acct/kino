using Kino.Mobile.Pages;

namespace Kino.Mobile;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
    }
}