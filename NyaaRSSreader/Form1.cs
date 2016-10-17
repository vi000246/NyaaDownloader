using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Xml;
using System.XML.Linq;

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
            XmlDocument Xmldoc = new XmlDocument();
            //從指定的字串載入XML文件
            Xmldoc.Load(cbRssCate.SelectedValue.ToString());
            //建立此物件，並輸入透過StringReader讀取Xmldoc中的Xmldoc字串輸出
            XmlReader Xmlreader = XmlReader.Create(new System.IO.StringReader(Xmldoc.OuterXml));
            //建立DataSet
            DataSet ds = new DataSet();
            //透過DataSet的ReadXml方法來讀取Xmlreader資料
           // ds.ReadXml(Xmlreader);
            //建立DataTable並將DataSet中的第0個Table資料給DataTable
           // DataTable dt = ds.Tables[0];
            //回傳DataTable
           // return dt;

        }


        private void btnLoadRSS_Click(object sender, EventArgs e)
        {
            XmlStringToDataTable();
        }

    }

   
}
