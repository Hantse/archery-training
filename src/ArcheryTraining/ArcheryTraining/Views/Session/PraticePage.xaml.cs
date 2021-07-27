using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ArcheryTraining.Views.Session
{
    [QueryProperty(nameof(SessionId), "session-id")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PraticePage : ContentPage
    {
        public string SessionId { get; set; }

        public PraticePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}