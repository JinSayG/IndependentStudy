using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    public class TESTController : ApiController
    {
        //把GetInventory的編號、倉庫 抓出來 等等交給ANumber使用
        public static string ItemCode, WhsCode;
        public static int number;
        public static List<ANumber> DATA = new List<ANumber>();
        //GetGG 開始(檢查連接資料庫是否成功)
        public IEnumerable<GG> GetGGs()
        {
            int errorCode = 0;
            string errorMessage = "";
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            try
            {
                oCompany.CompanyDB = "cyutdb";
                oCompany.Server = "WIN-IT8HPSMKSJR";
                oCompany.LicenseServer = "WIN-IT8HPSMKSJR";
                oCompany.DbUserName = "sa";
                oCompany.DbPassword = "2ixijklM";
                oCompany.UserName = "manager";
                oCompany.Password = "1234";
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                oCompany.UseTrusted = false;
                int connectionResult = oCompany.Connect();

                if (connectionResult != 0)
                {
                    oCompany.GetLastError(out errorCode, out errorMessage);
                    List<GG> GGData = new List<GG>();
                    GG gg = new GG();
                    gg.Fail = "連接失敗";
                    GGData.Add(gg);
                    return GGData;
                }
                else
                {
                    List<GG> GGData = new List<GG>();
                    GG gg = new GG();
                    gg.Success = "連接成功";
                    GGData.Add(gg);
                    return GGData;
                }
            }
            catch (Exception errMsg)
            {
                throw errMsg;
            }
        }
        //GetGG 結束(檢查連接資料庫是否成功)

        //GetInventory開始(抓盤點單)
        public IEnumerable<Inventory> GetInventory(int x)
        {

            int errorCode = 0;
            string errorMessage = "";
            number = x;
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            try
            {
                oCompany.CompanyDB = "cyutdb";
                oCompany.Server = "WIN-IT8HPSMKSJR";
                oCompany.LicenseServer = "WIN-IT8HPSMKSJR";
                oCompany.DbUserName = "sa";
                oCompany.DbPassword = "2ixijklM";
                oCompany.UserName = "manager";
                oCompany.Password = "1234";
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                oCompany.UseTrusted = false;
                int connectionResult = oCompany.Connect();

                if (connectionResult != 0)
                {
                    oCompany.GetLastError(out errorCode, out errorMessage);
                    List<Inventory> Inventory = new List<Inventory>();
                    Inventory Getdata = new Inventory();
                    Getdata.ItemCode = "連接失敗";

                    Inventory.Add(Getdata);
                    return Inventory;
                }
                else
                {
                    List<Inventory> Inventory = new List<Inventory>();
                    Inventory Getdata = new Inventory();
                    SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordSet.DoQuery("SELECT T0.[DocNum], T1.[ItemCode], T1.[ItemDesc], T1.[WhsCode] FROM OINC T0  INNER JOIN INC1 T1 ON T0.[DocEntry] = T1.[DocEntry] WHERE T0.[DocNum] =" + x);
                    while (oRecordSet.EoF == false)
                    {

                        Getdata.ItemCode = oRecordSet.Fields.Item("ItemCode").Value.ToString();
                        Getdata.ItemDesc = oRecordSet.Fields.Item("ItemDesc").Value.ToString();
                        Getdata.WhsCode = oRecordSet.Fields.Item("WhsCode").Value.ToString();
                        ItemCode = Getdata.ItemCode;
                        WhsCode = Getdata.WhsCode;
                        oRecordSet.MoveNext();
                        Inventory.Add(Getdata);
                    }
                    return Inventory;
                }
            }
            catch (Exception errMsg)
            {
                throw errMsg;
            }
        }
        //GetInventory 結束(抓盤點單

        //ANumber 開始(用盤點單裡的商品編號抓序號)
        public IEnumerable<ANumber> GetANumber()
        {

            int errorCode = 0;
            string errorMessage = "";
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            try
            {
                oCompany.CompanyDB = "cyutdb";
                oCompany.Server = "WIN-IT8HPSMKSJR";
                oCompany.LicenseServer = "WIN-IT8HPSMKSJR";
                oCompany.DbUserName = "sa";
                oCompany.DbPassword = "2ixijklM";
                oCompany.UserName = "manager";
                oCompany.Password = "1234";
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                oCompany.UseTrusted = false;
                int connectionResult = oCompany.Connect();

                if (connectionResult != 0)
                {
                    oCompany.GetLastError(out errorCode, out errorMessage);
                    List<ANumber> ANumber = new List<ANumber>();
                    ANumber Getdata = new ANumber();
                    Getdata.ItemCode = "連接失敗";

                    ANumber.Add(Getdata);
                    return ANumber;
                }
                else
                {
                    List<ANumber> ANumber = new List<ANumber>();
                    SAPbobsCOM.Recordset oRecordSet = (SAPbobsCOM.Recordset)oCompany.GetBusinessObject(SAPbobsCOM.BoObjectTypes.BoRecordset);
                    oRecordSet.DoQuery("SELECT T1.[ItemCode], T0.[DistNumber] FROM OSRN T0  INNER JOIN OSRQ T1 ON T0.[AbsEntry] = T1.[MdAbsEntry] WHERE T0.[ItemCode] ='" + ItemCode + "'and T1.[WhsCode] ='" + WhsCode + "'and T1.[Quantity] = 1");
                    while (oRecordSet.EoF == false)
                    {
                        ANumber Getdata = new ANumber();
                        Getdata.ItemCode = oRecordSet.Fields.Item("ItemCode").Value.ToString();
                        Getdata.DistNumber = oRecordSet.Fields.Item("DistNumber").Value.ToString();
                        Getdata.Whether = "RedCross.png";
                        oRecordSet.MoveNext();
                        ANumber.Add(Getdata);
                    }
                    return ANumber;
                }
            }
            catch (Exception errMsg)
            {
                throw errMsg;
            }
        }
        //ANumber 結束(用盤點單裡的商品編號抓序號)

        //GetA 開始(把剛剛APP盤點的資料回傳API)
        public string GetA(string ItemCode, string DistNumber, string Whether)
        { 
            ANumber Postdata = new ANumber();
            Postdata.ItemCode = ItemCode;
            Postdata.DistNumber = DistNumber;
            Postdata.Whether = Whether;
            DATA.Add(Postdata); 
            return "成功";
        }
        //GetA(把剛剛APP盤點的資料回傳API)

        //GetAtoSir 開始(回傳GetA資料給主管看)
        public IEnumerable<ANumber> GetAtoSir()
        {
            List<ANumber> Sir = new List <ANumber>();
            foreach(var evm in DATA)
            {
                ANumber GetData = new ANumber();
                GetData.ItemCode = evm.ItemCode;
                GetData.DistNumber = evm.DistNumber;
                GetData.Whether = evm.Whether;
                Sir.Add(GetData);
            }
            return Sir;
           
        }
        //GetAtoSir 結束(回傳GetA資料給主管看)


        //GetDATAClear(開始)將DATA清除 測試會用到 成果不會用到
        public string GetDATAClear()
        {
            DATA.Clear();
            return "清除";
        }
        //GetDATAClear(結束)將DATA清除 測試會用到 成果不會用到

        //GetAPosting(開始)把GetA的資料丟入SAP進行過帳
        public IEnumerable<GG> GetAPosting()
        {

            int errorCode = 0;
            string errorMessage = "";
            SAPbobsCOM.Company oCompany = new SAPbobsCOM.Company();
            try
            {
                oCompany.CompanyDB = "cyutdb";
                oCompany.Server = "WIN-IT8HPSMKSJR";
                oCompany.LicenseServer = "WIN-IT8HPSMKSJR";
                oCompany.DbUserName = "sa";
                oCompany.DbPassword = "2ixijklM";
                oCompany.UserName = "manager";
                oCompany.Password = "1234";
                oCompany.DbServerType = SAPbobsCOM.BoDataServerTypes.dst_MSSQL2014;
                oCompany.UseTrusted = false;
                int connectionResult = oCompany.Connect();

                if (connectionResult != 0)
                {
                    oCompany.GetLastError(out errorCode, out errorMessage);
                    List<GG> GGData = new List<GG>();
                    GG gg = new GG();
                    gg.Fail = "失敗";
                    GGData.Add(gg);
                    return GGData;
                }
                else
                {
                    //把DATA整理，只要把GreenTick的丟回即可
                    List<ANumber> PostingData = new List<ANumber>();
                    ANumber ANumber = new ANumber();
                    foreach (var Peko in DATA.Where(w=>w.Whether == "GreenTick.png"))
                    {
                        ANumber.ItemCode = Peko.ItemCode;
                        ANumber.DistNumber = Peko.DistNumber;
                        ANumber.Whether = Peko.Whether;
                        PostingData.Add(ANumber);
                    }

                    SAPbobsCOM.CompanyService oCS = oCompany.GetCompanyService();
                    SAPbobsCOM.InventoryCountingsService oICS = (SAPbobsCOM.InventoryCountingsService)oCS.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryCountingsService);
                    SAPbobsCOM.InventoryCountingParams oICP = (SAPbobsCOM.InventoryCountingParams)oICS.GetDataInterface(SAPbobsCOM.InventoryCountingsServiceDataInterfaces.icsInventoryCountingParams);
                    oICP.DocumentEntry = number;
                    SAPbobsCOM.InventoryCounting oIC = oICS.Get(oICP) as SAPbobsCOM.InventoryCounting;
                    SAPbobsCOM.InventoryCountingLine line = oIC.InventoryCountingLines.Item(0);
                    line.CountedQuantity = DATA.Count();
                    line.Counted = SAPbobsCOM.BoYesNoEnum.tYES;
                    oICS.Update(oIC);
                    //過帳
                    if(line.InWarehouseQuantity==DATA.Count)
                    {
                    oICS.Close(oICP);
                    }
                    else
                    {
                        SAPbobsCOM.InventoryPostingsService oIPS = oCS.GetBusinessService(SAPbobsCOM.ServiceTypes.InventoryPostingsService);
                        SAPbobsCOM.InventoryPosting oIP = oIPS.GetDataInterface(SAPbobsCOM.InventoryPostingsServiceDataInterfaces.ipsInventoryPosting);
                        oIP.CountDate = DateTime.Now;
                        SAPbobsCOM.InventoryPostingLines oIPLS = oIP.InventoryPostingLines;
                        SAPbobsCOM.InventoryPostingLine oIPL = oIPLS.Add();
                        oIPL.BaseEntry = oICP.DocumentEntry;
                        oIPL.BaseLine = 1;
                        SAPbobsCOM.InventoryPostingSerialNumber oInventoryPostingSerialNumber;
                        foreach (var item in PostingData)
                        {
                            oInventoryPostingSerialNumber = oIPL.InventoryPostingSerialNumbers.Add();
                            oInventoryPostingSerialNumber.InternalSerialNumber = item.DistNumber;
                        }
                        SAPbobsCOM.InventoryPostingParams oInventoryPostingParams = oIPS.Add(oIP);
                        DATA.Clear();
                        PostingData.Clear();

                    }
                    List<GG> GGData = new List<GG>();
                    GG gg = new GG();
                    gg.Success = "成功";
                    GGData.Add(gg);
                    return GGData;

                }
            }
            catch (Exception errMsg)
            {
                DATA.Clear();
                throw errMsg;
            }

        }
        //GetAPosting(結束)把GetA的資料丟入SAP進行過帳

    }
}