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

            #region 圖床判斷1 small換big 刪掉_thumb
            /*
             * 圖床清單:
             * imgdream,imgblank,img.yt,dimtus.com,imgstudio
             * damimage,imgseed,55888,imageteam,imagedecode,
             * hentai,imgchili
             * 
             * 邏輯:
             * 以上的圖床，將small或_thumb替換掉就能顯示大圖
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
                string newUrl = strUrl.Replace("small","big").Replace("_thumb","");
                BigImageList.Add(newUrl);
            }
            #endregion

            return BigImageList;
        }
    }
}
