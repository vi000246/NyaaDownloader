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
            this.cbRssCate = new System.Windows.Forms.ComboBox();
            this.lbRssCate = new System.Windows.Forms.Label();
            this.btnLoadRSS = new System.Windows.Forms.Button();
            this.lbTorrPath = new System.Windows.Forms.Label();
            this.textPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cbRssCate
            // 
            this.cbRssCate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRssCate.FormattingEnabled = true;
            this.cbRssCate.Location = new System.Drawing.Point(12, 22);
            this.cbRssCate.Name = "cbRssCate";
            this.cbRssCate.Size = new System.Drawing.Size(121, 20);
            this.cbRssCate.TabIndex = 0;
            // 
            // lbRssCate
            // 
            this.lbRssCate.AutoSize = true;
            this.lbRssCate.Location = new System.Drawing.Point(12, 9);
            this.lbRssCate.Name = "lbRssCate";
            this.lbRssCate.Size = new System.Drawing.Size(97, 12);
            this.lbRssCate.TabIndex = 1;
            this.lbRssCate.Text = "請選擇RSS分類：";
            // 
            // btnLoadRSS
            // 
            this.btnLoadRSS.Location = new System.Drawing.Point(158, 19);
            this.btnLoadRSS.Name = "btnLoadRSS";
            this.btnLoadRSS.Size = new System.Drawing.Size(75, 23);
            this.btnLoadRSS.TabIndex = 2;
            this.btnLoadRSS.Text = "載入RSS";
            this.btnLoadRSS.UseVisualStyleBackColor = true;
            this.btnLoadRSS.Click += new System.EventHandler(this.btnLoadRSS_Click);
            // 
            // lbTorrPath
            // 
            this.lbTorrPath.AutoSize = true;
            this.lbTorrPath.Location = new System.Drawing.Point(12, 45);
            this.lbTorrPath.Name = "lbTorrPath";
            this.lbTorrPath.Size = new System.Drawing.Size(89, 12);
            this.lbTorrPath.TabIndex = 1;
            this.lbTorrPath.Text = "種子檔存檔路徑";
            // 
            // textPath
            // 
            this.textPath.Location = new System.Drawing.Point(14, 61);
            this.textPath.Name = "textPath";
            this.textPath.Size = new System.Drawing.Size(119, 22);
            this.textPath.TabIndex = 3;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(158, 59);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 4;
            this.btnBrowse.Text = "瀏覽";
            this.btnBrowse.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.textPath);
            this.Controls.Add(this.btnLoadRSS);
            this.Controls.Add(this.lbTorrPath);
            this.Controls.Add(this.lbRssCate);
            this.Controls.Add(this.cbRssCate);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRssCate;
        private System.Windows.Forms.Label lbRssCate;
        private System.Windows.Forms.Button btnLoadRSS;
        private System.Windows.Forms.Label lbTorrPath;
        private System.Windows.Forms.TextBox textPath;
        private System.Windows.Forms.Button btnBrowse;
    }
}

