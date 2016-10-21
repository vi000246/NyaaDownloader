using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NyaaRSSreader;
using System.Text.RegularExpressions;


namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
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
        public void TestimgbabesAndImgFlare()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://www.imgbabes.com/r4dq19sff9dp/thumbs20140317153605.jpg.html  http://www.imgflare.com/lg4ssabtlplz/1fset00466jp-2.jpg.html");
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
        //測試抓不到預覽圖時的事件 1.彈出提示訊息 2.不顯示訊息 3.直接在瀏覽器開啟
        public void TestImageNotFindWindow() {
            new Nyaa抓檔神器().ImageNotFindBehavior("http://www.google.com.tw/","na");
        }

        [TestMethod]
        //測試C#能不能解密AES
        public void TestAesDecrypt() {
            string input = "";
            string key = "";
            string iv = "";
            string result = GetPreViewImage.Decrypt("5dbd6ed07e2b5ec7584f3f9426fd159c", "443e6d0c0095e4facee39bbdab84a5b6", "cfd9f6f21b5731a30417d9680432cfba");
            Assert.IsNotNull(result);
        }


    }
}
