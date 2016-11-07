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
            var urlList = new GetPreViewImage().GetBigImageUrl("http://t10.imgchili.net/93906/93906255_49gesu012pl.jpg");
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
            var urlList = new GetPreViewImage().GetBigImageUrl("http://imgdream.net/images/41748499593488458535.jpg");
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
            var urlList = new GetPreViewImage().GetBigImageUrl("http://www.imgbabes.com/g5e4vg38gwhd/XVSR-169_s.jpg.html");
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
        //有擋Continue to Image的網站   imgrock  imgview imgtown
        public void TestImgContinue()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://imgtown.net/o7ss5ap57m0q/UMSO-082.jpg.html http://imgrock.net/sgy68jca9je4/a66.jpg.html  http://imgview.net/wxwqjfjwt6dg/JUFD-637-1.jpg.html");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://[\d\w]+.(imgrock|imgview|imgtown).net/img/\w+/[\w-]+.jpe?g", RegexOptions.Singleline));
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
        //判斷imgtrex和imagetwist能不能傳回大圖
        public void TestImgtrexAndimagetwist()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://imgtrex.com/xk2lsfht6j92/AVOP-214.jpg  http://imagetwist.com/po3r49tmoyz1/87247336f2129a70ee23945d9f94407d.jpg");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://\w+.(imgtrex|imagetwist).com/i/[\w/]+.jpe?g", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷Javtotal能不能傳回大圖
        public void TestJavtotal()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl("http://cdn.javtotal.com/img/06504335015718765865");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://cdn.javtotal.com/images/\w+.jpe?g", RegexOptions.Singleline));
            }
        }

        [TestMethod]
        //判斷imgcandy和img.yt和idlelive和imgbb能不能傳回大圖
        public void TestImgCandy()
        {
            //輸入小圖的網址 
            var urlList = new GetPreViewImage().GetBigImageUrl(@" http://imgbb.net/v-58117f83b292e
            http://idlelive.com/uMT 
            https://img.yt/img-581185e90616e.html  
            http://imgcandy.net/img-58102e705444f_VENU-631.jpg.html 
            http://imgbb.net/v-580ceff919b0c_SPZ-922.jpg");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://(imgcandy|img).(net|yt)/upload/big/[\w-/_#&]+.jpe?g", RegexOptions.Singleline));
            }
        }



        [TestMethod]
        //大圖網址像 upload/big/2016/10/25/580f7084e76dc.jpg 的網站專用
        public void TestUrl_UploadBig()
        {
            //輸入小圖的網址 
        //            http://imgleveret.com/img-58102fd94a437.html 
        //http://imagedecode.com/img-580f72216b83d.html 
            var urlList = new GetPreViewImage().GetBigImageUrl(@"http://damimage.com/img-5810c8032ac04.html");
            Assert.IsTrue(urlList.Count > 0);
            foreach (var url in urlList)
            {
                Assert.IsTrue(Regex.IsMatch(url, @"http://[\w\.]*(imgleveret|imagedecode|porn84|imageteam|imgstudio|damimage).(com|org)/upload/big/[\w-/_#&]+.jpe?g", RegexOptions.Singleline));
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
