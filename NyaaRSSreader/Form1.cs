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
        //頁數 從第一頁開始
        public int OffsetIndex = 1;

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

            //產生第一頁的資料
            XmlStringToDataTable(cbRssCate.SelectedValue.ToString());
            //顯示目前頁數
            textPage.Text = OffsetIndex.ToString();
            //頁數textBox的文字置中
            textPage.TextAlign = HorizontalAlignment.Center;
            //頁數textbox的按下Enter事件
            this.textPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
        }
        /// <summary>
        /// 下拉選單的值
        /// </summary>
        public void BindComboboxData(){
            Dictionary<string, string>test = new Dictionary<string, string>();
            test.Add("https://www.nyaa.se/?page=rss&cats=1_0", "Anime");
            test.Add("https://www.nyaa.se/?page=rss&cats=3_0", "Audio");
            test.Add("https://sukebei.nyaa.se/?page=rss&cats=8_0", "R18-RealLife");

        
        cbRssCate.DataSource = new BindingSource(test, null);
        cbRssCate.DisplayMember = "Value";
        cbRssCate.ValueMember = "Key";
        //選擇預設的選項 
        cbRssCate.SelectedIndex = Properties.Settings.Default.ComboIndex;
        }

        #region 將RSS取得的Xml字串轉成DataGridView
        /// <summary>
        /// 將Xml字串轉成Datatable
        /// </summary>
        /// <param name="Xmlstring"></param>
        /// <returns></returns>
        public void XmlStringToDataTable(string url)
        {
            try
            {
                //新建XML文件類別
                XDocument myXDoc = XDocument.Load(url);
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
                        pubDate = myDate.ToString("yyyy/MM/dd HH:mm");
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
                    //顯示目前頁數
                    textPage.Text = OffsetIndex.ToString();
                    ButtonEnable();
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
            try
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
                            //row.Cells["articleLink"].Value.ToString()
                            //只有RealLife類別才能用預覽圖
                            if(!cbRssCate.Text.Contains("RealLife"))
                                MessageBox.Show("此類別尚不支援預覽圖功能");
                            else
                                MessageBox.Show(row.Cells["articleLink"].Value.ToString());
                        }
                        else
                        {
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
                                webClient.DownloadFile(row.Cells["DownloadLink"].Value.ToString(), textPath.Text + "\\" + fileName);
                            }
                        }
                        else
                        {
                            MessageBox.Show("下載連結為空");
                        }
                    }

                }
            }
            catch (Exception ex) {
                MessageBox.Show("發生錯誤"+ex.Message);
            }
        }
        #region 檔案下載路徑鈕、改變下拉選單、上一頁、下一頁、改變頁數

        //當下拉選單發生選取事件時
        private void cbRssCate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string url = cbRssCate.SelectedValue.ToString();
            if (!string.IsNullOrEmpty(url))
            {
                //清空dataGridView的資料
                this.dataGridView1.Rows.Clear();
                //重設頁數為1
                OffsetIndex = 1;
                //載入資料
                XmlStringToDataTable(url);

                //將下拉選單的值存到全域變數
                Properties.Settings.Default.ComboIndex = cbRssCate.SelectedIndex;
                Properties.Settings.Default.Save();

            }
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

        //按下上一頁事件
        private void btnPrev_Click(object sender, EventArgs e)
        {
            PageButtonClickEvent("Prev");
        }

        //按下下一頁事件
        private void btnNext_Click(object sender, EventArgs e)
        {
            PageButtonClickEvent("Next");
        }

        public void PageButtonClickEvent(string type,int jumpPage=0) 
        {
            string offset=string.Empty;
            string url = cbRssCate.SelectedValue.ToString();
            //判斷要增加頁數還是減頁數還是跳頁
            if(type=="Prev")
                 offset = "&offset=" + --OffsetIndex;
            else if (type == "Next")
                offset = "&offset=" + ++OffsetIndex;
            else if (type == "Jump")
            {
                offset = "&offset=" + jumpPage;
                OffsetIndex = jumpPage;
            }
            ButtonEnable();
            url += offset;

            if (!string.IsNullOrEmpty(url))
            {
                //清空dataGridView的資料
                this.dataGridView1.Rows.Clear();
                //載入資料
                XmlStringToDataTable(url);

                //顯示目前頁數
                textPage.Text = OffsetIndex.ToString();
            }
        }

        //改變頁數text事件 按下Enter才會觸發
        private void CheckEnterKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                int page;
                if (int.TryParse(textPage.Text, out page) && page > 0 && page < 101)
                {
                        PageButtonClickEvent("Jump", page);
                }
                else
                {
                    MessageBox.Show("請輸入1~100間的數字");
                }
            }
        }
        #endregion
        #region 判斷上一頁、下一頁按鈕可否使用
        //判斷上一頁、下一頁按鈕可否使用
        public void ButtonEnable() {
            if (OffsetIndex == 1)
            {
                btnPrev.Enabled = false;
                btnNext.Enabled = true;
            }
            else if (OffsetIndex == 100)
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = false;
            }
            else
            {
                btnPrev.Enabled = true;
                btnNext.Enabled = true;
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
