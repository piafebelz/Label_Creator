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
            this.newBarcode = new System.Windows.Forms.Button();
            this.newLine = new System.Windows.Forms.Button();
            this.Rotate = new System.Windows.Forms.Button();
            this.menuFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBoldFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItalicFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuUnderlineFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSizeFormat = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeSmall = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeMedium = new System.Windows.Forms.ToolStripMenuItem();
            this.sizeLarge = new System.Windows.Forms.ToolStripMenuItem();
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newFile = new System.Windows.Forms.ToolStripButton();
            this.openTextFile = new System.Windows.Forms.ToolStripButton();
            this.Save = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.Font = new System.Windows.Forms.ToolStripButton();
            this.cutToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.boldFont = new System.Windows.Forms.ToolStripButton();
            this.italicFont = new System.Windows.Forms.ToolStripButton();
            this.underlineFont = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.leftAlign = new System.Windows.Forms.ToolStripButton();
            this.centerAlign = new System.Windows.Forms.ToolStripButton();
            this.rightAlign = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.alignToTop = new System.Windows.Forms.ToolStripButton();
            this.alignToCenter = new System.Windows.Forms.ToolStripButton();
            this.alignToBottom = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exit = new System.Windows.Forms.ToolStripButton();
            this.fontDialog = new System.Windows.Forms.FontDialog();
            this.newCode128Barcode = new System.Windows.Forms.Button();
            this.newLabel = new System.Windows.Forms.Button();
            this.menuMain.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtEditor
            // 
            this.txtEditor.AllowDrop = true;
            this.txtEditor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtEditor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtEditor.Location = new System.Drawing.Point(704, 150);
            this.txtEditor.Name = "txtEditor";
            this.txtEditor.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.txtEditor.Size = new System.Drawing.Size(134, 153);
            this.txtEditor.TabIndex = 1;
            this.txtEditor.Text = "";
            this.txtEditor.TextChanged += new System.EventHandler(this.txtEditor_TextChanged);
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
            this.btnDynText.Location = new System.Drawing.Point(766, 309);
            this.btnDynText.Name = "btnDynText";
            this.btnDynText.Size = new System.Drawing.Size(72, 42);
            this.btnDynText.TabIndex = 4;
            this.btnDynText.Text = "New Text";
            this.btnDynText.UseVisualStyleBackColor = true;
            this.btnDynText.Click += new System.EventHandler(this.btnDynText_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(704, 357);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(57, 37);
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
            this.openPanel.Size = new System.Drawing.Size(57, 42);
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
            this.Header.Size = new System.Drawing.Size(134, 35);
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
            this.Standard.Size = new System.Drawing.Size(134, 35);
            this.Standard.TabIndex = 12;
            this.Standard.Text = "Standard";
            this.Standard.UseVisualStyleBackColor = true;
            this.Standard.Click += new System.EventHandler(this.Standard_Click);
            // 
            // Generate
            // 
            this.Generate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Generate.Location = new System.Drawing.Point(704, 501);
            this.Generate.Name = "Generate";
            this.Generate.Size = new System.Drawing.Size(134, 31);
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
            this.SubHeader.Size = new System.Drawing.Size(134, 35);
            this.SubHeader.TabIndex = 14;
            this.SubHeader.Text = "SubHeader";
            this.SubHeader.UseVisualStyleBackColor = true;
            this.SubHeader.Click += new System.EventHandler(this.SubHeader_Click);
            // 
            // newBarcode
            // 
            this.newBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newBarcode.Location = new System.Drawing.Point(704, 444);
            this.newBarcode.Name = "newBarcode";
            this.newBarcode.Size = new System.Drawing.Size(57, 51);
            this.newBarcode.TabIndex = 15;
            this.newBarcode.Text = "New QR Barcode";
            this.newBarcode.UseVisualStyleBackColor = true;
            this.newBarcode.Click += new System.EventHandler(this.newBarcode_Click);
            // 
            // newLine
            // 
            this.newLine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newLine.Location = new System.Drawing.Point(704, 400);
            this.newLine.Name = "newLine";
            this.newLine.Size = new System.Drawing.Size(57, 38);
            this.newLine.TabIndex = 16;
            this.newLine.Text = "New Line";
            this.newLine.UseVisualStyleBackColor = true;
            this.newLine.Click += new System.EventHandler(this.newLine_Click);
            // 
            // Rotate
            // 
            this.Rotate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Rotate.Location = new System.Drawing.Point(766, 401);
            this.Rotate.Name = "Rotate";
            this.Rotate.Size = new System.Drawing.Size(72, 38);
            this.Rotate.TabIndex = 17;
            this.Rotate.Text = "Rotate";
            this.Rotate.UseVisualStyleBackColor = true;
            this.Rotate.Click += new System.EventHandler(this.Rotate_Click);
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
            // menuMain
            // 
            this.menuMain.Dock = System.Windows.Forms.DockStyle.None;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuFormat});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Size = new System.Drawing.Size(65, 24);
            this.menuMain.TabIndex = 2;
            this.menuMain.Text = "menuStrip1";
            this.menuMain.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuMain_ItemClicked);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newFile,
            this.openTextFile,
            this.Save,
            this.toolStripSeparator,
            this.Font,
            this.cutToolStripButton,
            this.toolStripSeparator1,
            this.boldFont,
            this.italicFont,
            this.underlineFont,
            this.toolStripSeparator2,
            this.leftAlign,
            this.centerAlign,
            this.rightAlign,
            this.toolStripSeparator4,
            this.alignToTop,
            this.alignToCenter,
            this.alignToBottom,
            this.toolStripSeparator3,
            this.exit});
            this.toolStrip1.Location = new System.Drawing.Point(65, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(387, 25);
            this.toolStrip1.TabIndex = 18;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // newFile
            // 
            this.newFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.newFile.Image = ((System.Drawing.Image)(resources.GetObject("newFile.Image")));
            this.newFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newFile.Name = "newFile";
            this.newFile.Size = new System.Drawing.Size(23, 22);
            this.newFile.Text = "&New";
            this.newFile.Click += new System.EventHandler(this.newFile_Click);
            // 
            // openTextFile
            // 
            this.openTextFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openTextFile.Image = ((System.Drawing.Image)(resources.GetObject("openTextFile.Image")));
            this.openTextFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openTextFile.Name = "openTextFile";
            this.openTextFile.Size = new System.Drawing.Size(23, 22);
            this.openTextFile.Text = "&Open";
            this.openTextFile.Click += new System.EventHandler(this.openTextFile_Click);
            // 
            // Save
            // 
            this.Save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Save.Image = ((System.Drawing.Image)(resources.GetObject("Save.Image")));
            this.Save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Save.Name = "Save";
            this.Save.Size = new System.Drawing.Size(23, 22);
            this.Save.Text = "&Save";
            this.Save.Click += new System.EventHandler(this.Save_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // Font
            // 
            this.Font.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.Font.Image = ((System.Drawing.Image)(resources.GetObject("Font.Image")));
            this.Font.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.Font.Name = "Font";
            this.Font.Size = new System.Drawing.Size(23, 22);
            this.Font.Text = "Detailed Font Settings...";
            this.Font.Click += new System.EventHandler(this.Font_Click);
            // 
            // cutToolStripButton
            // 
            this.cutToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.cutToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("cutToolStripButton.Image")));
            this.cutToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.cutToolStripButton.Name = "cutToolStripButton";
            this.cutToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.cutToolStripButton.Text = "D&elete";
            this.cutToolStripButton.Click += new System.EventHandler(this.cutToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // boldFont
            // 
            this.boldFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.boldFont.Image = ((System.Drawing.Image)(resources.GetObject("boldFont.Image")));
            this.boldFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.boldFont.Name = "boldFont";
            this.boldFont.Size = new System.Drawing.Size(23, 22);
            this.boldFont.Text = "&Bold";
            this.boldFont.Click += new System.EventHandler(this.boldFont_Click);
            // 
            // italicFont
            // 
            this.italicFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.italicFont.Image = ((System.Drawing.Image)(resources.GetObject("italicFont.Image")));
            this.italicFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.italicFont.Name = "italicFont";
            this.italicFont.Size = new System.Drawing.Size(23, 22);
            this.italicFont.Text = "&Italic";
            this.italicFont.Click += new System.EventHandler(this.italicFont_Click);
            // 
            // underlineFont
            // 
            this.underlineFont.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.underlineFont.Image = ((System.Drawing.Image)(resources.GetObject("underlineFont.Image")));
            this.underlineFont.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.underlineFont.Name = "underlineFont";
            this.underlineFont.Size = new System.Drawing.Size(23, 22);
            this.underlineFont.Text = "&Underline";
            this.underlineFont.Click += new System.EventHandler(this.underlineFont_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // leftAlign
            // 
            this.leftAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.leftAlign.Image = ((System.Drawing.Image)(resources.GetObject("leftAlign.Image")));
            this.leftAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.leftAlign.Name = "leftAlign";
            this.leftAlign.Size = new System.Drawing.Size(23, 22);
            this.leftAlign.Text = "&Left Align";
            this.leftAlign.Click += new System.EventHandler(this.leftAlign_Click);
            // 
            // centerAlign
            // 
            this.centerAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.centerAlign.Image = ((System.Drawing.Image)(resources.GetObject("centerAlign.Image")));
            this.centerAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.centerAlign.Name = "centerAlign";
            this.centerAlign.Size = new System.Drawing.Size(23, 22);
            this.centerAlign.Text = "&Center Align";
            this.centerAlign.Click += new System.EventHandler(this.centerAlign_Click);
            // 
            // rightAlign
            // 
            this.rightAlign.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.rightAlign.Image = ((System.Drawing.Image)(resources.GetObject("rightAlign.Image")));
            this.rightAlign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.rightAlign.Name = "rightAlign";
            this.rightAlign.Size = new System.Drawing.Size(23, 22);
            this.rightAlign.Text = "&Right Align";
            this.rightAlign.Click += new System.EventHandler(this.rightAlign_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // alignToTop
            // 
            this.alignToTop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignToTop.Image = ((System.Drawing.Image)(resources.GetObject("alignToTop.Image")));
            this.alignToTop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignToTop.Name = "alignToTop";
            this.alignToTop.Size = new System.Drawing.Size(23, 22);
            this.alignToTop.Text = "Top Align";
            this.alignToTop.Click += new System.EventHandler(this.alignToTop_Click);
            // 
            // alignToCenter
            // 
            this.alignToCenter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignToCenter.Image = ((System.Drawing.Image)(resources.GetObject("alignToCenter.Image")));
            this.alignToCenter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignToCenter.Name = "alignToCenter";
            this.alignToCenter.Size = new System.Drawing.Size(23, 22);
            this.alignToCenter.Text = "Center Align";
            this.alignToCenter.Click += new System.EventHandler(this.alignToCenter_Click);
            // 
            // alignToBottom
            // 
            this.alignToBottom.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.alignToBottom.Image = ((System.Drawing.Image)(resources.GetObject("alignToBottom.Image")));
            this.alignToBottom.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.alignToBottom.Name = "alignToBottom";
            this.alignToBottom.Size = new System.Drawing.Size(23, 22);
            this.alignToBottom.Text = "Bottom Align";
            this.alignToBottom.Click += new System.EventHandler(this.alignToBottom_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // exit
            // 
            this.exit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.exit.Image = ((System.Drawing.Image)(resources.GetObject("exit.Image")));
            this.exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(23, 22);
            this.exit.Text = "&Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // newCode128Barcode
            // 
            this.newCode128Barcode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newCode128Barcode.Location = new System.Drawing.Point(766, 444);
            this.newCode128Barcode.Name = "newCode128Barcode";
            this.newCode128Barcode.Size = new System.Drawing.Size(72, 51);
            this.newCode128Barcode.TabIndex = 20;
            this.newCode128Barcode.Text = "New Code-128 Barcode";
            this.newCode128Barcode.UseVisualStyleBackColor = true;
            this.newCode128Barcode.Click += new System.EventHandler(this.newCode128Barcode_Click);
            // 
            // newLabel
            // 
            this.newLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.newLabel.Location = new System.Drawing.Point(766, 357);
            this.newLabel.Name = "newLabel";
            this.newLabel.Size = new System.Drawing.Size(72, 37);
            this.newLabel.TabIndex = 21;
            this.newLabel.Text = "New Static Image";
            this.newLabel.UseVisualStyleBackColor = true;
            this.newLabel.Click += new System.EventHandler(this.newLabel_Click);
            // 
            // formEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(945, 749);
            this.Controls.Add(this.newLabel);
            this.Controls.Add(this.newCode128Barcode);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.Rotate);
            this.Controls.Add(this.newLine);
            this.Controls.Add(this.newBarcode);
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
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox txtEditor;
        private System.Windows.Forms.OpenFileDialog dlgOpen;
        private System.Windows.Forms.SaveFileDialog dlgSave;
        private System.Windows.Forms.OpenFileDialog dlgOpenImage;
        private System.Windows.Forms.Button btnDynText;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button openPanel;
        private System.Windows.Forms.Button Header;
        private System.Windows.Forms.Button Standard;
        private System.Windows.Forms.Button Generate;
        private System.Windows.Forms.Button SubHeader;
        private System.Windows.Forms.Button newBarcode;
        private System.Windows.Forms.Button newLine;
        private System.Windows.Forms.Button Rotate;
        private System.Windows.Forms.ToolStripMenuItem menuFormat;
        private System.Windows.Forms.ToolStripMenuItem menuSizeFormat;
        private System.Windows.Forms.ToolStripMenuItem sizeSmall;
        private System.Windows.Forms.ToolStripMenuItem sizeMedium;
        private System.Windows.Forms.ToolStripMenuItem sizeLarge;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newFile;
        private System.Windows.Forms.ToolStripButton openTextFile;
        private System.Windows.Forms.ToolStripButton Save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton cutToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton boldFont;
        private System.Windows.Forms.ToolStripButton italicFont;
        private System.Windows.Forms.ToolStripButton underlineFont;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton leftAlign;
        private System.Windows.Forms.ToolStripButton centerAlign;
        private System.Windows.Forms.ToolStripButton rightAlign;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton exit;
        private System.Windows.Forms.ToolStripButton Font;
        private System.Windows.Forms.FontDialog fontDialog;
        private System.Windows.Forms.ToolStripMenuItem menuBoldFormat;
        private System.Windows.Forms.ToolStripMenuItem menuItalicFormat;
        private System.Windows.Forms.ToolStripMenuItem menuUnderlineFormat;
        private System.Windows.Forms.ToolStripButton alignToTop;
        private System.Windows.Forms.ToolStripButton alignToCenter;
        private System.Windows.Forms.ToolStripButton alignToBottom;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.Button newCode128Barcode;
        private System.Windows.Forms.Button newLabel;
    }
}

