using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class inventory : ContentPage
    {      
        public inventory()
        {
             InitializeComponent();
            
        }
       
        //前往盤點 注意!這邊要設定會前往ABC哪類商品的盤點頁面(目前還沒設定)
        private async void gotoinventory2(Object sender, EventArgs e)
        {
            //取出Entry的Text
            string x = DocNum.Text;
            //將DocNum放入Num類別裡
            Num.DocNum = x;
            await Navigation.PushAsync(new inventory4());
        }
        //返回首頁
        private async void gotomainpage(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}