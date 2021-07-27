using ArcheryTraining.ViewModels;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcheryTraining.Views.Global
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        public readonly HomeViewModel homeViewModel = new HomeViewModel();

        public HomePage()
        {
            InitializeComponent();
            this.BindingContext = homeViewModel;
        }

        protected override async void OnAppearing()
        {
            await homeViewModel.LoadSessionsHistoryAsync();
        }

        private async void ShowSession_Tapped(object sender, ItemTappedEventArgs e)
        {
            var session = e.Item as Models.Session;
            await Shell.Current.GoToAsync($"pratice?session-id={session.Id}");
        }
    }
}