namespace NyaaRSSreader
{
    partial class Nyaa抓檔神器
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器
        /// 修改這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Nyaa抓檔神器));
            this.cbRssCate = new System.Windows.Forms.ComboBox();
            this.lbRssCate = new System.Windows.Forms.Label();
            this.lbTorrPath = new System.Windows.Forms.Label();
            this.textPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.textPage = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.cbSort = new System.Windows.Forms.ComboBox();
            this.labelSort = new System.Windows.Forms.Label();
            this.Title = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Size = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Seeder = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Leecher = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.download = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.articleLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DownloadLink = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnView = new System.Windows.Forms.DataGridViewButtonColumn();
            this.btnDownload = new System.Windows.Forms.DataGridViewButtonColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRssCate
            // 
            this.cbRssCate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRssCate.FormattingEnabled = true;
            this.cbRssCate.Location = new System.Drawing.Point(3, 29);
            this.cbRssCate.Name = "cbRssCate";
            this.cbRssCate.Size = new System.Drawing.Size(121, 20);
            this.cbRssCate.TabIndex = 0;
            this.cbRssCate.SelectionChangeCommitted += new System.EventHandler(this.cbRssCate_SelectionChangeCommitted);
            // 
            // lbRssCate
            // 
            this.lbRssCate.AutoSize = true;
            this.lbRssCate.Location = new System.Drawing.Point(3, 11);
            this.lbRssCate.Name = "lbRssCate";
            this.lbRssCate.Size = new System.Drawing.Size(97, 12);
            this.lbRssCate.TabIndex = 1;
            this.lbRssCate.Text = "請選擇RSS分類：";
            // 
            // lbTorrPath
            // 
            this.lbTorrPath.AutoSize = true;
            this.lbTorrPath.Location = new System.Drawing.Point(3, 54);
            this.lbTorrPath.Name = "lbTorrPath";
            this.lbTorrPath.Size = new System.Drawing.Size(89, 12);
            this.lbTorrPath.TabIndex = 1;
            this.lbTorrPath.Text = "種子檔存檔路徑";
            // 
            // textPath
            // 
            this.textPath.Enabled = false;
            this.textPath.Location = new System.Drawing.Point(3, 75);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(187, 22);
            this.textPath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(196, 73);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "瀏覽";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.CausesValidation = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Title,
            this.Size,
            this.Seeder,
            this.Leecher,
            this.download,
            this.articleLink,
            this.DownloadLink,
            this.Date,
            this.btnView,
            this.btnDownload});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LavenderBlush;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.SteelBlue;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.GridColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridView1.Location = new System.Drawing.Point(12, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(839, 205);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.SortCompare += new System.Windows.Forms.DataGridViewSortCompareEventHandler(this.dataGridView1_SortCompare);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.labelSort);
            this.panel1.Controls.Add(this.cbSort);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.lbTorrPath);
            this.panel1.Controls.Add(this.textPath);
            this.panel1.Controls.Add(this.cbRssCate);
            this.panel1.Controls.Add(this.lbRssCate);
            this.panel1.Location = new System.Drawing.Point(14, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(837, 100);
            this.panel1.TabIndex = 6;
            // 
            // btnPrev
            // 
            this.btnPrev.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnPrev.Enabled = false;
            this.btnPrev.Location = new System.Drawing.Point(9, 5);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(69, 23);
            this.btnPrev.TabIndex = 7;
            this.btnPrev.Text = "上一頁";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnNext
            // 
            this.btnNext.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnNext.Location = new System.Drawing.Point(128, 5);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(79, 23);
            this.btnNext.TabIndex = 7;
            this.btnNext.Text = "下一頁";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // textPage
            // 
            this.textPage.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textPage.Location = new System.Drawing.Point(85, 5);
            this.textPage.Name = "textPage";
            this.textPage.Size = new System.Drawing.Size(39, 22);
            this.textPage.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.panel2.Controls.Add(this.btnPrev);
            this.panel2.Controls.Add(this.btnNext);
            this.panel2.Controls.Add(this.textPage);
            this.panel2.Location = new System.Drawing.Point(312, 311);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(214, 32);
            this.panel2.TabIndex = 9;
            // 
            // cbSort
            // 
            this.cbSort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbSort.FormattingEnabled = true;
            this.cbSort.Location = new System.Drawing.Point(196, 29);
            this.cbSort.Name = "cbSort";
            this.cbSort.Size = new System.Drawing.Size(121, 20);
            this.cbSort.TabIndex = 5;
            this.cbSort.SelectionChangeCommitted += new System.EventHandler(this.cbSort_SelectionChangeCommitted);
            // 
            // labelSort
            // 
            this.labelSort.AutoSize = true;
            this.labelSort.Location = new System.Drawing.Point(196, 11);
            this.labelSort.Name = "labelSort";
            this.labelSort.Size = new System.Drawing.Size(89, 12);
            this.labelSort.TabIndex = 6;
            this.labelSort.Text = "單頁預設排序：";
            // 
            // Title
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.Title.DefaultCellStyle = dataGridViewCellStyle1;
            this.Title.FillWeight = 166.434F;
            this.Title.HeaderText = "標題";
            this.Title.MinimumWidth = 180;
            this.Title.Name = "Title";
            // 
            // Size
            // 
            this.Size.FillWeight = 145.0105F;
            this.Size.HeaderText = "檔案大小";
            this.Size.MinimumWidth = 50;
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            // 
            // Seeder
            // 
            this.Seeder.FillWeight = 61.00914F;
            this.Seeder.HeaderText = "Seeders";
            this.Seeder.MinimumWidth = 20;
            this.Seeder.Name = "Seeder";
            this.Seeder.ReadOnly = true;
            // 
            // Leecher
            // 
            this.Leecher.FillWeight = 55.71515F;
            this.Leecher.HeaderText = "Leechers";
            this.Leecher.MinimumWidth = 20;
            this.Leecher.Name = "Leecher";
            this.Leecher.ReadOnly = true;
            // 
            // download
            // 
            this.download.FillWeight = 44.11765F;
            this.download.HeaderText = "Downloads";
            this.download.MinimumWidth = 30;
            this.download.Name = "download";
            this.download.ReadOnly = true;
            // 
            // articleLink
            // 
            this.articleLink.HeaderText = "文章連結";
            this.articleLink.MinimumWidth = 10;
            this.articleLink.Name = "articleLink";
            this.articleLink.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.articleLink.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.articleLink.Visible = false;
            // 
            // DownloadLink
            // 
            this.DownloadLink.HeaderText = "下載連結";
            this.DownloadLink.MinimumWidth = 10;
            this.DownloadLink.Name = "DownloadLink";
            this.DownloadLink.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DownloadLink.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DownloadLink.Visible = false;
            // 
            // Date
            // 
            this.Date.FillWeight = 127.7135F;
            this.Date.HeaderText = "發佈日期";
            this.Date.MinimumWidth = 20;
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // btnView
            // 
            this.btnView.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.NullValue = "預覽圖";
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.Brown;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.White;
            this.btnView.DefaultCellStyle = dataGridViewCellStyle2;
            this.btnView.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnView.HeaderText = "文章連結";
            this.btnView.Name = "btnView";
            this.btnView.Text = "預覽圖";
            this.btnView.UseColumnTextForButtonValue = true;
            this.btnView.Width = 59;
            // 
            // btnDownload
            // 
            this.btnDownload.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Tomato;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("微軟正黑體", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.NullValue = "下載";
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.Brown;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            this.btnDownload.DefaultCellStyle = dataGridViewCellStyle3;
            this.btnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDownload.HeaderText = "下載Torrent";
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Text = "下載";
            this.btnDownload.UseColumnTextForButtonValue = true;
            this.btnDownload.Width = 70;
            // 
            // Nyaa抓檔神器
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 342);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Nyaa抓檔神器";
            this.Text = "Nyaa抓檔神器";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRssCate;
        private System.Windows.Forms.Label lbRssCate;
        private System.Windows.Forms.Label lbTorrPath;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.TextBox textPage;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbSort;
        private System.Windows.Forms.Label labelSort;
        private System.Windows.Forms.DataGridViewTextBoxColumn Title;
        private System.Windows.Forms.DataGridViewTextBoxColumn Size;
        private System.Windows.Forms.DataGridViewTextBoxColumn Seeder;
        private System.Windows.Forms.DataGridViewTextBoxColumn Leecher;
        private System.Windows.Forms.DataGridViewTextBoxColumn download;
        private System.Windows.Forms.DataGridViewTextBoxColumn articleLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn DownloadLink;
        private System.Windows.Forms.DataGridViewTextBoxColumn Date;
        private System.Windows.Forms.DataGridViewButtonColumn btnView;
        private System.Windows.Forms.DataGridViewButtonColumn btnDownload;
    }
}

