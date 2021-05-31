using Notes.Models;
using Notes.ViewModels;
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
    public partial class SirPosting : ContentPage
    {
        PostingA evm;
        public SirPosting()
        {
            InitializeComponent();
            //把盤點單編號丟給Label顯示給主管
            var x = Num.DocNum;
            N.Text = "盤點單編號:"+x;

            evm = new PostingA();
        }
        
        //完成盤點，返回盤點首頁
        private async void gotomainpage(Object sender, EventArgs e)
        {
            
            bool answer = await DisplayAlert("⚠️警告", "要送出盤點結果嗎?", "確定", "取消");
            if (answer)
            {
                evm.Posting();
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
            }
        }
    }
}