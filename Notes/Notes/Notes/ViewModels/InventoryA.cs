using Newtonsoft.Json;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static Notes.inventory;

namespace Notes.ViewModels
{
    class InventoryA : INotifyPropertyChanged
    {

        public InventoryA()
        {
            GetData();
        }

        public async void GetData()
        {
            //Get
            using (var client = new HttpClient())
            {
                var x = Num.DocNum;
                //使用者輸入X為盤點單編號，透過此編號進入MSSQL抓取盤點單資料，並將資料存入Class Inventory
                var uri = "http://192.168.0.106:8056/API/api/TEST/GetInventory?x=" + x;
                var result = await client.GetStringAsync(uri);
                //將Inventory裡的商品編號、倉庫當作條件抓取位於指定倉庫裡某商品的序號，並將資料存入Class ANumber
                uri = "http://192.168.0.106:8056/API/api/TEST/GetANumber";
                result = await client.GetStringAsync(uri);
                //handling the answer  
                List<DataInventory>  DataList = JsonConvert.DeserializeObject<List<DataInventory>>(result);
                Data = new ObservableCollection<DataInventory>(DataList);               
            }
        }

        ObservableCollection<DataInventory> _Data;
        public ObservableCollection<DataInventory> Data
        {
            get
            {
                return _Data;
            }
            set
            {
                _Data = value;
                OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //將盤點好的DATA丟回Web API
        public async void InsertData(string ItemCode,string DistNumber,string Whether)
        {
            using(var client = new HttpClient())
            {
                var uri = "http://192.168.0.106:8056/API/api/TEST/GetA?ItemCode="+ ItemCode + "&DistNumber=" + DistNumber + "&Whether=" +Whether;
                var result = await client.GetStringAsync(uri);
            }
        }

    }
}


