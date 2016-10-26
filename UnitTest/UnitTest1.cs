using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NyaaRSSreader;
using System.Text.RegularExpressions;
using Jint;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        //判斷能不能回傳大圖
        public void TestBigimage()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://jav-hentai.host/i/upload/small/2016/10/21/580a1752537c8.jpeg");
            Assert.IsTrue(urlList.Count > 0);
            //foreach (var url in urlList)
            //{
            //    Assert.IsTrue(Regex.IsMatch(url, @"(?<url>https?://1dl.biz(?:[\d\w-_./\?]*)[\d\w-_.]*.jpe?g)", RegexOptions.Singleline));
            //}
        }

        [TestMethod]
        //判斷能不能回傳1dl.biz的大圖
        public void TestBizLink()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://1dl.biz/i.php?b/161016080626");
            Assert.IsTrue(urlList.Count>0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"(?<url>https?://1dl.biz(?:[\d\w-_./\?]*)[\d\w-_.]*.jpe?g)", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷imgchili能不能傳回大圖
        public void TestImgchili() {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://t10.imgchili.net/90670/90670491_nktv_007.jpg");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"(?<url>https?://i\d{0,3}.imgchili.net(?:[\d\w-_./\?]*)[\d\w-_.]*.jpe?g)", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷imgdream能不能傳回大圖
        public void Testimgdream()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://imgdream.net/images/21933295703414693413_thumb.jpg");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"(?<url>http://imgdream.net/images/\d+.jpg)", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷imgbabes和imgflare能不能傳回大圖
        public void TestImgbabesAndImgFlare()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://www.imgbabes.com/b4y95hmejbou/102216_01-10mu-1080p.jpeg.html");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://\w+.(imgflare|imgbabes).com/files/[\w/-]+.jpg", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷imagebam能不能傳回大圖
        public void Testimagebam()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://www.imagebam.com/image/9ad016275355093");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://[\d\w]+.imagebam.com/download/[\w/_-]+[\w\d\w_]+.jpg", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷imgrock能不能傳回大圖
        public void TestImgrock()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://imgrock.net/2uegtq384m9i/JUX-999.jpg.html");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://[\d\w]+.imgrock.net/img/\w+/[\w-]+.jpe?g", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷pixsense能不能傳回大圖
        public void TestPixsense()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://www.pixsense.net/site/v/650903#173&650903");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://[\d\w]+.pixsense.net/[\w/-]+.jpe?g", RegexOptions.Singleline));
            }
        }


        [TestMethod]
        //測試抓不到預覽圖時的事件 1.彈出提示訊息 2.不顯示訊息 3.直接在瀏覽器開啟
        public void TestImageNotFindWindow() {
            new Nyaa抓檔神器().ImageNotFindBehavior("http://www.google.com.tw/","na");
        }



        [TestMethod]
        public void TestJint() {
            //Engine jint;
            //jint = new Engine();
            //jint.Execute(JShelper.Script);
            //var a = jint.Invoke("GetCookie", "2c74fc8f6cbd3aac4dbd79d854eee1b0",
            //    "5907dbd743bae6749df54fc54f81e447",
            //    "7d74f260ffbe6844f2c77cba7446350c");
            //Assert.IsNotNull(a);
        }


    }
}
