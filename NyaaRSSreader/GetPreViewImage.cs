﻿using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Jint;
using System.Web;

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
            string html = string.Empty;
            try
            {
                //取得頁面的html
                var client = new RestClient(url);
                var request = new RestRequest("", Method.GET);
                IRestResponse response = client.Execute(request);
                html = response.Content;

                //只取出html裡描述的部份 其他去掉
                Regex pattern = new Regex(
                @"<div\sclass=""panel-body""\sid=""torrent-description"">(?<content>.*)<div\sclass=""panel\spanel-default"">"
                , RegexOptions.Singleline);
                Match content = pattern.Match(html);
                html = content.Groups["content"].Value;

            }
            catch (Exception ex) {
                throw ex;
            }
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

            #region 取出html裡全部網址 
            Regex ptAllUrl = new Regex(
                //p.s. ?:是關閉括號的capture功能
            @"(?<url>(?:\w+):\/\/(?<domain>[\w@][\w.:@-]+)\/?[\w\.?=%&=\-@/$,]*)"
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
                    //下面這種的也不給過
                    //htip://imgdream.net/viewer.php?file=26132226521392290432.jpg
                    //例外:http://dare.moe/f/4qy   可以直接取得圖片
                    if (Regex.IsMatch(bigImageUrl,@"(?:\w+):\/\/(?<domain>[\w@][\w.:@]+)\/?[\w\.=%&=\-@/$,]*.jpe?g")
                        || Regex.IsMatch(bigImageUrl, @"http://dare.moe/f/\w{0,5}"))
                        BigImageList.Add(bigImageUrl);
                }


            }
            #endregion
            return BigImageList;
        }

        #region 字典 取得大圖的各圖床委派
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
            {"dimtus",Url_changeSmallToBig},
            {"imgseed",Url_changeSmallToBig},
            {"55888",Url_changeSmallToBig},
            {"hentai",Url_changeSmallToBig},
            //直接回傳
            {"pics.dmm",Url_Direct},
            {"tinypic",Url_Direct},
            {"tokyo-hot",Url_Direct},
            {"cosjav",Url_Direct},
            {"dare",Url_Direct},
            {"qpic",Url_Direct},
            //replace特定字串
            {"imgchili",Url_imgchili},
            {"ultraimg",Url_ultraimg},
            {"1dl.biz",Url_biz},
            //imgbabes  and imgflare
            {"imgbabes",Url_ImgbabesAndImgflare},
            {"imgflare",Url_ImgbabesAndImgflare},
            //imagebam
            {"imagebam",Url_imagebam},
            //pixsense
            {"pixsense",Url_pixsense},
            //imgseed 有驗證碼 無法破解
            //{"imgseed",Url_imgseed}

            //大圖網址像 upload/big/2016/10/25/580f7084e76dc.jpg 的網站專用
            {"imgleveret",Url_UploadBig},
            {"imagedecode",Url_UploadBig},
            {"porn84",Url_UploadBig},
            {"imageteam",Url_UploadBig},
            {"imgstudio",Url_UploadBig},
            {"damimage",Url_UploadBig},
            //imgtrex imagetwist專用 只需要get一次就能取到大圖
            {"imgtrex",Url_OneGetRequest},
            {"imagetwist",Url_OneGetRequest},
            //javtotal專用
            {"javtotal",Url_javtotal},
            //擋continue to image 和input hidden value
            {"imgview",Url_continue},
            {"imgrock",Url_continue},
            {"imgtown",Url_continue},
            //擋continue to image
            {"imgcandy",Url_OneContinue},
            {"img.yt",Url_OneContinue},
            {"idlelive",Url_OneContinue},
            {"imgbb",Url_OneContinue}
        };
        #endregion


        //移除_thumb
        private static string Url_deleteThumb(string url)
        {
            return url.Replace("_thumb", "").Replace("viewer.php?file=", "images/");
        }
        //small改成big
        private static string Url_changeSmallToBig(string url) {
            return url.Replace("small","big");
        }
        //直接回傳
        private static string Url_Direct(string url)
        {
            return url;
        }
        #region imgchili專用
        //imgchili專用
        private static string Url_imgchili(string url)
        {
            string BigImageUrl = string.Empty;

            if (url.IndexOf("show") > -1)
            {
                var client = new RestClient(url);
                var request = new RestRequest("", Method.GET);
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;

                Regex ptAllUrl = new Regex(
                @"(?<url>https?://i\d{0,3}.imgchili.net(?:[\d\w-_./\?]*)[\d\w-_.]*.jpe?g)"
                , RegexOptions.Multiline);
                BigImageUrl = ptAllUrl.Match(html).Groups["url"].Value;
            }
            else
            {
                //將t10、t6、t100之類的字串換成i10、i6、i100
                BigImageUrl = Regex.Replace(url, @"t(\d{0,3})\.", m => "i" + m.Groups[1].Value + ".");
            }
            return BigImageUrl;
        }
        #endregion
        //ultraimg專用
        private static string Url_ultraimg(string url)
        {
            return url.Replace(".md","");
        }
        //biz專用
        private static string Url_biz(string url)
        {
            return url.Replace(".php?", "/") + ".jpg";
        }
        #region imagebam專用
        //imagebam專用
        private static string Url_imagebam(string url)
        {
            string BigImageUrl = string.Empty;
            //需要同意瀏覽18禁連結的cookie 無解

            //如果是連結網址就進行request 縮圖網址就忽略
            if (Regex.IsMatch(url, @"^http://\w+.imagebam.com/[\w/]+[\w\d]+$"))
            {
                var client = new RestClient(url);
                var request = new RestRequest("", Method.GET);
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;

                Regex ptAllUrl = new Regex(
                @"(?<url>http://[\d\w]+.imagebam.com/download/[\w/_-]+[\w\d\w_]+.jpg)"
                , RegexOptions.Multiline);
                BigImageUrl = ptAllUrl.Match(html).Groups["url"].Value;


            }

            return BigImageUrl;
        }
        #endregion

        #region 有擋Continue to Image的網站 imgcandy 、img.yt、idlelive、imgbb
        private static string Url_OneContinue(string url)
        {
            string BigImageUrl = string.Empty;

            //如果是idlelive 要先Get request一次 取得真正要連結的網址
            if (Regex.IsMatch(url, @"^http://idlelive.com/[\w/_-]+$"))
            {
                 var client = new RestClient(url);
                //這是Post
                var request = new RestRequest("", Method.GET);
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;
                Regex ptAllUrl = new Regex(
              @"(?<url>http://thumbnailus.com/[\w-]+.html)"
              , RegexOptions.Multiline);
                //重設url為正確的url
                url = ptAllUrl.Match(html).Groups["url"].Value;
            }

            //如果是連結網址就進行request 縮圖網址就忽略
            if (Regex.IsMatch(url, @"https?://(imgcandy|img|thumbnailus|imgbb).(net|yt|com)/[\w/_-]+(.jpe?g)?(.html)?"))
            {
                var client = new RestClient(url);
                //這是Post
                var request = new RestRequest("", Method.POST);
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
               
                //每家網站要附加的cookie字串都不一樣 用switch case選擇
                string[] keys = new string[] { "thumbnailus", "imgcandy","img.yt","imgbb" };

                string sKeyResult = keys.FirstOrDefault<string>(s => url.Contains(s));
                string bodyParameter = string.Empty;
                switch (sKeyResult)
                {
                    case "thumbnailus":
                        bodyParameter =  "Continue to View Screen image ... ";
                        break;
                    case "imgcandy":
                    case "img.yt":
                        bodyParameter = "Continue to your image";
                        break;
                    case "imgbb":
                        bodyParameter = "Continue to image ...";
                        break;
                }

                request.AddParameter("imgContinue", bodyParameter);
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;

                Regex ptAllUrl = new Regex(
                @"(?<url>https?://(imgcandy|img|thumbnailus|imgbb).(net|yt|com)/upload/big/[\w-/_#&]+.jpe?g)"
                , RegexOptions.Multiline);
                BigImageUrl = ptAllUrl.Match(html).Groups["url"].Value;
            }
            else {
                BigImageUrl = Url_changeSmallToBig(url);
            }

            return BigImageUrl;
        }
        #endregion


        #region pixsense專用
        //pixsense專用
        private static string Url_pixsense(string url)
        {
            string BigImageUrl = string.Empty;


            //如果是連結網址就進行request 縮圖網址就忽略
            if (Regex.IsMatch(url, @"^http://\w+.pixsense.net/[\w/#&]+$"))
            {
                var client = new RestClient(url);
                var request = new RestRequest("", Method.GET);
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;

                Regex ptAllUrl = new Regex(
                @"(?<url>http://[\d\w]+.pixsense.net/themes/[\w-/_#&]+.jpe?g)"
                , RegexOptions.Multiline);
                BigImageUrl = ptAllUrl.Match(html).Groups["url"].Value;


            }

            return BigImageUrl;
        }
        #endregion

        #region 大圖網址像 upload/big/2016/10/25/580f7084e76dc.jpg 的網站專用
        //imgleveret Imagedecode專用
        private static string Url_UploadBig(string url)
        {
            string BigImageUrl = string.Empty;

            //如果是連結網址就進行request 縮圖網址就忽略
            if (Regex.IsMatch(url, @"^http://[\w\.]*(imgleveret|imagedecode|porn84|imageteam|imgstudio|damimage).(com|org)/[\w/#&-]+.html$"))
            {
                var client = new RestClient(url);
                var request = new RestRequest("", Method.GET);
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;

                Regex ptAllUrl = new Regex(
                @"(?<url>http://[\w\.]*(imgleveret|imagedecode|porn84|imageteam|imgstudio|damimage).(com|org)/upload/big/[\w-/_#&]+.jpe?g)"
                , RegexOptions.Multiline);
                BigImageUrl = ptAllUrl.Match(html).Groups["url"].Value;


            }
            //如果傳來的是小圖的網址 
            else if (Regex.IsMatch(url, @".*jpe?g"))
            {
                BigImageUrl = Url_changeSmallToBig(url);
            }

            return BigImageUrl;
        }
        #endregion

        #region imgtrex和imagetwist專用
        //imgtrex imagetwist專用
        private static string Url_OneGetRequest(string url)
        {
            string BigImageUrl = string.Empty;

            //如果是連結網址就進行request 縮圖網址就忽略
            if (Regex.IsMatch(url, @"^http://(imgtrex|imagetwist).com/[\w/-]+.jpe?g$"))
            {
                var client = new RestClient(url);
                var request = new RestRequest("", Method.GET);
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;

                Regex ptAllUrl = new Regex(
                @"(?<url>http://\w+.(imgtrex|imagetwist).com/i/[\w/]+.jpe?g)"
                , RegexOptions.Multiline);
                BigImageUrl = ptAllUrl.Match(html).Groups["url"].Value;


            }
            //如果傳來的是小圖的網址 
            else if (Regex.IsMatch(url, @".*jpe?g"))
            {
                BigImageUrl = Url_changeSmallToBig(url);
            }

            return BigImageUrl;
        }
        #endregion

        #region imagetotal專用
        //imagetotal專用
        private static string Url_javtotal(string url)
        {
            string BigImageUrl = string.Empty;

            //如果是連結網址就進行request 縮圖網址就忽略
            if (Regex.IsMatch(url, @"^http://cdn.javtotal.com/img/[\w/-]+$"))
            {
                var client = new RestClient(url);
                var request = new RestRequest("", Method.GET);
                request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");
                IRestResponse response = client.Execute(request);
                //這是回傳的html
                string html = response.Content;

                //因為這網站回傳的html沒有host地址 所以要自已加上去
                Regex ptAllUrl = new Regex(
                @"src=""(?<url>images/\w+.jpe?g)"""
                , RegexOptions.Multiline);
                BigImageUrl = "http://cdn.javtotal.com/"+ptAllUrl.Match(html).Groups["url"].Value;
            }
            //如果傳來的是小圖的網址 
            else if (Regex.IsMatch(url, @".*jpe?g"))
            {
                BigImageUrl = Url_changeSmallToBig(url);
            }

            return BigImageUrl;
        }
        #endregion

        #region imgbabes和imgflare專用
        //imgbabes和imgflare專用
        private static string Url_ImgbabesAndImgflare(string url)
        {
            string BigImageUrl=string.Empty;
            try
            {
                //如果是連結網址就進行request 縮圖網址就忽略
                if (Regex.IsMatch(url, @"^http://\w+.(?:imgflare|imgbabes).com/(?:\w+/)+[\w\d\W]+.jpe?g.html$"))
                {
                    //============step1 先取得網頁頁面
                    var client = new RestClient(url);
                    var request = new RestRequest("", Method.GET);
                    request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/44.0.2403.157 Safari/537.36");

                    IRestResponse response = client.Execute(request);
                    //這是回傳的html
                    string html = response.Content;

                    //=============step2 取得html裡 slowAES的tonumber的三個key 並呼叫Jint還原為cookie的字串

                    Regex matchAES = new Regex(
    @"a=toNumbers\(""(?<input>\w+)""\).*b=toNumbers\(""(?<key>\w+)""\).*c=toNumbers\(""(?<iv>\w+)""\)"
    , RegexOptions.Multiline);
                    Match matchAesGroup = matchAES.Match(html);
                    string input = matchAesGroup.Groups["input"].Value;
                    string key = matchAesGroup.Groups["key"].Value;
                    string iv = matchAesGroup.Groups["iv"].Value;

                    Engine jint = new Engine();
                    jint.Execute(JShelper.Script);
                    string cookie = jint.Invoke("GetCookie", input, key, iv).ToString();

                    //============step3 加上cookie
                    //p.s. 加上同意18禁連結的cookie  這cookie是我手動複製來的 好像是動態組出來 反正過了 沒差
                    //這是imgbabes的cookie
                    request.AddParameter("denial", cookie, ParameterType.Cookie);
                    //這是imgflare的cookie
                    request.AddParameter("verifid", cookie, ParameterType.Cookie);

                    IRestResponse response2 = client.Execute(request);
                    //這是擁有大圖的html
                    string html2 = response2.Content;

                    Regex ptAllUrl = new Regex(
                    @"(?<url>http://\w+.(imgflare|imgbabes).com/files/(?:[\w-]+/?)+.jpe?g)"
                    , RegexOptions.Multiline);
                    BigImageUrl = ptAllUrl.Match(html2).Groups["url"].Value;


                }
            }
            catch (Exception ex) {
                throw ex;
            }

            return BigImageUrl;
        }
        #endregion

        #region  有擋Continue to Image的網站(先轉頁到.php 再取hiden value :pre、op、id 再取到大圖)
        // 有擋Continue to Image的網站
        private static string Url_continue(string url)
        {
            string BigImageUrl = string.Empty;
            try
            {
                Regex matchUrl = new Regex(
                @"^http://\w*.?(?:imgrock|imgview|imgtown).net/(?<file_code>\w+)/[\w\d\W]+.jpe?g.html$"
                , RegexOptions.Multiline);
                Match matchUrlGroup = matchUrl.Match(url);

                //取得fileCode 用來加在cookie 用在step2的Get request
                string fileCode = matchUrlGroup.Groups["file_code"].Value;

                //如果是連結網址就進行request 縮圖網址就忽略
                if (matchUrlGroup.Success)
                {
                    //============step1 先取得網頁頁面
                    var client = new RestClient(url);
                    var request = new RestRequest("", Method.GET);

                    IRestResponse response = client.Execute(request);
                    //這是回傳的html
                    string html = response.Content;

                    //=============step2 取得html裡 .php結尾的連結 需要附加file_code的cookie
                    //才會回傳必須的hidden value
                    Regex matchRealUrl = new Regex(
                   @"(?<link>http://(imgrock|imgview|imgtown).net/\w+.php)"
                   , RegexOptions.Multiline);
                    string RealLink = matchRealUrl.Match(html).Groups["link"].Value;
                    var clientRealLink = new RestClient(RealLink);
                    var requestRealLink = new RestRequest("", Method.GET);

                    requestRealLink.AddParameter("file_code", fileCode, ParameterType.Cookie);
                    IRestResponse responseRealLink = clientRealLink.Execute(requestRealLink);
                    //這是回傳的html
                    string html3 = responseRealLink.Content;


                    //=============step3 取得隱藏欄位的值組成cookie

                    Regex matchHiddenValue = new Regex(
    @"<input\stype=""hidden""\sname=""(?<hidden>\w{10,})""\svalue=""1"">"
    , RegexOptions.Multiline);
                    Match matchImgRockGroup = matchHiddenValue.Match(html3);
                    string hiddenValue = matchImgRockGroup.Groups["hidden"].Value;

                    var clientBigImg = new RestClient(RealLink);
                    var requestBigImg = new RestRequest("", Method.POST);
                    requestBigImg.AddHeader("content-type", "application/x-www-form-urlencoded");

                    //============step4 加上Body 再Post request .php

                    requestBigImg.AddParameter("op", "view");
                    requestBigImg.AddParameter("id", fileCode);
                    requestBigImg.AddParameter("pre", "1");
                    requestBigImg.AddParameter(hiddenValue, "1");
                    IRestResponse responseBigImage = clientBigImg.Execute(requestBigImg);
                    //這是回傳的html
                    string html4 = responseBigImage.Content;

                    Regex ptAllUrl = new Regex(
                    @"(?<url>http://\w+.(imgrock|imgview|imgtown).net/img/(?:[\w-]+/?)+.jpe?g)"
                    , RegexOptions.Multiline);
                    BigImageUrl = ptAllUrl.Match(html4).Groups["url"].Value;


                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return BigImageUrl;
        }
        #endregion







    }
}
