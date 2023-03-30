namespace LabelCreator
{
    partial class formEditor
    {
        /// <summary>
        ///Gerekli tasarımcı değişkeni.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///Kullanılan tüm kaynakları temizleyin.
        /// </summary>
        ///<param name="disposing">yönetilen kaynaklar dispose edilmeliyse doğru; aksi halde yanlış.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer üretilen kod

        /// <summary>
        /// Tasarımcı desteği için gerekli metot - bu metodun 
        ///içeriğini kod düzenleyici ile değiştirmeyin.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(formEditor));
            this.txtEditor = new System.Windows.Forms.RichTextBox();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.menuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.fileNewFile = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openText = new System.Windows.Forms.ToolStripMenuItem();
            this.savetryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.menuFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBoldFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItalicFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUnderlineFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSizeFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgOpen = new System.Windows.Forms.OpenFileDialog();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.dlgOpenImage = new System.Windows.Forms.OpenFileDialog();
            this.btnDynText = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.openPanel = new System.Windows.Forms.Button();
            this.Header = new System.Windows.Forms.Button();
            this.Standard = new System.Windows.Forms.Button();
            this.Generate = new System.Windows.Forms.Button();
            this.SubHeader = new System.Windows.Forms.Button();
            this.menuMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEditor
            // 
            this.txtEditor.AllowDrop = true;
            this.txtEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEditor.Location = new System.Drawing.Point(704, 150);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtEditor.Size = new System.Drawing.Size(122, 153);
            this.txtEditor.TabIndex = 1;
            this.txtEditor.Text = "";
            this.txtEditor.TextChanged += new System.EventHandler(this.txtEditor_TextChanged);
            // 
            // menuMain
            // 
            this.menuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFile,
            this.menuFormat});
            this.menuMain.Location = new System.Drawing.Point(1, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(102, 24);
            this.menuMain.TabIndex = 2;
            this.menuMain.Text = "menuStrip1";
            this.menuMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuMain_ItemClicked);
            // 
            // menuFile
            // 
            this.menuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileNewFile,
            this.openToolStripMenuItem,
            this.savetryToolStripMenuItem,
            this.fileExit});
            this.menuFile.Name = "menuFile";
            this.menuFile.Size = new System.Drawing.Size(37, 20);
            this.menuFile.Text = "&File";
            this.menuFile.Click += new System.EventHandler(this.menuFile_Click);
            // 
            // fileNewFile
            // 
            this.fileNewFile.Name = "fileNewFile";
            this.fileNewFile.Size = new System.Drawing.Size(119, 22);
            this.fileNewFile.Text = "&New File";
            this.fileNewFile.Click += new System.EventHandler(this.fileNewFile_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openText});
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.openToolStripMenuItem.Text = "&Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.menuFileOpen_Click);
            // 
            // openText
            // 
            this.openText.Name = "openText";
            this.openText.Size = new System.Drawing.Size(116, 22);
            this.openText.Text = "&Text File";
            this.openText.Click += new System.EventHandler(this.openTexFile_Click);
            // 
            // savetryToolStripMenuItem
            // 
            this.savetryToolStripMenuItem.Name = "savetryToolStripMenuItem";
            this.savetryToolStripMenuItem.Size = new System.Drawing.Size(119, 22);
            this.savetryToolStripMenuItem.Text = "Save";
            this.savetryToolStripMenuItem.Click += new System.EventHandler(this.savetryToolStripMenuItem_Click);
            // 
            // fileExit
            // 
            this.fileExit.Name = "fileExit";
            this.fileExit.Size = new System.Drawing.Size(119, 22);
            this.fileExit.Text = "Exit";
            this.fileExit.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // menuFormat
            // 
            this.menuFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuBoldFormat,
            this.menuItalicFormat,
            this.menuUnderlineFormat,
            this.menuSizeFormat});
            this.menuFormat.Name = "menuFormat";
            this.menuFormat.Size = new System.Drawing.Size(57, 20);
            this.menuFormat.Text = "F&ormat";
            this.menuFormat.Click += new System.EventHandler(this.menuFormat_Click);
            // 
            // menuBoldFormat
            // 
            this.menuBoldFormat.Name = "menuBoldFormat";
            this.menuBoldFormat.Size = new System.Drawing.Size(125, 22);
            this.menuBoldFormat.Text = "&Bold";
            this.menuBoldFormat.Click += new System.EventHandler(this.menuBoldFormat_Click);
            // 
            // menuItalicFormat
            // 
            this.menuItalicFormat.Name = "menuItalicFormat";
            this.menuItalicFormat.Size = new System.Drawing.Size(125, 22);
            this.menuItalicFormat.Text = "&Italic";
            this.menuItalicFormat.Click += new System.EventHandler(this.menuItalicFormat_Click);
            // 
            // menuUnderlineFormat
            // 
            this.menuUnderlineFormat.Name = "menuUnderlineFormat";
            this.menuUnderlineFormat.Size = new System.Drawing.Size(125, 22);
            this.menuUnderlineFormat.Text = "&Underline";
            this.menuUnderlineFormat.Click += new System.EventHandler(this.menuUnderlineFormat_Click);
            // 
            // menuSizeFormat
            // 
            this.menuSizeFormat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeSmall,
            this.sizeMedium,
            this.sizeLarge});
            this.menuSizeFormat.Name = "menuSizeFormat";
            this.menuSizeFormat.Size = new System.Drawing.Size(125, 22);
            this.menuSizeFormat.Text = "&Size";
            this.menuSizeFormat.Click += new System.EventHandler(this.menuSizeFormat_click);
            // 
            // sizeSmall
            // 
            this.sizeSmall.Checked = true;
            this.sizeSmall.CheckState = System.Windows.Forms.CheckState.Checked;
            this.sizeSmall.Name = "sizeSmall";
            this.sizeSmall.Size = new System.Drawing.Size(119, 22);
            this.sizeSmall.Text = "&Small";
            this.sizeSmall.Click += new System.EventHandler(this.menuSizeFormat_click);
            // 
            // sizeMedium
            // 
            this.sizeMedium.Name = "sizeMedium";
            this.sizeMedium.Size = new System.Drawing.Size(119, 22);
            this.sizeMedium.Text = "&Medium";
            this.sizeMedium.Click += new System.EventHandler(this.menuSizeFormat_click);
            // 
            // sizeLarge
            // 
            this.sizeLarge.Name = "sizeLarge";
            this.sizeLarge.Size = new System.Drawing.Size(119, 22);
            this.sizeLarge.Text = "&Large";
            this.sizeLarge.Click += new System.EventHandler(this.menuSizeFormat_click);
            // 
            // dlgOpen
            // 
            this.dlgOpen.FileName = "openFileDialog1";
            this.dlgOpen.Filter = "Text Files (*txt)|*.txt";
            this.dlgOpen.Title = "Open File";
            // 
            // dlgSave
            // 
            this.dlgSave.Filter = "PDF files (*.pdf)|*.pdf";
            this.dlgSave.Title = "Save File";
            // 
            // dlgOpenImage
            // 
            this.dlgOpenImage.FileName = "openFileDialog1";
            this.dlgOpenImage.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            this.dlgOpenImage.Title = "Open Image";
            // 
            // btnDynText
            // 
            this.btnDynText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDynText.Location = new System.Drawing.Point(704, 349);
            this.btnDynText.Name = "btnDynText";
            this.btnDynText.Size = new System.Drawing.Size(100, 33);
            this.btnDynText.TabIndex = 4;
            this.btnDynText.Text = "New Text";
            this.btnDynText.UseVisualStyleBackColor = true;
            this.btnDynText.Click += new System.EventHandler(this.btnDynText_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(704, 388);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 33);
            this.button2.TabIndex = 5;
            this.button2.Text = "New Image";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // openPanel
            // 
            this.openPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openPanel.Location = new System.Drawing.Point(704, 309);
            this.openPanel.Name = "openPanel";
            this.openPanel.Size = new System.Drawing.Size(100, 34);
            this.openPanel.TabIndex = 8;
            this.openPanel.Text = "New Project";
            this.openPanel.UseVisualStyleBackColor = true;
            this.openPanel.Click += new System.EventHandler(this.button1_Click);
            // 
            // Header
            // 
            this.Header.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Header.Location = new System.Drawing.Point(704, 27);
            this.Header.Name = "Header";
            this.Header.Size = new System.Drawing.Size(122, 35);
            this.Header.TabIndex = 11;
            this.Header.Text = "Header";
            this.Header.UseVisualStyleBackColor = true;
            this.Header.Click += new System.EventHandler(this.Header_Click);
            // 
            // Standard
            // 
            this.Standard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Standard.Location = new System.Drawing.Point(704, 109);
            this.Standard.Name = "Standard";
            this.Standard.Size = new System.Drawing.Size(122, 35);
            this.Standard.TabIndex = 12;
            this.Standard.Text = "Standard";
            this.Standard.UseVisualStyleBackColor = true;
            this.Standard.Click += new System.EventHandler(this.Standard_Click);
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate.Location = new System.Drawing.Point(704, 427);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(100, 31);
            this.Generate.TabIndex = 13;
            this.Generate.Text = "Generate";
            this.Generate.UseVisualStyleBackColor = true;
            this.Generate.Click += new System.EventHandler(this.Generate_Click);
            // 
            // SubHeader
            // 
            this.SubHeader.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SubHeader.Location = new System.Drawing.Point(704, 68);
            this.SubHeader.Name = "SubHeader";
            this.SubHeader.Size = new System.Drawing.Size(122, 35);
            this.SubHeader.TabIndex = 14;
            this.SubHeader.Text = "SubHeader";
            this.SubHeader.UseVisualStyleBackColor = true;
            this.SubHeader.Click += new System.EventHandler(this.SubHeader_Click);
            // 
            // formEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(945, 749);
            this.Controls.Add(this.SubHeader);
            this.Controls.Add(this.Generate);
            this.Controls.Add(this.Standard);
            this.Controls.Add(this.Header);
            this.Controls.Add(this.openPanel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.txtEditor);
            this.Controls.Add(this.btnDynText);
            this.Controls.Add(this.menuMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuMain;
            this.Name = "formEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formEditor_FormClosing);
            this.Load += new System.EventHandler(this.formEditor_Load);
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtEditor;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStripMenuItem menuFile;
        private System.Windows.Forms.ToolStripMenuItem fileNewFile;
        private System.Windows.Forms.ToolStripMenuItem fileExit;
        private System.Windows.Forms.ToolStripMenuItem menuFormat;
        private System.Windows.Forms.ToolStripMenuItem menuBoldFormat;
        private System.Windows.Forms.ToolStripMenuItem menuItalicFormat;
        private System.Windows.Forms.ToolStripMenuItem menuUnderlineFormat;
        private System.Windows.Forms.ToolStripMenuItem menuSizeFormat;
        private System.Windows.Forms.ToolStripMenuItem sizeSmall;
        private System.Windows.Forms.ToolStripMenuItem sizeMedium;
        private System.Windows.Forms.ToolStripMenuItem sizeLarge;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.ToolStripMenuItem openText;
        private System.Windows.Forms.OpenFileDialog dlgOpenImage;
        private System.Windows.Forms.Button btnDynText;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button openPanel;
        private System.Windows.Forms.ToolStripMenuItem savetryToolStripMenuItem;
        private System.Windows.Forms.Button Header;
        private System.Windows.Forms.Button Standard;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Button SubHeader;
    }
}

