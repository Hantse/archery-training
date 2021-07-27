using ArcheryTraining.Enums;
using ArcheryTraining.Interfaces;
using ArcheryTraining.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Realms;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ArcheryTraining.ViewModels
{
    public class HomeViewModel : ViewModelBase, IDisposable
    {
        private readonly ISessionRepository sessionRepository;
        private IDisposable syncToken;

        private IQueryable<Session> sessionsQuery;
        public ObservableCollection<Session> History { get; set; } = new ObservableCollection<Session>();

        private Session _active;

        public Session Active
        {
            get { return _active; }
            set
            {
                Set(ref _active, value);
            }
        }

        private bool _hasActive;

        public bool HasActive
        {
            get { return _hasActive; }
            set
            {
                Set(ref _hasActive, value);
            }
        }

        private bool _hasValue;

        public bool HasValue
        {
            get { return _hasValue; }
            set
            {
                Set(ref _hasValue, value);
            }
        }

        private bool _displayOnboard;

        public bool DisplayOnboard
        {
            get { return _displayOnboard; }
            set
            {
                Set(ref _displayOnboard, value);
            }
        }

        private bool _loadingInProgress;

        public bool LoadingInProgress
        {
            get { return _loadingInProgress; }
            set
            {
                Set(ref _loadingInProgress, value);
            }
        }

        public RelayCommand NewSessionCommand
        {
            get;
        }


        public HomeViewModel()
        {
            NewSessionCommand = new RelayCommand(async () => await Shell.Current.GoToAsync("session-create"));
            sessionRepository = DependencyService.Get<ISessionRepository>();
        }

        public async Task LoadSessionsHistoryAsync()
        {
            LoadingInProgress = true;

            sessionsQuery = await sessionRepository.GetAllUserSessionsAsync();
            RegisterObservable();

            SetAndPopulateView(sessionsQuery);
            LoadingInProgress = false;
        }

        private void RegisterObservable()
        {
            if (syncToken == null)
            {
                syncToken = sessionsQuery.SubscribeForNotifications((sender, changes, error) =>
                {
                    if (sender != null)
                    {
                        SetAndPopulateView(sender.AsQueryable());
                    }
                });
            }
        }

        public void SetAndPopulateView(IQueryable<Session> sessions)
        {
            History = new ObservableCollection<Session>(sessions);
            RaisePropertyChanged(() => History);

            HasValue = History.Any();
            DisplayOnboard = !HasValue;

            Active = sessions.Where(s => s.Status == SessionStatus.IN_PROGRESS.Value)
                                .OrderByDescending(s => s.Date)
                                .FirstOrDefault();
        }

        public void Dispose()
        {
            syncToken.Dispose();
        }
    }
}
