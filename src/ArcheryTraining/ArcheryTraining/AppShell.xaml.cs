using ArcheryTraining.Views.Session;
using System;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.CommunityToolkit.UI.Views.Options;

namespace ArcheryTraining
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute("pratice", typeof(PraticePage));
            Routing.RegisterRoute("session-create", typeof(SessionCreate));

            MessagingCenter.Subscribe<Shell, string>(this, "Toast", async (sender, arg) =>
            {
                await this.DisplayToastAsync(new ToastOptions()
                {
                    MessageOptions = new MessageOptions()
                    {
                        Message = arg
                    },
                    Duration = TimeSpan.FromSeconds(2)
                });
            });
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
