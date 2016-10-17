using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
        /// <summary>
        /// 下拉選單的值
        /// </summary>
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
            //取得每個文章的資料
            foreach (var item in rootNode)
            {
                string title = item.Element("title").Value;  // 標題
                string link = item.Element("link").Value;    // 下載連結
                string guid = item.Element("guid").Value;    // 文章連結
                string pubDate = item.Element("pubDate").Value;    // 發佈日期
                string description = item.Element("description").Value;    // 作種者 下載者個數

                //過濾出需要的資訊
                Regex pattern1 = new Regex(
@"(?<seeder>\d+)\s+seeder\(s\),\s+(?<leecher>\d+)\s+leecher\(s\),\s+(?<download>\d+)\s+download\(s\)\s-\s+(?<size>[\d.\w\W]+)"
                , RegexOptions.Singleline);
                Match match = pattern1.Match(description);

                string Download = match.Groups["download"].Value;
                string Size = match.Groups["size"].Value;
                string Leecher = match.Groups["leecher"].Value;
                string Seeder = match.Groups["seeder"].Value;
                
                this.dataGridView1.Rows.Add(title, Size, Seeder, Leecher, Download, guid, link, pubDate);
            }



        }

        /// <summary>
        /// 按下載入Rss按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadRSS_Click(object sender, EventArgs e)
        {
            XmlStringToDataTable();
        }

        /// <summary>
        /// Row Cell Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            
            //如果按下的是預覽按鈕
            if (e.ColumnIndex == dataGridView1.Columns["btnView"].Index && e.RowIndex >= 0)
            {
                MessageBox.Show(row.Cells["articleLink"].Value.ToString());
            }

            //如果按下的是下載按鈕
            if (e.ColumnIndex == dataGridView1.Columns["btnDownload"].Index && e.RowIndex >= 0)
            {
                MessageBox.Show(row.Cells["DownloadLink"].Value.ToString());
            }
        }

    }

   
}
