using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaaRSSreader
{
    public class GetPreViewImage
    {
        /// <summary>
        /// 依據圖片的網址 選擇要處理的method
        /// </summary>
        /// <param name="url"></param>
        public List<string> CallImageHanderdle(string url) 
        {
            List<string> imageList = new List<string>();
            //取得頁面的html
            var client = new RestClient(url);
            var request = new RestRequest("", Method.GET);
            IRestResponse response = client.Execute(request);
            string html = response.Content;

            //分離出html裡的image Url

            //判斷能否取到大圖 

            return imageList;
        }
    }
}
