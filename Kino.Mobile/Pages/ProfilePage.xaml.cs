using Kino.ApiClient.Requests;
using Kino.Mobile.Models;

namespace Kino.Mobile.Pages;

public partial class ProfilePage : ContentPage
{
	public ProfilePage()
	{
        InitializeComponent();
	}

    protected async override void OnAppearing()
    {
        base.OnAppearing();

        if (Context.ÑurrentUser == null)
        {
            await Shell.Current.GoToAsync(nameof(AuthorizationPage));
            return;
        }

        lbLogin.Text = Context.ÑurrentUser.Username;
        lbEmail.Text = Context.ÑurrentUser.Email;
        var source = Context.ÑurrentUser.ImageUrl ?? "icon_image.png"; 
        imgProfile.Source = source;

    }

    private void btnExit_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AuthorizationPage));
        Context.ÑurrentUser = null;
    }

    private async void btnSetPhoto_Clicked(object sender, EventArgs e)
    {
        var provile = new UpdateProfileRequest()
        {
            Id = Context.ÑurrentUser.Id,
            ImageUrl = tbImgUrl.Text,
        };

        await Context.apiClient.UpdateUserInfo(tbImgUrl.Text);
        imgProfile.Source = tbImgUrl.Text;
        tbImgUrl.Text = string.Empty;
    }
}