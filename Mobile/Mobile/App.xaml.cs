using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using AgoraAcademy.AgoraEgo.Mobile.Services;
using AgoraAcademy.AgoraEgo.Mobile.Views;

namespace AgoraAcademy.AgoraEgo.Mobile
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
