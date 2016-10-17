using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace NyaaRSSreader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //綁定下拉選單的物件
            BindComboboxData();
        }

        public void BindComboboxData(){
            Dictionary<string, string>test = new Dictionary<string, string>();
            test.Add("https://sukebei.nyaa.se/?page=rss&cats=8_0", "R18-RealLife");
        cbRssCate.DataSource = new BindingSource(test, null);
        cbRssCate.DisplayMember = "Value";
        cbRssCate.ValueMember = "Key";
        }


        /// <summary>
        /// 將Xml字串轉成Datatable
        /// </summary>
        /// <param name="Xmlstring"></param>
        /// <returns></returns>
        public void XmlStringToDataTable()
        {

            //新建XML文件類別
             XDocument myXDoc = XDocument.Load(cbRssCate.SelectedValue.ToString());
             var rootNode = myXDoc.Root.Element("channel").Descendants("item");
            foreach (var item in rootNode)
            {
                string title = item.Element("title").Value;  // 標題
                string link = item.Element("link").Value;    // 下載連結
                string guid = item.Element("guid").Value;    // 文章連結
                string description = item.Element("description").Value;    // 作種者 下載者個數
                string pubDate = item.Element("pubDate").Value;    // 發佈日期
            }



        }


        private void btnLoadRSS_Click(object sender, EventArgs e)
        {
            XmlStringToDataTable();
        }

    }

   
}
