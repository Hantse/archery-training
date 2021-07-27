
using ArcheryTraining.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;

namespace ArcheryTraining.Views.Session
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SessionCreate : ContentPage
    {
        private readonly SessionCreateViewModel sessionCreateViewModel = new SessionCreateViewModel();

        public SessionCreate()
        {
            InitializeComponent();
            this.BindingContext = sessionCreateViewModel;
        }
    }
}