using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            List<string> BigImageList = new List<string>();
            List<string> SmallImageList = new List<string>();
            //取得頁面的html
            var client = new RestClient(url);
            var request = new RestRequest("", Method.GET);
            IRestResponse response = client.Execute(request);
            string html = response.Content;

            /*
             目前圖床支援清單:
             * imgdream,imgblank,img.yt,dimtus.com,imgstudio
             * damimage,imgseed,55888,imageteam,imagedecode,
             * hentai,imgchili
             */

            //分離出html裡的image Url
            Regex pattern = new Regex(
                //p.s. ?:是關閉括號的capture功能
            @"(?<url>https?://[\d\w-_.]*(?:imgdream|imgblank|img.yt|dimtus|imgstudio|damimage|imgseed|55888|imageteam|imagedecode|hentai|imgchili)(?:[\d\w-_./]*)[\d\w-_.]*.jpe?g)"
            , RegexOptions.Multiline);
            MatchCollection matchGuides = pattern.Matches(html);
            foreach (Match guide in matchGuides)
            {
                SmallImageList.Add(guide.Groups["url"].ToString());
            }

            //判斷能否取到大圖 
            foreach (var strUrl in SmallImageList) { 
                
            }

            return BigImageList;
        }
    }
}
