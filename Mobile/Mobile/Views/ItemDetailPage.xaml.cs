using System.ComponentModel;
using Xamarin.Forms;
using AgoraAcademy.AgoraEgo.Mobile.ViewModels;

namespace AgoraAcademy.AgoraEgo.Mobile.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}