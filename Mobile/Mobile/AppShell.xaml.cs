using System;
using System.Collections.Generic;
using AgoraAcademy.AgoraEgo.Mobile.ViewModels;
using AgoraAcademy.AgoraEgo.Mobile.Views;
using Xamarin.Forms;

namespace AgoraAcademy.AgoraEgo.Mobile
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
