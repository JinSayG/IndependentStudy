using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Newtonsoft.Json;
using System.Net.Http;


namespace Notes
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

        }
        //前往盤點
        private async void gotoinventory(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new inventory());
        }
        //前往過帳
        private async void gotoposting(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SirPosting());
        }
        //前往收貨
        private async void gotoimport(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new import());
        }
        //前往發貨
        private async void gotoexport(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new export());
        }


    }
}
