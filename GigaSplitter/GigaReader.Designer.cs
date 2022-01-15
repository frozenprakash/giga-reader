namespace GigaReader
{
    partial class frmCountandSplit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCountandSplit));
            this.lblTotalLineCountValue = new System.Windows.Forms.Label();
            this.lblTotalClaimCount = new System.Windows.Forms.Label();
            this.btnSaveFile = new System.Windows.Forms.Button();
            this.btnSourceFile = new System.Windows.Forms.Button();
            this.txtSourceFile = new System.Windows.Forms.TextBox();
            this.txtDestinationFile = new System.Windows.Forms.TextBox();
            this.ofdSourceFile = new System.Windows.Forms.OpenFileDialog();
            this.sfdDestinationFolder = new System.Windows.Forms.SaveFileDialog();
            this.lblDestinationFile = new System.Windows.Forms.Label();
            this.ss1 = new System.Windows.Forms.StatusStrip();
            this.ss1Lbl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ss1Pb1 = new System.Windows.Forms.ToolStripProgressBar();
            this.ss1Lbl2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.ss2 = new System.Windows.Forms.StatusStrip();
            this.ss2Lbl1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuApplication = new System.Windows.Forms.ToolStripMenuItem();
            this.rtb1 = new System.Windows.Forms.RichTextBox();
            this.rdoReadFirst1000Lines = new System.Windows.Forms.RadioButton();
            this.rdoReadLast1000Lines = new System.Windows.Forms.RadioButton();
            this.btnClearText = new System.Windows.Forms.Button();
            this.rdoReadCustomLines = new System.Windows.Forms.RadioButton();
            this.txtFromRCL = new System.Windows.Forms.TextBox();
            this.txtToRCL = new System.Windows.Forms.TextBox();
            this.lblFromRCL = new System.Windows.Forms.Label();
            this.lblToRCL = new System.Windows.Forms.Label();
            this.chkModifyData = new System.Windows.Forms.CheckBox();
            this.btnRemoveHeader = new System.Windows.Forms.Button();
            this.btnRemoveTrailer = new System.Windows.Forms.Button();
            this.ss1.SuspendLayout();
            this.ss2.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTotalLineCountValue
            // 
            this.lblTotalLineCountValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalLineCountValue.AutoSize = true;
            this.lblTotalLineCountValue.Location = new System.Drawing.Point(825, 51);
            this.lblTotalLineCountValue.Name = "lblTotalLineCountValue";
            this.lblTotalLineCountValue.Size = new System.Drawing.Size(143, 13);
            this.lblTotalLineCountValue.TabIndex = 17;
            this.lblTotalLineCountValue.Text = "<Total Line Count - Number>";
            // 
            // lblTotalClaimCount
            // 
            this.lblTotalClaimCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotalClaimCount.AutoSize = true;
            this.lblTotalClaimCount.Location = new System.Drawing.Point(731, 51);
            this.lblTotalClaimCount.Name = "lblTotalClaimCount";
            this.lblTotalClaimCount.Size = new System.Drawing.Size(88, 13);
            this.lblTotalClaimCount.TabIndex = 16;
            this.lblTotalClaimCount.Text = "Total Line Count:";
            // 
            // btnSaveFile
            // 
            this.btnSaveFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveFile.Location = new System.Drawing.Point(12, 421);
            this.btnSaveFile.Name = "btnSaveFile";
            this.btnSaveFile.Size = new System.Drawing.Size(518, 46);
            this.btnSaveFile.TabIndex = 20;
            this.btnSaveFile.Text = "&Save File";
            this.btnSaveFile.UseVisualStyleBackColor = true;
            this.btnSaveFile.Click += new System.EventHandler(this.btnSaveFile_Click);
            // 
            // btnSourceFile
            // 
            this.btnSourceFile.Location = new System.Drawing.Point(12, 46);
            this.btnSourceFile.Name = "btnSourceFile";
            this.btnSourceFile.Size = new System.Drawing.Size(104, 23);
            this.btnSourceFile.TabIndex = 0;
            this.btnSourceFile.Text = "Source &File";
            this.btnSourceFile.UseVisualStyleBackColor = true;
            this.btnSourceFile.Click += new System.EventHandler(this.btnSourceFile_Click);
            // 
            // txtSourceFile
            // 
            this.txtSourceFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourceFile.Location = new System.Drawing.Point(128, 48);
            this.txtSourceFile.Name = "txtSourceFile";
            this.txtSourceFile.Size = new System.Drawing.Size(588, 20);
            this.txtSourceFile.TabIndex = 1;
            // 
            // txtDestinationFile
            // 
            this.txtDestinationFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtDestinationFile.Location = new System.Drawing.Point(128, 83);
            this.txtDestinationFile.Name = "txtDestinationFile";
            this.txtDestinationFile.Size = new System.Drawing.Size(588, 20);
            this.txtDestinationFile.TabIndex = 3;
            // 
            // ofdSourceFile
            // 
            this.ofdSourceFile.Filter = "Text Files (*.txt)|*.txt|XML files (*.xml)|*.xml";
            this.ofdSourceFile.RestoreDirectory = true;
            this.ofdSourceFile.Title = "Source File";
            // 
            // sfdDestinationFolder
            // 
            this.sfdDestinationFolder.FileName = "Destination Folder";
            this.sfdDestinationFolder.Filter = "All files|*.*";
            this.sfdDestinationFolder.RestoreDirectory = true;
            this.sfdDestinationFolder.Title = "Destination Folder";
            // 
            // lblDestinationFile
            // 
            this.lblDestinationFile.AutoSize = true;
            this.lblDestinationFile.Location = new System.Drawing.Point(12, 86);
            this.lblDestinationFile.Name = "lblDestinationFile";
            this.lblDestinationFile.Size = new System.Drawing.Size(82, 13);
            this.lblDestinationFile.TabIndex = 2;
            this.lblDestinationFile.Text = "&Destination File:";
            // 
            // ss1
            // 
            this.ss1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss1Lbl1,
            this.ss1Pb1,
            this.ss1Lbl2});
            this.ss1.Location = new System.Drawing.Point(0, 526);
            this.ss1.Name = "ss1";
            this.ss1.Size = new System.Drawing.Size(980, 22);
            this.ss1.SizingGrip = false;
            this.ss1.TabIndex = 10;
            // 
            // ss1Lbl1
            // 
            this.ss1Lbl1.AutoSize = false;
            this.ss1Lbl1.Name = "ss1Lbl1";
            this.ss1Lbl1.Size = new System.Drawing.Size(50, 17);
            this.ss1Lbl1.Text = "0%";
            // 
            // ss1Pb1
            // 
            this.ss1Pb1.AutoSize = false;
            this.ss1Pb1.Name = "ss1Pb1";
            this.ss1Pb1.Size = new System.Drawing.Size(800, 16);
            // 
            // ss1Lbl2
            // 
            this.ss1Lbl2.AutoSize = false;
            this.ss1Lbl2.Name = "ss1Lbl2";
            this.ss1Lbl2.Size = new System.Drawing.Size(50, 17);
            this.ss1Lbl2.Text = "100%";
            // 
            // ss2
            // 
            this.ss2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ss2Lbl1});
            this.ss2.Location = new System.Drawing.Point(0, 504);
            this.ss2.Name = "ss2";
            this.ss2.Size = new System.Drawing.Size(980, 22);
            this.ss2.SizingGrip = false;
            this.ss2.TabIndex = 11;
            // 
            // ss2Lbl1
            // 
            this.ss2Lbl1.Name = "ss2Lbl1";
            this.ss2Lbl1.Size = new System.Drawing.Size(74, 17);
            this.ss2Lbl1.Text = "<SS2 LBL-1>";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(980, 24);
            this.menuStrip1.TabIndex = 12;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAbout,
            this.mnuApplication});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuAbout
            // 
            this.mnuAbout.Name = "mnuAbout";
            this.mnuAbout.Size = new System.Drawing.Size(135, 22);
            this.mnuAbout.Text = "&About";
            this.mnuAbout.Click += new System.EventHandler(this.mnuAbout_Click);
            // 
            // mnuApplication
            // 
            this.mnuApplication.Name = "mnuApplication";
            this.mnuApplication.Size = new System.Drawing.Size(135, 22);
            this.mnuApplication.Text = "Applicatio&n";
            this.mnuApplication.Click += new System.EventHandler(this.mnuApplication_Click);
            // 
            // rtb1
            // 
            this.rtb1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtb1.Location = new System.Drawing.Point(12, 152);
            this.rtb1.Name = "rtb1";
            this.rtb1.ReadOnly = true;
            this.rtb1.Size = new System.Drawing.Size(759, 263);
            this.rtb1.TabIndex = 15;
            this.rtb1.Text = "";
            this.rtb1.WordWrap = false;
            // 
            // rdoReadFirst1000Lines
            // 
            this.rdoReadFirst1000Lines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoReadFirst1000Lines.AutoSize = true;
            this.rdoReadFirst1000Lines.Location = new System.Drawing.Point(777, 233);
            this.rdoReadFirst1000Lines.Name = "rdoReadFirst1000Lines";
            this.rdoReadFirst1000Lines.Size = new System.Drawing.Size(137, 17);
            this.rdoReadFirst1000Lines.TabIndex = 7;
            this.rdoReadFirst1000Lines.Text = "&1 Read First 1000 Lines";
            this.rdoReadFirst1000Lines.UseVisualStyleBackColor = true;
            this.rdoReadFirst1000Lines.CheckedChanged += new System.EventHandler(this.rdoReadFirst10Lines_CheckedChanged);
            this.rdoReadFirst1000Lines.Click += new System.EventHandler(this.rdoReadFirst10Lines_Click);
            // 
            // rdoReadLast1000Lines
            // 
            this.rdoReadLast1000Lines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoReadLast1000Lines.AutoSize = true;
            this.rdoReadLast1000Lines.Location = new System.Drawing.Point(777, 256);
            this.rdoReadLast1000Lines.Name = "rdoReadLast1000Lines";
            this.rdoReadLast1000Lines.Size = new System.Drawing.Size(138, 17);
            this.rdoReadLast1000Lines.TabIndex = 8;
            this.rdoReadLast1000Lines.Text = "&2 Read Last 1000 Lines";
            this.rdoReadLast1000Lines.UseVisualStyleBackColor = true;
            this.rdoReadLast1000Lines.CheckedChanged += new System.EventHandler(this.rdoReadLast10Lines_CheckedChanged);
            this.rdoReadLast1000Lines.Click += new System.EventHandler(this.rdoReadLast10Lines_Click);
            // 
            // btnClearText
            // 
            this.btnClearText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearText.Location = new System.Drawing.Point(636, 421);
            this.btnClearText.Name = "btnClearText";
            this.btnClearText.Size = new System.Drawing.Size(135, 46);
            this.btnClearText.TabIndex = 14;
            this.btnClearText.Text = "&Clear Text";
            this.btnClearText.UseVisualStyleBackColor = true;
            this.btnClearText.Click += new System.EventHandler(this.btnClearText_Click);
            // 
            // rdoReadCustomLines
            // 
            this.rdoReadCustomLines.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rdoReadCustomLines.AutoSize = true;
            this.rdoReadCustomLines.Checked = true;
            this.rdoReadCustomLines.Location = new System.Drawing.Point(777, 279);
            this.rdoReadCustomLines.Name = "rdoReadCustomLines";
            this.rdoReadCustomLines.Size = new System.Drawing.Size(126, 17);
            this.rdoReadCustomLines.TabIndex = 9;
            this.rdoReadCustomLines.TabStop = true;
            this.rdoReadCustomLines.Text = "&3 Read Custom Lines";
            this.rdoReadCustomLines.UseVisualStyleBackColor = true;
            this.rdoReadCustomLines.CheckedChanged += new System.EventHandler(this.rdoReadCustomLines_CheckedChanged);
            this.rdoReadCustomLines.Click += new System.EventHandler(this.rdoReadCustomLines_Click);
            // 
            // txtFromRCL
            // 
            this.txtFromRCL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtFromRCL.Location = new System.Drawing.Point(844, 308);
            this.txtFromRCL.Name = "txtFromRCL";
            this.txtFromRCL.Size = new System.Drawing.Size(96, 20);
            this.txtFromRCL.TabIndex = 11;
            this.txtFromRCL.Enter += new System.EventHandler(this.txtFromRCL_Enter);
            this.txtFromRCL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtFromRCL_KeyUp);
            // 
            // txtToRCL
            // 
            this.txtToRCL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtToRCL.Location = new System.Drawing.Point(844, 334);
            this.txtToRCL.Name = "txtToRCL";
            this.txtToRCL.Size = new System.Drawing.Size(96, 20);
            this.txtToRCL.TabIndex = 13;
            this.txtToRCL.Enter += new System.EventHandler(this.txtToRCL_Enter);
            this.txtToRCL.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtToRCL_KeyUp);
            // 
            // lblFromRCL
            // 
            this.lblFromRCL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFromRCL.AutoSize = true;
            this.lblFromRCL.Location = new System.Drawing.Point(805, 311);
            this.lblFromRCL.Name = "lblFromRCL";
            this.lblFromRCL.Size = new System.Drawing.Size(33, 13);
            this.lblFromRCL.TabIndex = 10;
            this.lblFromRCL.Text = "F&rom:";
            // 
            // lblToRCL
            // 
            this.lblToRCL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToRCL.AutoSize = true;
            this.lblToRCL.Location = new System.Drawing.Point(815, 337);
            this.lblToRCL.Name = "lblToRCL";
            this.lblToRCL.Size = new System.Drawing.Size(23, 13);
            this.lblToRCL.TabIndex = 12;
            this.lblToRCL.Text = "T&o:";
            // 
            // chkModifyData
            // 
            this.chkModifyData.AutoSize = true;
            this.chkModifyData.Location = new System.Drawing.Point(12, 129);
            this.chkModifyData.Name = "chkModifyData";
            this.chkModifyData.Size = new System.Drawing.Size(81, 17);
            this.chkModifyData.TabIndex = 4;
            this.chkModifyData.Text = "&Modify data";
            this.chkModifyData.UseVisualStyleBackColor = true;
            this.chkModifyData.CheckedChanged += new System.EventHandler(this.chkModifyData_CheckedChanged);
            // 
            // btnRemoveHeader
            // 
            this.btnRemoveHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveHeader.Location = new System.Drawing.Point(777, 152);
            this.btnRemoveHeader.Name = "btnRemoveHeader";
            this.btnRemoveHeader.Size = new System.Drawing.Size(191, 23);
            this.btnRemoveHeader.TabIndex = 5;
            this.btnRemoveHeader.Text = "&Remove Header [Remove FirstLine]";
            this.btnRemoveHeader.UseVisualStyleBackColor = true;
            this.btnRemoveHeader.Click += new System.EventHandler(this.btnRemoveHeader_Click);
            // 
            // btnRemoveTrailer
            // 
            this.btnRemoveTrailer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemoveTrailer.Location = new System.Drawing.Point(777, 181);
            this.btnRemoveTrailer.Name = "btnRemoveTrailer";
            this.btnRemoveTrailer.Size = new System.Drawing.Size(191, 23);
            this.btnRemoveTrailer.TabIndex = 6;
            this.btnRemoveTrailer.Text = "Remove &Trailer [Remove LastLine]";
            this.btnRemoveTrailer.UseVisualStyleBackColor = true;
            this.btnRemoveTrailer.Click += new System.EventHandler(this.btnRemoveTrailer_Click);
            // 
            // frmCountandSplit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(980, 548);
            this.Controls.Add(this.btnRemoveTrailer);
            this.Controls.Add(this.btnRemoveHeader);
            this.Controls.Add(this.chkModifyData);
            this.Controls.Add(this.lblToRCL);
            this.Controls.Add(this.lblFromRCL);
            this.Controls.Add(this.txtToRCL);
            this.Controls.Add(this.txtFromRCL);
            this.Controls.Add(this.rdoReadCustomLines);
            this.Controls.Add(this.btnClearText);
            this.Controls.Add(this.rdoReadLast1000Lines);
            this.Controls.Add(this.rdoReadFirst1000Lines);
            this.Controls.Add(this.rtb1);
            this.Controls.Add(this.ss2);
            this.Controls.Add(this.ss1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.lblDestinationFile);
            this.Controls.Add(this.txtDestinationFile);
            this.Controls.Add(this.txtSourceFile);
            this.Controls.Add(this.btnSourceFile);
            this.Controls.Add(this.lblTotalLineCountValue);
            this.Controls.Add(this.lblTotalClaimCount);
            this.Controls.Add(this.btnSaveFile);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(996, 586);
            this.Name = "frmCountandSplit";
            this.Text = "GigaReader";
            this.Load += new System.EventHandler(this.CountandSplit_Load);
            this.ss1.ResumeLayout(false);
            this.ss1.PerformLayout();
            this.ss2.ResumeLayout(false);
            this.ss2.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblTotalLineCountValue;
        private System.Windows.Forms.Label lblTotalClaimCount;
        private System.Windows.Forms.Button btnSaveFile;
        private System.Windows.Forms.Button btnSourceFile;
        private System.Windows.Forms.TextBox txtSourceFile;
        private System.Windows.Forms.TextBox txtDestinationFile;
        private System.Windows.Forms.OpenFileDialog ofdSourceFile;
        private System.Windows.Forms.SaveFileDialog sfdDestinationFolder;
        private System.Windows.Forms.Label lblDestinationFile;
        private System.Windows.Forms.StatusStrip ss1;
        private System.Windows.Forms.ToolStripStatusLabel ss1Lbl1;
        private System.Windows.Forms.ToolStripProgressBar ss1Pb1;
        private System.Windows.Forms.ToolStripStatusLabel ss1Lbl2;
        private System.Windows.Forms.StatusStrip ss2;
        private System.Windows.Forms.ToolStripStatusLabel ss2Lbl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuAbout;
        private System.Windows.Forms.ToolStripMenuItem mnuApplication;
        private System.Windows.Forms.RichTextBox rtb1;
        private System.Windows.Forms.RadioButton rdoReadFirst1000Lines;
        private System.Windows.Forms.RadioButton rdoReadLast1000Lines;
        private System.Windows.Forms.Button btnClearText;
        private System.Windows.Forms.RadioButton rdoReadCustomLines;
        private System.Windows.Forms.TextBox txtFromRCL;
        private System.Windows.Forms.TextBox txtToRCL;
        private System.Windows.Forms.Label lblFromRCL;
        private System.Windows.Forms.Label lblToRCL;
        private System.Windows.Forms.CheckBox chkModifyData;
        private System.Windows.Forms.Button btnRemoveHeader;
        private System.Windows.Forms.Button btnRemoveTrailer;
    }
}