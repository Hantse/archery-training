using ArcheryTraining.Interfaces;
using ArcheryTraining.Repositories;
using ArcheryTraining.Services;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Forms;

namespace ArcheryTraining
{
    public partial class App : Application
    {

        public App()
        {
            try
            {
                InitializeComponent();
                DependencyService.Register<ISessionRepository, SessionRepository>();
                DependencyService.Register<ISecurityService, SecurityService>();
                DependencyService.Register<ISessionService, SessionService>();
                MainPage = new AppShell();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=a5ae7750-26b5-492e-b25d-dee3ba73a411", typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
