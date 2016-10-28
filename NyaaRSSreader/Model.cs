using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NyaaRSSreader
{
    /// <summary>
    /// 用來存放各種物件
    /// </summary>
    public class Model
    {
        public class RssObj 
        {
            public string ArticleLink { get; set; }
            public string DownloadLink { get; set; }
            public string Size { get; set; }
            public string Title { get; set; }

            /// <summary>
            /// 建立Rss物件
            /// </summary>
            /// <param name="ArticleLink">文章網址</param>
            /// <param name="DownloadLink">種子下載地址</param>
            /// <param name="Size">檔案大小</param>
            /// <param name="Title">檔案標題</param>
            public RssObj(string ArticleLink,string DownloadLink,string Size,string Title) 
            {
                this.ArticleLink = ArticleLink;
                this.DownloadLink = DownloadLink;
                this.Size = Size;
                this.Title = Title;
            }
        }
    }
}
