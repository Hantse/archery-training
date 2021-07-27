using ArcheryTraining.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcheryTraining.Views.Global
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentPage
    {
        private readonly LoadingViewModel loadingViewModel = new LoadingViewModel();

        public Loading()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            await loadingViewModel.CheckAuthenticate();
        }
    }
}