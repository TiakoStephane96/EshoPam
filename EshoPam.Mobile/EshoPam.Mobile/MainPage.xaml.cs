using EshoPam.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace EshoPam.Mobile
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void BtnConnect_Clicked(object sender, EventArgs e)
        {
            Loader.IsVisible = true;
            BtnConnect.IsEnabled = false;
            try
            {
                UserService service = new UserService(App.ServiceBaseAddress);
                var user = await service.LoginAsync(TxtUserName.Text, TxtPassword.Text);
                await DisplayAlert("Good", user.Fullname, "Ok");
            }
            catch (UnauthorizedAccessException ex)
            {
                await DisplayAlert("Bad", ex.Message, "Ok");
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.Message);
                await DisplayAlert("Bad", "An error occured !", "Ok");
            }
            Loader.IsVisible = false;
            BtnConnect.IsEnabled = true;
        }

        private async void BtnRegister_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RegisterPage());
        }

        private void BtnEye_Clicked(object sender, EventArgs e)
        {
            TxtPassword.IsPassword = !TxtPassword.IsPassword;
            FisEye.FontFamily = TxtPassword.IsPassword ? "FontFaSolid900" : "FontFaRegular400";
        }
    }
}
