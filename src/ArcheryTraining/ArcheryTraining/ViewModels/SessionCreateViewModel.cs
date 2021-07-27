using ArcheryTraining.Enums;
using ArcheryTraining.Interfaces;
using ArcheryTraining.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;

namespace ArcheryTraining.ViewModels
{
    public class SessionCreateViewModel : ViewModelBase
    {
        private readonly ISessionRepository sessionRepository;

        public List<string> SessionArea { get; set; } = new List<string>()
        {
            "Indoor",
            "Outdoor",
            "Custom"
        };

        public List<long> Distance { get; set; } = new List<long>()
        {
            18,
            20,
            25,
            30,
            40,
            50,
            60,
            70,
            80,
            90
        };

        public List<string> SessionType { get; set; } = new List<string>()
        {
            "Solo",
            "Duo",
            "Team"
        };


        private DateTime _date = DateTime.UtcNow;
        public DateTime Date { get { return _date; } set { Set(ref _date, value); } }

        private string _area;
        public string Area { get { return _area; } set { Set(ref _area, value); } }

        private int _distanceSelected;
        public int DistanceSelected { get { return _distanceSelected; } set { Set(ref _distanceSelected, value); } }

        private string _type;
        public string Type { get { return _type; } set { Set(ref _type, value); } }

        public RelayCommand SaveCommand
        {
            get;
        }

        public SessionCreateViewModel()
        {
            SaveCommand = new RelayCommand(async () => await SaveSessionAsync());
            sessionRepository = DependencyService.Get<ISessionRepository>();
        }

        public async Task SaveSessionAsync()
        {
            var session = new Session()
            {
                Date = Date,
                Distance = DistanceSelected,
                Type = Type,
                Status = SessionStatus.IN_PROGRESS.Value
            };

            var sessionResult = await sessionRepository.WriteSessionAsync(session);

            if (sessionResult.Id != null) {
                MessagingCenter.Send<Shell, string>(Shell.Current, "Toast", "Session create.");
                await Shell.Current.Navigation.PopAsync();
            }
        }
    }
}
