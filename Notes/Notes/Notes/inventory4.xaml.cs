using Notes.Models;
using Notes.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
//A類商品盤點
namespace Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class inventory4 : ContentPage
    {
        InventoryA evm;
        public inventory4()
        {
            InitializeComponent();
            evm = new InventoryA();
        }

        //Barcode相機設立
        public void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            var option = new ZXing.Mobile.MobileBarcodeScanningOptions()
            {
                PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE },
                CameraResolutionSelector = DependencyService.Get<IZXingHelper>().SelectLowestResolutionMatchingDisplayAspectRatio
            };
            //相機掃描到的資料顯示
            var x = result.Text.Split(',');
            string ItemCode = x[0];
            string DistNumber = x[1];
            int gg = 0;
            Device.BeginInvokeOnMainThread(() =>
            {
                foreach (var Peko in evm.Data)
                {
                    //確認掃到的條碼是否存在於Data裡
                    if (Peko.ItemCode == ItemCode && Peko.DistNumber == DistNumber)
                    {
                        gg = 1;
                    }
                }
                foreach (var Peko in evm.Data)
                {
                    //編號、序號一樣，盤點成功
                    if (Peko.ItemCode == ItemCode && Peko.DistNumber == DistNumber && gg==1 )
                    {
                        Peko.Whether = "GreenTick.png";
                        break;
                    }
                    //編號一樣，序號不一樣，盤營新增
                    else if (Peko.ItemCode == ItemCode && gg==0)
                    {
                        Add(ItemCode, DistNumber);
                        break;
                    }
                    //編號、序號不一樣，不是盤點商品，提醒使用者
                    else if (Peko.ItemCode != ItemCode && gg == 0)
                    {
                        Bartext.Text = "此商品並非此次盤點項目";
                        AlertAsync();
                        break;
                    }
                }
                BindingContext = evm;
                Bartext.Text = result.Text;
                UpdateListView();
            });

        }
        //更新ListView
        void UpdateListView()
        {
            var itemsSource = Listview.ItemsSource;
            Listview.ItemsSource = null;
            Listview.ItemsSource = itemsSource;
        }
        //新增商品
        void Add(string ItemCode, string DistNumber)
        {
            DataInventory InventoeyADD = new DataInventory() { ItemCode = ItemCode, DistNumber = DistNumber, Whether = "GreenTick.png" };
            evm.Data.Add(InventoeyADD);
        }
        //非盤點商品，警告使用者
        async void AlertAsync ()
        {
            await DisplayAlert("⚠️警告", "此條碼並非盤點商品", "確認");
        }


        //完成盤點，返回首頁
        private async void gotoinventory(Object sender, EventArgs e)
        {
            bool answer = await DisplayAlert("⚠️警告", "要送出盤點結果嗎?", "確定", "取消");
            if(answer)
            {
                foreach (var item in evm.Data)
                {
                    evm.InsertData(item.ItemCode, item.DistNumber, item.Whether);
                }
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
            }    
            //evm.PostData();
        }

    }


}