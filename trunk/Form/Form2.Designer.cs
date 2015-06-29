namespace RFIDReaderMiddleware
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.gbReaderAddress = new System.Windows.Forms.GroupBox();
            this.readerLB = new System.Windows.Forms.ListBox();
            this.btnOpenReader = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.lockBTN = new System.Windows.Forms.Button();
            this.lengthCB = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.startCB = new System.Windows.Forms.ComboBox();
            this.btnReadM4 = new System.Windows.Forms.Button();
            this.bankTypeCB = new System.Windows.Forms.ComboBox();
            this.dataLength = new System.Windows.Forms.Label();
            this.btnWrite = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.tbWriteValue = new System.Windows.Forms.TextBox();
            this.APTB = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.clearBTN = new System.Windows.Forms.Button();
            this.msgLB = new System.Windows.Forms.ListBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.gbReaderAddress.SuspendLayout();
            this.groupBox7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // gbReaderAddress
            // 
            this.gbReaderAddress.Controls.Add(this.readerLB);
            this.gbReaderAddress.Controls.Add(this.btnOpenReader);
            this.gbReaderAddress.Location = new System.Drawing.Point(9, 7);
            this.gbReaderAddress.Name = "gbReaderAddress";
            this.gbReaderAddress.Size = new System.Drawing.Size(295, 64);
            this.gbReaderAddress.TabIndex = 1;
            this.gbReaderAddress.TabStop = false;
            this.gbReaderAddress.Text = "Reader Address";
            // 
            // readerLB
            // 
            this.readerLB.FormattingEnabled = true;
            this.readerLB.ItemHeight = 12;
            this.readerLB.Location = new System.Drawing.Point(107, 17);
            this.readerLB.Name = "readerLB";
            this.readerLB.Size = new System.Drawing.Size(181, 40);
            this.readerLB.TabIndex = 7;
            // 
            // btnOpenReader
            // 
            this.btnOpenReader.Location = new System.Drawing.Point(5, 27);
            this.btnOpenReader.Name = "btnOpenReader";
            this.btnOpenReader.Size = new System.Drawing.Size(89, 21);
            this.btnOpenReader.TabIndex = 6;
            this.btnOpenReader.Text = "Open Reader";
            this.btnOpenReader.UseVisualStyleBackColor = true;
            this.btnOpenReader.Click += new System.EventHandler(this.btnOpenReader_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(327, 70);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "Messages Box";
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.lockBTN);
            this.groupBox7.Controls.Add(this.lengthCB);
            this.groupBox7.Controls.Add(this.label1);
            this.groupBox7.Controls.Add(this.startCB);
            this.groupBox7.Controls.Add(this.btnReadM4);
            this.groupBox7.Controls.Add(this.bankTypeCB);
            this.groupBox7.Controls.Add(this.dataLength);
            this.groupBox7.Controls.Add(this.btnWrite);
            this.groupBox7.Controls.Add(this.label12);
            this.groupBox7.Controls.Add(this.tbWriteValue);
            this.groupBox7.Controls.Add(this.APTB);
            this.groupBox7.Controls.Add(this.label11);
            this.groupBox7.Controls.Add(this.label8);
            this.groupBox7.Controls.Add(this.label9);
            this.groupBox7.Location = new System.Drawing.Point(9, 76);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(295, 201);
            this.groupBox7.TabIndex = 31;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Read/Write Data";
            // 
            // lockBTN
            // 
            this.lockBTN.Location = new System.Drawing.Point(35, 172);
            this.lockBTN.Name = "lockBTN";
            this.lockBTN.Size = new System.Drawing.Size(207, 21);
            this.lockBTN.TabIndex = 25;
            this.lockBTN.Text = "Lock/Kill";
            this.lockBTN.UseVisualStyleBackColor = true;
            this.lockBTN.Click += new System.EventHandler(this.lockBTN_Click);
            // 
            // lengthCB
            // 
            this.lengthCB.FormattingEnabled = true;
            this.lengthCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.lengthCB.Location = new System.Drawing.Point(146, 36);
            this.lengthCB.Name = "lengthCB";
            this.lengthCB.Size = new System.Drawing.Size(47, 20);
            this.lengthCB.TabIndex = 24;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(149, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 23;
            this.label1.Text = "Length";
            // 
            // startCB
            // 
            this.startCB.FormattingEnabled = true;
            this.startCB.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10"});
            this.startCB.Location = new System.Drawing.Point(91, 36);
            this.startCB.Name = "startCB";
            this.startCB.Size = new System.Drawing.Size(39, 20);
            this.startCB.TabIndex = 22;
            // 
            // btnReadM4
            // 
            this.btnReadM4.Location = new System.Drawing.Point(35, 117);
            this.btnReadM4.Name = "btnReadM4";
            this.btnReadM4.Size = new System.Drawing.Size(207, 21);
            this.btnReadM4.TabIndex = 21;
            this.btnReadM4.Text = "Read Data";
            this.btnReadM4.UseVisualStyleBackColor = true;
            this.btnReadM4.Click += new System.EventHandler(this.btnReadM4_Click);
            // 
            // bankTypeCB
            // 
            this.bankTypeCB.FormattingEnabled = true;
            this.bankTypeCB.Items.AddRange(new object[] {
            "RESERVED",
            "EPC",
            "TID",
            "USER"});
            this.bankTypeCB.Location = new System.Drawing.Point(14, 36);
            this.bankTypeCB.Name = "bankTypeCB";
            this.bankTypeCB.Size = new System.Drawing.Size(61, 20);
            this.bankTypeCB.TabIndex = 20;
            // 
            // dataLength
            // 
            this.dataLength.AutoSize = true;
            this.dataLength.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dataLength.ForeColor = System.Drawing.Color.Red;
            this.dataLength.Location = new System.Drawing.Point(249, 84);
            this.dataLength.Name = "dataLength";
            this.dataLength.Size = new System.Drawing.Size(29, 20);
            this.dataLength.TabIndex = 19;
            this.dataLength.Text = "24";
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(35, 145);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(207, 21);
            this.btnWrite.TabIndex = 18;
            this.btnWrite.Text = "Write Data";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(13, 67);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(119, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "Value to be written";
            // 
            // tbWriteValue
            // 
            this.tbWriteValue.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbWriteValue.Location = new System.Drawing.Point(14, 83);
            this.tbWriteValue.Multiline = true;
            this.tbWriteValue.Name = "tbWriteValue";
            this.tbWriteValue.Size = new System.Drawing.Size(228, 21);
            this.tbWriteValue.TabIndex = 16;
            this.tbWriteValue.Text = "360000000000005010100001";
            this.tbWriteValue.TextChanged += new System.EventHandler(this.tbWriteValue_TextChanged);
            // 
            // APTB
            // 
            this.APTB.Location = new System.Drawing.Point(209, 35);
            this.APTB.Name = "APTB";
            this.APTB.Size = new System.Drawing.Size(73, 21);
            this.APTB.TabIndex = 15;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(216, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 12);
            this.label11.TabIndex = 14;
            this.label11.Text = "Access PW";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(95, 21);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(35, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "Start";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 21);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 6;
            this.label9.Text = "Memory Bank";
            // 
            // clearBTN
            // 
            this.clearBTN.Location = new System.Drawing.Point(361, 223);
            this.clearBTN.Name = "clearBTN";
            this.clearBTN.Size = new System.Drawing.Size(146, 23);
            this.clearBTN.TabIndex = 33;
            this.clearBTN.Text = "Clear Message Box";
            this.clearBTN.UseVisualStyleBackColor = true;
            this.clearBTN.Click += new System.EventHandler(this.clearBTN_Click);
            // 
            // msgLB
            // 
            this.msgLB.FormattingEnabled = true;
            this.msgLB.ItemHeight = 12;
            this.msgLB.Location = new System.Drawing.Point(322, 85);
            this.msgLB.Name = "msgLB";
            this.msgLB.Size = new System.Drawing.Size(225, 124);
            this.msgLB.TabIndex = 35;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.InitialImage = null;
            this.pictureBox1.Location = new System.Drawing.Point(322, 9);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(225, 52);
            this.pictureBox1.TabIndex = 36;
            this.pictureBox1.TabStop = false;
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 282);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.msgLB);
            this.Controls.Add(this.clearBTN);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.gbReaderAddress);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form2";
            this.Text = "RFIDWriter";
            this.gbReaderAddress.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox gbReaderAddress;
        private System.Windows.Forms.Button btnOpenReader;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox tbWriteValue;
        private System.Windows.Forms.TextBox APTB;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button clearBTN;
        private System.Windows.Forms.ListBox msgLB;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label dataLength;
        private System.Windows.Forms.ComboBox bankTypeCB;
        private System.Windows.Forms.Button btnReadM4;
        private System.Windows.Forms.ListBox readerLB;
        private System.Windows.Forms.ComboBox lengthCB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox startCB;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button lockBTN;

    }
}