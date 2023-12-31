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

        if (Context.ŅurrentUser == null)
        {
            await Shell.Current.GoToAsync(nameof(AuthorizationPage));
            return;
        }

        lbLogin.Text = Context.ŅurrentUser.Username;
        lbEmail.Text = Context.ŅurrentUser.Email;
        var source = Context.ŅurrentUser.ImageUrl ?? "icon_image.png"; 
        imgProfile.Source = source;

    }

    private void btnExit_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync(nameof(AuthorizationPage));
        Context.ŅurrentUser = null;
    }

    private async void btnSetPhoto_Clicked(object sender, EventArgs e)
    {
        var provile = new UpdateProfileRequest()
        {
            Id = Context.ŅurrentUser.Id,
            ImageUrl = tbImgUrl.Text,
        };

        await Context.apiClient.UpdateUserInfo(tbImgUrl.Text);
        imgProfile.Source = tbImgUrl.Text;
        tbImgUrl.Text = string.Empty;
    }
}