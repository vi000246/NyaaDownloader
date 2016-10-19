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

            //只取出html裡描述的部份 其他去掉
            Regex pattern = new Regex(
            @"<div\sclass=""viewdescription"">(?<content>.*)<h3>Files\sin\storrent\:</h3>"
            , RegexOptions.Multiline);
            Match content = pattern.Match(html);
            html = content.Groups["content"].Value;

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

            #region 取出html裡全部的圖片網址 
            Regex ptAllUrl = new Regex(
                //p.s. ?:是關閉括號的capture功能
            @"(?<url>(?:\w+):\/\/(?<domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*)"
            , RegexOptions.Multiline);
            MatchCollection matchAllUrl = ptAllUrl.Matches(html);
            #endregion

            #region 判斷Match到的url是哪個圖床 判斷成功呼叫取大圖method後加進BigImageList
            foreach (Match guide in matchAllUrl)
            {
                //網址
                string url=guide.Groups["url"].ToString();
                //domain
                string domain = guide.Groups["domain"].ToString();

               /* 
                * 從dictionary裡尋找相符於這個domain的委派 如果找到的話就呼叫他
                * 例如domain="pics.dmm.co.jp" 就能找到dictonary裡key=dmm的value 
                * 這個value就是委派的方法
                *
                */
                var FuncGetBigImage = DicFuncGetbigImage
                .FirstOrDefault(x => domain.Contains(x.Key))
                .Value;
                //如果有找到dictonary對應的方法 就呼叫它 並加到BigImageList
                if (FuncGetBigImage != null)
                {
                    string bigImageUrl = FuncGetBigImage(url);
                    //必須是圖片才能加到list
                    if (Regex.IsMatch(bigImageUrl,@"(?:\w+):\/\/(?<domain>[\w@][\w.:@]+)\/?[\w\.?=%&=\-@/$,]*.jpe?g"))
                        BigImageList.Add(bigImageUrl);
                }


            }
            #endregion
            return BigImageList;
        }

        #region 取得大圖的各圖床委派
        /// <summary>
        /// 儲存各圖床需要叫用的方法 輸入網址會回傳大圖網址
        /// 用法: string newUrl=functions["imgdream"]("http://www.test.com.tw");
        /// </summary>
        Dictionary<string, Func<string, string>> DicFuncGetbigImage =
            new Dictionary<string, Func<string, string>>
        {
            //去除_thumb
            { "imgdream", Url_deleteThumb },
            //small改big
            {"imgblank",Url_changeSmallToBig},
            {"img.yt",Url_changeSmallToBig},
            {"dimtus",Url_changeSmallToBig},
            {"imgstudio",Url_changeSmallToBig},
            {"damimage",Url_changeSmallToBig},
            {"imgseed",Url_changeSmallToBig},
            {"55888",Url_changeSmallToBig},
            {"imageteam",Url_changeSmallToBig},
            {"imagedecode",Url_changeSmallToBig},
            {"hentai",Url_changeSmallToBig},
            //直接回傳
            {"pics.dmm",Url_Direct},
            {"tinypic",Url_Direct},
            //replace特定字串
            {"imgchili",Url_imgchili},
            {"ultraimg",Url_ultraimg},
            {"1dl.biz",Url_biz}
        };

        //移除_thumb
        private static string Url_deleteThumb(string url)
        {
            return url.Replace("_thumb", "");
        }
        //改成big
        private static string Url_changeSmallToBig(string url) {
            return url.Replace("small","big");
        }
        //直接回傳
        private static string Url_Direct(string url)
        {
            return url;
        }
        //imgchili
        private static string Url_imgchili(string url)
        {
            //將t10、t6、t100之類的字串換成i10、i6、i100
            return Regex.Replace(url, @"t(\d{0,3})\.", m => "i" + m.Groups[1].Value + ".");
        }
        //ultraimg
        private static string Url_ultraimg(string url)
        {
            return url.Replace(".md","");
        }
        //biz
        private static string Url_biz(string url)
        {
            return url.Replace(".php?", "/") + ".jpg";
        }

        #endregion



    }
}
