namespace RFIDReaderMiddleware
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.openreaderBTN = new System.Windows.Forms.Button();
            this.readerListBox = new System.Windows.Forms.ListBox();
            this.monitorrfidBTN = new System.Windows.Forms.Button();
            this.contentrfidTB = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.closereaderBTN = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.sendCountLable = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            //this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // openreaderBTN
            // 
            this.openreaderBTN.Location = new System.Drawing.Point(12, 15);
            this.openreaderBTN.Name = "openreaderBTN";
            this.openreaderBTN.Size = new System.Drawing.Size(101, 23);
            this.openreaderBTN.TabIndex = 0;
            this.openreaderBTN.Text = "打开Reader";
            this.openreaderBTN.UseVisualStyleBackColor = true;
            this.openreaderBTN.Click += new System.EventHandler(this.openreaderBTN_Click);
            // 
            // readerListBox
            // 
            this.readerListBox.FormattingEnabled = true;
            this.readerListBox.ItemHeight = 12;
            this.readerListBox.Location = new System.Drawing.Point(15, 77);
            this.readerListBox.Name = "readerListBox";
            this.readerListBox.Size = new System.Drawing.Size(190, 292);
            this.readerListBox.TabIndex = 1;
            // 
            // monitorrfidBTN
            // 
            this.monitorrfidBTN.Enabled = false;
            this.monitorrfidBTN.Location = new System.Drawing.Point(265, 14);
            this.monitorrfidBTN.Name = "monitorrfidBTN";
            this.monitorrfidBTN.Size = new System.Drawing.Size(112, 23);
            this.monitorrfidBTN.TabIndex = 2;
            this.monitorrfidBTN.Text = "监控数据";
            this.monitorrfidBTN.UseVisualStyleBackColor = true;
            this.monitorrfidBTN.Click += new System.EventHandler(this.monitorrfidBTN_Click);
            // 
            // contentrfidTB
            // 
            this.contentrfidTB.Location = new System.Drawing.Point(235, 78);
            this.contentrfidTB.Multiline = true;
            this.contentrfidTB.Name = "contentrfidTB";
            this.contentrfidTB.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.contentrfidTB.Size = new System.Drawing.Size(456, 292);
            this.contentrfidTB.TabIndex = 3;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // closereaderBTN
            // 
            this.closereaderBTN.Enabled = false;
            this.closereaderBTN.Location = new System.Drawing.Point(136, 14);
            this.closereaderBTN.Name = "closereaderBTN";
            this.closereaderBTN.Size = new System.Drawing.Size(104, 23);
            this.closereaderBTN.TabIndex = 4;
            this.closereaderBTN.Text = "关闭Reader";
            this.closereaderBTN.UseVisualStyleBackColor = true;
            this.closereaderBTN.Click += new System.EventHandler(this.closereaderBTN_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(244, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(116, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "获取信息总数:\r\n";
            // 
            // sendCountLable
            // 
            this.sendCountLable.AutoSize = true;
            this.sendCountLable.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendCountLable.ForeColor = System.Drawing.Color.Red;
            this.sendCountLable.Location = new System.Drawing.Point(359, 52);
            this.sendCountLable.Name = "sendCountLable";
            this.sendCountLable.Size = new System.Drawing.Size(19, 20);
            this.sendCountLable.TabIndex = 6;
            this.sendCountLable.Text = "0";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(570, 14);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(81, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "写标签";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "读写器列表";
            // 
            // button2
            /* 
            this.button2.Location = new System.Drawing.Point(398, 14);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "Simulate";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);*/
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(700, 381);
            //this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.sendCountLable);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.closereaderBTN);
            this.Controls.Add(this.contentrfidTB);
            this.Controls.Add(this.monitorrfidBTN);
            this.Controls.Add(this.readerListBox);
            this.Controls.Add(this.openreaderBTN);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "RFIDReader";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openreaderBTN;
        private System.Windows.Forms.ListBox readerListBox;
        private System.Windows.Forms.Button monitorrfidBTN;
        private System.Windows.Forms.TextBox contentrfidTB;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button closereaderBTN;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label sendCountLable;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label2;
        //private System.Windows.Forms.Button button2;
    }
}

