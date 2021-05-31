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
    class PostingA : INotifyPropertyChanged
    {

        public PostingA()
        {
            PostData();
        }


        //將剛剛的盤點結果丟到SirPosting讓主管審核
        public async void PostData()
        {
            //Get
            using (var client = new HttpClient())
            {
                //X為剛剛員工輸入的盤點單編號
                var x = Num.DocNum; 
                var uri = "http://192.168.0.106:8056/API/api/TEST/GetAtoSir";
                var result = await client.GetStringAsync(uri);
                List<DataInventory> DataList = JsonConvert.DeserializeObject<List<DataInventory>>(result);
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

        //執行盤點
        public async void Posting()
        {
            using (var client = new HttpClient())
            {
                var uri = "http://192.168.0.106:8056/API/api/TEST/GetAPosting";
                var result = await client.GetStringAsync(uri);
            }
        }
    }
}