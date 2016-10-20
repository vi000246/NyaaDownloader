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
        //判斷imgbabes能不能傳回大圖
        public void Testimgbabes()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://fenix.imgbabes.com/i/00693/co8c12vu0lr0_t.jpg");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
               // Assert.IsTrue(Regex.IsMatch(url, @"(?<url>http://imgdream.net/images/\d+.jpg)", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //測試抓不到預覽圖時的事件 1.彈出提示訊息 2.不顯示訊息 3.直接在瀏覽器開啟
        public void TestImageNotFindWindow() {
            new Nyaa抓檔神器().ImageNotFindBehavior("http://www.google.com.tw/","na");
        }


    }
}
