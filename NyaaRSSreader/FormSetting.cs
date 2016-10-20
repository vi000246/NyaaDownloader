using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaaRSSreader
{
    public static class FormSetting
    {
        //主視窗透明度
        public static double FormOpaticy = 1;
        //下拉選單的值 RSS分類
        public static Dictionary<string, string>CatalogueDropDownList = new Dictionary<string, string>()
            {
            {"https://www.nyaa.se/?page=rss&cats=1_0","Anime"},
            {"https://www.nyaa.se/?page=rss&cats=3_0","Audio"},
            {"https://sukebei.nyaa.se/?page=rss&cats=8_0","R18-RealLife"},
            
            };

        //下拉選單的值 dataGrid排序
        public static Dictionary<string, string> SortDropDownList = new Dictionary<string, string>()
            {
            {"無","無"},
            {"title","標題 ↓"},
            {"download","下載數量 ↓"},
            {"size","檔案大小 ↓"},
            {"Seeder","Seeder ↓"},
            {"Leecher","Leecher ↓"},
            {"Date","日期 ↓"},
            };


        public static class PopupWindow 
        {
            //下載按鈕寬度
            public static int btnDownloadWidth = 150;
            //下載按鈕高度
            public static int btnDownloadHeight = 35;
        }
    }
}
