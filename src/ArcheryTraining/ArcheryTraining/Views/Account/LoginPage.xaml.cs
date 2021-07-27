using ArcheryTraining.Interfaces;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcheryTraining.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            var service = DependencyService.Get<ISecurityService>();
            var authResult = await service.AuthenticateProcessAsync();

            if (authResult.success)
            {
                await Shell.Current.GoToAsync("//home");
            }
        }
    }
}