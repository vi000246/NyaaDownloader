using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace NyaaRSSreader
{
    public partial class Nyaa抓檔神器 : Form
    {
        //頁數 從第一頁開始
        public int OffsetIndex = 1;

        public Nyaa抓檔神器()
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
            XmlStringToDataTable(BuildRssUrl());
            //顯示目前頁數
            textPage.Text = OffsetIndex.ToString();
            //頁數textBox的文字置中
            textPage.TextAlign = HorizontalAlignment.Center;
            //頁數textbox的按下Enter事件
            this.textPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(CheckEnterKeyPress);
        }
        #region 綁定下拉選單的值
        /// <summary>
        /// 下拉選單的值
        /// </summary>
        public void BindComboboxData(){
            //分類下拉選單
        cbRssCate.DataSource = new BindingSource(FormSetting.CatalogueDropDownList, null);
        cbRssCate.DisplayMember = "Value";
        cbRssCate.ValueMember = "Key";
        //選擇預設的選項 
        cbRssCate.SelectedIndex = Properties.Settings.Default.ComboIndex;

            //排序下拉選單
        cbSort.DataSource = new BindingSource(FormSetting.SortDropDownList, null);
        cbSort.DisplayMember = "Value";
        cbSort.ValueMember = "Key";
        //選擇預設的選項 
        cbSort.SelectedIndex = Properties.Settings.Default.cbSortIndex;

        //查無預覽圖預設行為
        cbPopWindowBehavior.SelectedIndex = Properties.Settings.Default.cbNotFindImage;
        }
        #endregion

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
                    //將資料綁定到dataGridView (標題、日期、serder、leecher、文章連結(hide)、下載連結(hide)、檔案大小、下載數量)
                    this.dataGridView1.Rows.Add(title, pubDate, Seeder, Leecher, guid, link,Size,Download);
                    //顯示目前頁數
                    textPage.Text = OffsetIndex.ToString();
                    //依照下拉選單的值 排序DataGridView
                    if (cbSort.SelectedValue.ToString()!="無")
                        SortDataGrid();
                    //設定上一步、下一步按鈕
                    ButtonEnable();
                }

            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }


        }
        #endregion

        //排序DataGridView
        public void SortDataGrid() {
            //依據下拉選單的值 自動排序
            if (cbSort.SelectedValue.ToString() != "無") 
                this.dataGridView1.Sort(this.dataGridView1.Columns[cbSort.SelectedValue.ToString()], ListSortDirection.Descending);

        }

        #region Row Cell Button Click Event
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
                    #region GridView的預覽按鈕Click
                    //==============
                    //如果按下的是預覽按鈕
                    if (e.ColumnIndex == dataGridView1.Columns["btnView"].Index && e.RowIndex >= 0)
                    {
                        if (row.Cells["articleLink"].Value != null)
                        {
                                var t = new Thread(() => ImagePopup(row.Cells["articleLink"].Value.ToString(), row));
                                t.IsBackground = true;
                                t.Start();
                        }
                        else
                        {
                            MessageBox.Show("文章連結為空");
                        }
                    }
                    #endregion

                    #region GridView的下載按鈕click
                    //==============
                    //如果按下的是下載按鈕
                    if (e.ColumnIndex == dataGridView1.Columns["btnDownload"].Index && e.RowIndex >= 0)
                    {
                        if (row.Cells["DownloadLink"] != null)
                        {
                            var t = new Thread(() => DownloadTorr(row.Cells["DownloadLink"].Value.ToString()));
                            t.IsBackground = true;
                            t.Start();
                        }
                        else
                        {
                            MessageBox.Show("下載連結為空");
                        }
                    }
                    #endregion

                }
            }
            catch (Exception ex) {
                MessageBox.Show("發生錯誤"+ex.Message);
            }
        }
        #endregion
        #region 下載Torr檔
        /// <summary>
        /// 下載Torr檔
        /// </summary>
        /// <param name="DownloadLink">Torr的下載連結</param>
        public void DownloadTorr(string DownloadLink) {
            WebClient wc = new WebClient();
            var data = wc.DownloadData(DownloadLink);
            string fileName = "";

            //取得原始檔名
            if (!String.IsNullOrEmpty(wc.ResponseHeaders["Content-Disposition"]))
            {
                fileName = wc.ResponseHeaders["Content-Disposition"].Substring(wc.ResponseHeaders["Content-Disposition"].IndexOf("filename=") + 10).Replace("\"", "");
            }
            //下載檔案
            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(DownloadLink, textPath.Text + "\\" + fileName);
            }
        }
        #endregion

        #region 傳入文章頁面 解析出圖片再popup出來  、popup視窗的下載按鈕點擊事件 、查無預覽圖提示視窗
        /// <summary>
        /// 傳入文章頁面 解析出圖片再popup出來
        /// </summary>
        /// <param name="url"></param>
        public void ImagePopup(string url,DataGridViewRow Row) {
            try
            {
                //取得圖片網址List
                List<string> imageFileList = new GetPreViewImage().CallImageHanderdle(url);
                //如果有解析到圖片
                if (imageFileList.Count > 0)
                {
                    if (FormSetting.IsEnablePopup)
                    {
                        using (Form form = new Form())
                        {

                            form.StartPosition = FormStartPosition.CenterScreen;
                            //popup視窗標題
                            string Size = Row.Cells["Size"] == null ? "" : Row.Cells["Size"].Value.ToString();
                            string Title = Row.Cells["Title"] == null ? "" : Row.Cells["Title"].Value.ToString();
                            form.Text = "預覽圖 [" + Size + "] " + Title;

                            //因為已經有按鈕了 所以從按鈕高度開始插入picturebox
                            int TotalHeight = FormSetting.PopupWindow.btnDownloadHeight;
                            int MaxWidth = 0;
                            //依據imageList的個數 產生出數個picturebox
                            foreach (var imageFile in imageFileList)
                            {
                                PictureBox eachPictureBox = new PictureBox();
                                form.Controls.Add(eachPictureBox);
                                eachPictureBox.SizeMode = PictureBoxSizeMode.AutoSize;
                                //將圖片下移總高度
                                eachPictureBox.Top = TotalHeight;
                                //載入圖片
                                eachPictureBox.Load(imageFile);
                                //如果總高度小於螢幕高度 將總高度加上此圖片的高度
                                if (TotalHeight + eachPictureBox.Size.Height < Screen.PrimaryScreen.Bounds.Height)
                                    TotalHeight += eachPictureBox.Size.Height;
                                else
                                    TotalHeight = Screen.PrimaryScreen.Bounds.Height - 50;
                                //取得最大圖片的寬度
                                if (eachPictureBox.Size.Width > MaxWidth)
                                    MaxWidth = eachPictureBox.Size.Width;
                            }
                            //改變form的大小 如果小於setting裡的最小寬度 就設為最小寬度
                            form.Size = new Size(MaxWidth < FormSetting.PopupWindow.popWindowMinWidth ?
                                FormSetting.PopupWindow.popWindowMinWidth : MaxWidth, TotalHeight);

                            //增加下載連結按鈕
                            Button btnDownload = new Button();
                            btnDownload.Width = FormSetting.PopupWindow.btnDownloadWidth;
                            btnDownload.Height = FormSetting.PopupWindow.btnDownloadHeight;
                            btnDownload.ForeColor = Color.Black;
                            btnDownload.Text = "下載";
                            //讓button置於最上面的中間
                            btnDownload.Left = (form.ClientSize.Width - btnDownload.Width) / 2;
                            form.Controls.Add(btnDownload);

                            //增加熱鍵說明的label
                            Label info = new Label();
                            info.TextAlign = ContentAlignment.TopLeft;
                            //C#顏色表 http://www.flounder.com/csharp_color_table.htm
                            info.BackColor = Color.SkyBlue;
                            info.Font = new Font("Arial", 10, FontStyle.Bold);
                            string text = "熱鍵：" + Environment.NewLine + "(D):下載種子 (C):關閉視窗 (A):下載種子並關閉視窗";
                            info.Text = text;
                            info.AutoSize = true;
                            form.Controls.Add(info);

                            //綁定下載按鈕點擊事件
                            btnDownload.Click += (sender, EventArgs) => buttonDownload_Click(sender, EventArgs, Row.Cells["DownloadLink"]);
                            //啟用form的點擊事件
                            form.KeyPreview = true;
                            //綁定form的熱鍵
                            form.KeyDown += (sender, e) => this.PopFormHotKey(sender, e, form, Row.Cells["DownloadLink"]);

                            //如果預覽圖大於螢幕高度 就增加捲軸
                            if (TotalHeight >= (Screen.PrimaryScreen.Bounds.Height - 50))
                                form.AutoScroll = true;
                            //表單透明度 記得拿掉
                            form.Opacity = FormSetting.FormOpaticy;
                            form.ShowDialog();
                        }//end of using form
                    }//end of FormSetting.IsEnablePopup

                }
                else
                {
                    //彈出視窗詢問是否直接開啟網頁
                    ImageNotFindBehavior(url, "na");
                }
            }catch(Exception ex){
                //彈出視窗詢問是否直接開啟網頁
                ImageNotFindBehavior(url, "error");
            }

        }

        //popup視窗的下載按鈕點擊事件
        void buttonDownload_Click(object sender, EventArgs e, DataGridViewCell CellValue)
        {
            if (CellValue != null)
            {
                var t = new Thread(() => DownloadTorr(CellValue.Value.ToString()));
                t.IsBackground = true;
                t.Start();
            }
            else
                MessageBox.Show("下載連結為空");
        }


        //綁定視窗的熱鍵
        void PopFormHotKey(object sender, KeyEventArgs e, Form form, DataGridViewCell CellValue)
        {
            if (e.KeyCode == Keys.D || e.KeyCode == Keys.A)
            {
                if (CellValue != null)
                {
                    var t = new Thread(() => DownloadTorr(CellValue.Value.ToString()));
                    t.IsBackground = true;
                    t.Start();
                }
                else
                    MessageBox.Show("下載連結為空");
            }

            if (e.KeyCode == Keys.C || e.KeyCode == Keys.A)
            {
                form.Close();
                form.Dispose();
            }
        }

        /// <summary>
        /// 當回傳的預覽圖=0時的事件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="type"></param>
        public void ImageNotFindBehavior(string url,string type)
        {
            int selectedIndex=0;
            //下拉選單選中的值  0:提醒視窗 1:忽略 2:自動開啟連結
            cbPopWindowBehavior.InvokeIfRequired(() =>
            {
                selectedIndex = cbPopWindowBehavior.SelectedIndex;
            });

            string msg="";
            if (type == "error")
                msg = "發生錯誤，是否直接開啟頁面?";
            else if (type == "na")
                msg = "尚不支援此圖床，是否直接開啟頁面?";

            //如果是"忽略" 就無動作
            if (selectedIndex != 1)
            {
                //彈出MessageBox詢問是否直接開啟網頁
                if (
                    selectedIndex == 2||
                    (MessageBox.Show(msg, "訊息", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                        MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes))
                {
                    //用瀏覽器開啟網址
                    ProcessStartInfo sInfo = new ProcessStartInfo(url);
                    Process.Start(sInfo);
                }
            }


        }
        #endregion

        #region 檔案下載路徑鈕、改變下拉選單、上一頁、下一頁、改變頁數、刷新按鈕

        //當下拉選單發生選取事件時
        private void cbRssCate_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string url = BuildRssUrl();
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
        //排序下拉選單的值改變時
        private void cbSort_SelectionChangeCommitted(object sender, EventArgs e)
        {
            //排序dataGrid
            SortDataGrid();
            //將下拉選單的值存到全域變數
            Properties.Settings.Default.cbSortIndex = cbSort.SelectedIndex;
            Properties.Settings.Default.Save();

            
        }

        //查無預覽圖預設事件下拉選單的值改變時
        private void cbPopWindowBehavior_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Properties.Settings.Default.cbNotFindImage = cbPopWindowBehavior.SelectedIndex;
            Properties.Settings.Default.Save();
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
            string url = BuildRssUrl();
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

        //刷新按鈕
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //清空dataGridView的資料
            this.dataGridView1.Rows.Clear();
            XmlStringToDataTable(BuildRssUrl());
        }
        #endregion
        #region 判斷上一頁、下一頁按鈕可否使用  | textBox只可輸入數字
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
        private void TxtBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            //如果不是數字且不是backspace 就禁止輸入
            if (!Char.IsDigit(e.KeyChar)&& e.KeyChar != (char)8)
                e.Handled = true;
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

        #region 組成網址搜尋條件 回傳網址
        private string BuildRssUrl() {
            string url = string.Empty;
            url = cbRssCate.SelectedValue.ToString();
            //標題搜尋
            if (!string.IsNullOrEmpty(textKeyWord.Text))
                url += "&term=" + textKeyWord.Text;
            //排序搜尋
            if (cbRssSort.SelectedIndex != -1)
                url += "&sort="+(cbRssSort.SelectedIndex+1);
            //日期B搜尋
            if (!string.IsNullOrEmpty(textDate_B.Text))
                url += "&minage=" + textDate_B.Text;
            //日期E搜尋
            if (!string.IsNullOrEmpty(textDate_E.Text))
                url += "&maxage=" + textDate_E.Text;
            //檔案大小B搜尋
            if (!string.IsNullOrEmpty(textLarge_B.Text))
                url += "&minsize=" + textLarge_B.Text;
            //檔案大小E搜尋
            if (!string.IsNullOrEmpty(textLarge_E.Text))
                url += "&maxsize=" + textLarge_E.Text;

            return url;
        }
        #endregion






    }

   
}

//擴充方法
public static class Extension
{
    /// <summary>
    /// 非同步委派更新UI
    /// 使用範例:
    ///         (控制項名稱)cbPopWindowBehavior.InvokeIfRequired(() =>
    ///        {
    ///            selectedIndex = cbPopWindowBehavior.SelectedIndex;
    ///        });
    /// </summary>
    /// <param name="control"></param>
    /// <param name="action"></param>

    public static void InvokeIfRequired(
        this Control control, MethodInvoker action)
    {
        if (control.InvokeRequired)//在非當前執行緒內 使用委派
        {
            control.Invoke(action);
        }
        else
        {
            action();
        }
    }
}
