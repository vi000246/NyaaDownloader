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

        //是否開啟預覽圖彈出視窗功能
        public static bool IsEnablePopup = true;

        //下拉選單的值 RSS分類
        public static Dictionary<string, string>CatalogueDropDownList = new Dictionary<string, string>()
            {
            {"https://sukebei.nyaa.si/?page=rss&c=1_1&f=0","Anime"},
            {"https://sukebei.nyaa.si/?page=rss&c=2_2&f=0","RealLife Videos"},
            {"https://sukebei.nyaa.si/?page=rss&c=2_0&f=0","RealLife"},
            
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

            //彈出視窗最小寬度
            public static int popWindowMinWidth = 1000;
        }
    }
}
