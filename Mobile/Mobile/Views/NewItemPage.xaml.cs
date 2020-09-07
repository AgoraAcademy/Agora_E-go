using System;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using AgoraAcademy.AgoraEgo.Mobile.Models;
using AgoraAcademy.AgoraEgo.Mobile.ViewModels;

namespace AgoraAcademy.AgoraEgo.Mobile.Views
{
    public partial class NewItemPage : ContentPage
    {
        public Item Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();
            BindingContext = new NewItemViewModel();
        }
    }
}