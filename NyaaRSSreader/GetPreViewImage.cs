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
        /// 傳入網址 得到網頁的html 並回傳大圖的url list
        /// </summary>
        /// <param name="url"></param>
        public List<string> CallImageHanderdle(string url) 
        {
            //取得頁面的html
            var client = new RestClient(url);
            var request = new RestRequest("", Method.GET);
            IRestResponse response = client.Execute(request);
            string html = response.Content;

            return GetBigImageUrl(html);
        }

        /// <summary>
        /// 傳入html 解析出圖片網址
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public List<string> GetBigImageUrl(string html) {


            List<string> BigImageList = new List<string>();
            List<string> SmallImageList = new List<string>();

            //只取出html裡描述的部份 其他去掉


            #region 圖床判斷1 small換big 刪掉_thumb
            /*
             * 圖床清單:
             * imgdream,imgblank,img.yt,dimtus.com,imgstudio
             * damimage,imgseed,55888,imageteam,imagedecode,
             * hentai,imgchili,
             * 以下兩個是直接開圖就好
             * tinypic,pics.dmm.co.jp
             * 
             * 邏輯:
             * 以上的圖床，將small或_thumb替換掉就能顯示大圖
             */

            //分離出html裡的image Url
            Regex pattern = new Regex(
                //p.s. ?:是關閉括號的capture功能
            @"(?<url>https?://[\d\w-_.]*(?:imgdream|imgblank|img.yt|dimtus|imgstudio|damimage|imgseed|55888|imageteam|imagedecode|hentai|tinypic|pics.dmm)(?:[\d\w-_./]*)[\d\w-_.]*.jpe?g)"
            , RegexOptions.Multiline);
            MatchCollection matchGuides = pattern.Matches(html);
            foreach (Match guide in matchGuides)
            {
                SmallImageList.Add(guide.Groups["url"].ToString());
            }

            //判斷能否取到大圖 
            foreach (var strUrl in SmallImageList)
            {
                string newUrl = strUrl.Replace("small", "big").Replace("_thumb", "");
                BigImageList.Add(newUrl);
            }
            #endregion

            #region 判斷imgchili
            SmallImageList.Clear();
            Regex pattern3 = new Regex(
                //p.s. ?:是關閉括號的capture功能
            @"(?<url>https?://[\d\w-_.]*(?:imgchili)(?:[\d\w-_./]*)[\d\w-_.]*.jpe?g)"
            , RegexOptions.Multiline);
            MatchCollection matchGuides3 = pattern3.Matches(html);
            foreach (Match guide in matchGuides3)
            {
                SmallImageList.Add(guide.Groups["url"].ToString());
            }

            //判斷能否取到大圖 
            foreach (var strUrl in SmallImageList)
            {
                //將t10、t6、t100之類的字串換成i10、i6、i100
                string newUrl = Regex.Replace(strUrl, @"t(\d{0,3})\.", m => "i" + m.Groups[1].Value);
                BigImageList.Add(newUrl);
            }
            #endregion

            #region 判斷1dl.biz
            SmallImageList.Clear();
            Regex pattern2 = new Regex(
                //p.s. ?:是關閉括號的capture功能
            @"(?<url>https?://1dl.biz(?:[\d\w-_./\?]*)[\d\w-_.]*)"
            , RegexOptions.Multiline);
            MatchCollection matchGuides2 = pattern2.Matches(html);
            foreach (Match guide in matchGuides2)
            {
                SmallImageList.Add(guide.Groups["url"].ToString());
            }

            //判斷能否取到大圖 
            foreach (var strUrl in SmallImageList)
            {
                string newUrl = strUrl.Replace(".php?", "/") + ".jpg";
                BigImageList.Add(newUrl);
            }
            #endregion

            return BigImageList;
        }
    }
}
