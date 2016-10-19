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
        public static double FormOpaticy = 0.1;
        //下拉選單的值
        public static Dictionary<string, string>CatalogueDropDownList = new Dictionary<string, string>()
            {
            {"https://www.nyaa.se/?page=rss&cats=1_0","Anime"},
            {"https://www.nyaa.se/?page=rss&cats=3_0","Audio"},
            {"https://sukebei.nyaa.se/?page=rss&cats=8_0","R18-RealLife"},
            
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
