using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;
//C類商品盤點
namespace Notes
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class inventory2 : ContentPage
    {
        //抓資料
        public IList<test> tests { get; set; }
        
        public inventory2()
        {
            InitializeComponent();
            tests = new List<test>();
            tests.Add(new test() { Id = 1, Name = "原子筆", Code = "C001", Quantity = 20 });
            tests.Add(new test() { Id = 2, Name = "鉛筆", Code = "C002", Quantity = 20 });
            tests.Add(new test() { Id = 3, Name = "奇異筆", Code = "C003", Quantity = 20 });

            BindingContext = this;
        }
        public class test
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public String Code { get; set; }
            public int Quantity { get; set; }
        }
        //Barcode相機設立
        void ZXingScannerView_OnScanResult(ZXing.Result result)
        {
            var option = new ZXing.Mobile.MobileBarcodeScanningOptions()
            {
                PossibleFormats = new List<ZXing.BarcodeFormat>() { ZXing.BarcodeFormat.QR_CODE },
                CameraResolutionSelector = DependencyService.Get<IZXingHelper>().SelectLowestResolutionMatchingDisplayAspectRatio
            };
        //相機掃描到的資料
            Device.BeginInvokeOnMainThread(() =>
            {
                BarText.Text = result.Text;
            });
        }
        //完成盤點，返回盤點首頁
        private async void gotoinventory(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new inventory());
        }

    }


}