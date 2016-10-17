using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
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

            //檔案預設下載路徑是桌面或是全域變數裡的值
            if (string.IsNullOrEmpty(Properties.Settings.Default.Path))
                textPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            else
                textPath.Text = Properties.Settings.Default.Path;

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

        #region 將Xml字串轉成DataGridView
        /// <summary>
        /// 將Xml字串轉成Datatable
        /// </summary>
        /// <param name="Xmlstring"></param>
        /// <returns></returns>
        public void XmlStringToDataTable()
        {
            try
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
                    DateTime myDate;
                    string pubDate = string.Empty;
                    if (DateTime.TryParse(item.Element("pubDate").Value, out myDate))
                    {
                        pubDate = myDate.ToString("yyyy/MM/dd");
                    }

                    string description = item.Element("description").Value;
                    //過濾出需要的資訊
                    Regex pattern1 = new Regex(
    @"(?<seeder>\d+)\s+seeder\(s\),\s+(?<leecher>\d+)\s+leecher\(s\),\s+(?<download>\d+)\s+download\(s\)\s-\s+(?<size>[\d.]+\s+\w+)");
                    Match match = pattern1.Match(description);

                    string Download = match.Groups["download"].Value;
                    string Size = match.Groups["size"].Value;
                    string Leecher = match.Groups["leecher"].Value;
                    string Seeder = match.Groups["seeder"].Value;
                    //將資料綁定到dataGridView
                    this.dataGridView1.Rows.Add(title, Size, Seeder, Leecher, Download, guid, link, pubDate);
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }


        }
        #endregion



        /// <summary>
        /// Row Cell Button Click Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //不是head row
            if (e.RowIndex != -1)
            {
 
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];
            
                //如果按下的是預覽按鈕
                if (e.ColumnIndex == dataGridView1.Columns["btnView"].Index && e.RowIndex >= 0)
                {
                    if (row.Cells["articleLink"].Value != null)
                    {
                        MessageBox.Show(row.Cells["articleLink"].Value.ToString());
                    }
                    else {
                        MessageBox.Show("文章連結為空");
                    }
                }

                //如果按下的是下載按鈕
                if (e.ColumnIndex == dataGridView1.Columns["btnDownload"].Index && e.RowIndex >= 0)
                {
                    if (row.Cells["DownloadLink"] != null)
                    {
                        WebClient wc = new WebClient();
                        var data = wc.DownloadData(row.Cells["DownloadLink"].Value.ToString());
                        string fileName = "";

                        //取得原始檔名
                        if (!String.IsNullOrEmpty(wc.ResponseHeaders["Content-Disposition"]))
                        {
                            fileName = wc.ResponseHeaders["Content-Disposition"].Substring(wc.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "");
                        }
                        //下載檔案
                        using (WebClient webClient = new WebClient())
                        {
                            webClient.DownloadFile(row.Cells["DownloadLink"].Value.ToString(), textPath.Text +"\\"+ fileName);
                        }
                    }
                    else {
                        MessageBox.Show("下載連結為空");
                    }
                }

            }
        }
        #region 按下搜尋按鈕和檔案下載路徑鈕
        /// <summary>
        /// 按下搜尋按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadRSS_Click(object sender, EventArgs e)
        {
            XmlStringToDataTable();
        }

        //選擇檔案下載路徑按鈕
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();

            fbd.ShowNewFolderButton = false;
            fbd.RootFolder = System.Environment.SpecialFolder.Desktop;
            DialogResult dr = fbd.ShowDialog();

            if (dr == DialogResult.OK)
            {
                textPath.Text = fbd.SelectedPath;
                //將路徑存到全域變數
                Properties.Settings.Default.Path = fbd.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }
        #endregion

        #region 檔案大小的排序事件
        //dataGridView排序事件
        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            //if (e.Column.Name == "Size")
            //{
                 e.Handled = true;
                 e.SortResult = Compare(e.CellValue1.ToString(), e.CellValue2.ToString());
            //}
        }
        private int Compare(string o1, string o2)
        {
            //用conpareTo方法比較大小
            return GBConvertToMB(o1).CompareTo(GBConvertToMB(o2));
        }
        /// <summary>
        /// 輸入像是"1.67 Gib"或是"2.47 Mib"的字串 轉成MB回傳
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private decimal GBConvertToMB(string obj) 
        {
            decimal result = 0;
            //去除數字以外的字串
            string input = Regex.Replace(obj, @"[^\d\.]+", string.Empty);
            decimal decimalInput = 0;
            if (decimal.TryParse(input, out decimalInput))
            {
                result = decimalInput;
            }
            if (obj.ToString().IndexOf("GiB") > -1)
            {
                result *= 1000;
            }
            return result;
        }
        #endregion

    }

   
}
