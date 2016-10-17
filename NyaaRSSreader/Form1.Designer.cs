namespace NyaaRSSreader
{
    partial class Form1
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.cbRssCate = new System.Windows.Forms.ComboBox();
            this.lbRssCate = new System.Windows.Forms.Label();
            this.btnLoadRSS = new System.Windows.Forms.Button();
            this.lbTorrPath = new System.Windows.Forms.Label();
            this.textPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
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
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
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
            // btnLoadRSS
            // 
            this.btnLoadRSS.Location = new System.Drawing.Point(130, 26);
            this.btnLoadRSS.Name = "btnLoadRSS";
            this.btnLoadRSS.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRSS.TabIndex = 2;
            this.btnLoadRSS.Text = "搜尋";
            this.btnLoadRSS.UseVisualStyleBackColor = true;
            this.btnLoadRSS.Click += new System.EventHandler(this.btnLoadRSS_Click);
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
            this.textPath.Location = new System.Drawing.Point(3, 75);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(119, 22);
            this.textPath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(130, 73);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "瀏覽";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
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
            this.dataGridView1.Location = new System.Drawing.Point(12, 103);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(839, 221);
            this.dataGridView1.TabIndex = 5;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            // 
            // Title
            // 
            dataGridViewCellStyle1.Font = new System.Drawing.Font("新細明體", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            this.Title.DefaultCellStyle = dataGridViewCellStyle1;
            this.Title.HeaderText = "標題";
            this.Title.Name = "Title";
            // 
            // Size
            // 
            this.Size.HeaderText = "檔案大小";
            this.Size.Name = "Size";
            this.Size.ReadOnly = true;
            // 
            // Seeder
            // 
            this.Seeder.HeaderText = "Seeders";
            this.Seeder.Name = "Seeder";
            this.Seeder.ReadOnly = true;
            // 
            // Leecher
            // 
            this.Leecher.HeaderText = "Leechers";
            this.Leecher.Name = "Leecher";
            this.Leecher.ReadOnly = true;
            // 
            // download
            // 
            this.download.HeaderText = "Downloads";
            this.download.Name = "download";
            this.download.ReadOnly = true;
            // 
            // articleLink
            // 
            this.articleLink.HeaderText = "文章連結";
            this.articleLink.Name = "articleLink";
            this.articleLink.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.articleLink.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.articleLink.Visible = false;
            // 
            // DownloadLink
            // 
            this.DownloadLink.HeaderText = "下載連結";
            this.DownloadLink.Name = "DownloadLink";
            this.DownloadLink.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.DownloadLink.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.DownloadLink.Visible = false;
            // 
            // Date
            // 
            this.Date.HeaderText = "發佈日期";
            this.Date.Name = "Date";
            this.Date.ReadOnly = true;
            // 
            // btnView
            // 
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.NullValue = "預覽圖";
            this.btnView.DefaultCellStyle = dataGridViewCellStyle2;
            this.btnView.HeaderText = "文章連結";
            this.btnView.Name = "btnView";
            this.btnView.Text = "預覽圖";
            this.btnView.UseColumnTextForButtonValue = true;
            // 
            // btnDownload
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Transparent;
            dataGridViewCellStyle3.NullValue = "下載";
            this.btnDownload.DefaultCellStyle = dataGridViewCellStyle3;
            this.btnDownload.HeaderText = "下載連結";
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Text = "下載";
            this.btnDownload.UseColumnTextForButtonValue = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnLoadRSS);
            this.panel1.Controls.Add(this.btnBrowse);
            this.panel1.Controls.Add(this.lbTorrPath);
            this.panel1.Controls.Add(this.textPath);
            this.panel1.Controls.Add(this.cbRssCate);
            this.panel1.Controls.Add(this.lbRssCate);
            this.panel1.Location = new System.Drawing.Point(14, -3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(586, 100);
            this.panel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 336);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRssCate;
        private System.Windows.Forms.Label lbRssCate;
        private System.Windows.Forms.Button btnLoadRSS;
        private System.Windows.Forms.Label lbTorrPath;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.DataGridView dataGridView1;
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
        private System.Windows.Forms.Panel panel1;
    }
}

