using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Shapes;
using ZXing;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Windows.Documents;
using static LabelCreator.formEditor;
using iTextSharp.text.pdf;
using System.Linq;

namespace LabelCreator
{
    public partial class formEditor : Form
    {
        LabelDTO label = null;
        private bool isDrawingLine = false;
        private Point startPoint;
        private Point endPoint;
        System.Drawing.Font headerFont;
        System.Drawing.Font subHeaderFont;
        System.Drawing.Font standardFont;
        private Control lastClicked;
        private string itemCode;
        private Tile2 lastClickedTextBox;
        private Control previousClicked;
        private string result;
        private Control tempClicked;
        private Line currentLine;
        Graphics myGraphics;
        public Guid ID { get; private set; }
        private List<Tile2> richTextBoxList = new List<Tile2>();
        private Panel panel1;

        public formEditor()
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                control.MouseDown += Control_MouseDown;
            }
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            lastClicked = (Control)sender;
            if (!(sender is Button))
            {
                previousClicked = lastClicked;
            }
        }

        private void formEditor_Load(object sender, EventArgs e)
        {
            openSavedData();

            foreach (Tile2 rtb in richTextBoxList)
            {
                rtb.SelectionChanged += richTextBox_SelectionChanged;
            }

            ChangeFont(txtEditor);
        }

        public void btnDynText_Click(object sender, EventArgs e)
        {
            
            menuBoldFormat.Checked = false;
            menuItalicFormat.Checked = false;
            menuUnderlineFormat.Checked = false;
            var initialText = "Write Here";

            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first");
                return;
            }

            Tile2 txtbox = new Tile2();
            txtbox.MouseDown += Control_MouseDown;
            var newItem = new LabelItemDTO
            {
                Font = new System.Drawing.Font(fontName, fontSize, FontStyle.Regular),
                ItemType = LabelItemType.RichText,
                Value = initialText,
                Y = 70,
                X = 415,
                Width = 200,
                Height = 50,
                ID = txtbox.ID,
                Angle = 0,
                ItemReference = txtbox
            };
            txtbox.Visible = true;
            

            this.Controls.Add(txtbox);

            txtbox.MouseDown += TextBox_MouseDown;

            txtbox.BorderStyle = BorderStyle.None;
            txtbox.BringToFront();
            txtbox.Top = newItem.Y;
            txtbox.Left = newItem.X;
            txtbox.Font = newItem.Font;
            txtbox.Size = new Size(newItem.Width, newItem.Height);
            txtbox.Text = initialText;
            txtbox.BorderStyle = BorderStyle.FixedSingle;

            this.label.LabelItems.Add(newItem);
        }

        // Exit button
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private bool newFileIsClicked = false;

        // Creates a new file
        private void fileNewFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start a new file?", "New File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                newFileIsClicked = true;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\my_file.txt";
                File.WriteAllText(path, String.Empty);
                txtEditor.Text = "";
                label.LabelItems.Clear();
                foreach (Control c in Controls)
                {
                    Action<Control.ControlCollection> func = null;

                    func = (controls) =>
                    {
                        foreach (Control control in controls)
                            if (control is Tile || control is Tile2 || control is Panel)
                                this.Controls.Remove(control);
                            else
                                func(control.Controls);
                    };

                    func(Controls);
                }
            }
        }
        private void menuBoldFormat_Click(object sender, EventArgs e)
        {

            menuBoldFormat.Checked = !menuBoldFormat.Checked;

            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                ChangeFont(obj);
            }
        }

        private void menuItalicFormat_Click(object sender, EventArgs e)
        {
            menuItalicFormat.Checked = !menuItalicFormat.Checked;
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                ChangeFont(obj);
            }

        }
        private void menuUnderlineFormat_Click(object sender, EventArgs e)
        {
            menuUnderlineFormat.Checked = !menuUnderlineFormat.Checked;
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }
 
               ChangeFont(obj);

            }
        }

        private void menuSizeFormat_click(object sender, EventArgs e)
        {
            string sizeClicked = ((ToolStripMenuItem)sender).Text;
            sizeSmall.Checked = false;
            sizeMedium.Checked = false;
            sizeLarge.Checked = false;

            switch (sizeClicked)
            {

                case "&Small":
                    fontSize = 8;
                    sizeSmall.Checked = true;
                    break;
                case "&Medium":
                    fontSize = 12;
                    sizeMedium.Checked = true;
                    break;
                case "&Large":
                    fontSize = 18;
                    sizeLarge.Checked = true;
                    break;

            }
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                ChangeFont(obj);
            }
        }

        private void formEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            StreamWriter outputFile = new StreamWriter(Application.StartupPath + "\\note.ini");
            outputFile.WriteLine(menuBoldFormat.Checked);
            outputFile.WriteLine(menuItalicFormat.Checked);
            outputFile.WriteLine(menuUnderlineFormat.Checked);
            if (sizeSmall.Checked)
                outputFile.WriteLine(1);
            else if (sizeMedium.Checked)
                outputFile.WriteLine(2);
            else
                outputFile.WriteLine(3);

            outputFile.Close();
        }

        private void menuFileOpen_Click(object sender, EventArgs e)
        {

        }

        //Opens a text file from the computer. Text is shown in the textbox fixed to the form
        private void openTexFile_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                StreamReader inputFile = new StreamReader(dlgOpen.FileName);
                txtEditor.Text = inputFile.ReadToEnd();
                txtEditor.BringToFront();
                inputFile.Close();
                txtEditor.SelectionLength = 0;
            }
        }

        // New Image button
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first.");
                return;
            }

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Tile picBox = new Tile();
                this.Controls.Add(picBox);
                picBox.MouseDown += Control_MouseDown;
                picBox.BorderStyle = BorderStyle.FixedSingle;
                picBox.SizeMode = PictureBoxSizeMode.Zoom;
                picBox.BackColor = Color.Transparent;
                picBox.ImageLocation = ofd.FileName;
                var image = System.Drawing.Image.FromFile(picBox.ImageLocation);
                var base64 = image.ToBase64();

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Jpeg);
                    base64 = Convert.ToBase64String(ms.ToArray());
                }

                var newImageItem = new LabelItemDTO
                {
                    Font = null,
                    ItemType = LabelItemType.Image,
                    Value = base64,
                    Y = 70,
                    X = 415,
                    Width = picBox.Width,
                    Height = picBox.Height,
                    ID = picBox.ID,
                    Angle = 0,
                    ItemReference = picBox
                };

                picBox.BringToFront();
                picBox.Top = newImageItem.Y;
                picBox.Left = newImageItem.X;
                picBox.Size = new Size(newImageItem.Width, newImageItem.Height);

                this.label.LabelItems.Add(newImageItem);
            }
        }

        private void txtEditor_TextChanged(object sender, EventArgs e)
        {

        }


        // New Project button. Creates plain white background for the label with the size taken as an input
        private void button1_Click(object sender, EventArgs e)
        {

            string message, title, defaultValue;
            string myValue;
            message = "Please enter the size needed (in mm) :";
            title = "New Project";
            defaultValue = "630,891";
            myValue = Interaction.InputBox(message, title, defaultValue);

            if ((string)myValue == "")
            {
                myValue = defaultValue;
                Interaction.MsgBox("myValue = " + myValue.ToString(), MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "msgbox demonstration");

            }

            var coords = myValue.Split(',');
            var xcoord = Int32.Parse(coords[0]);
            var ycoord = Int32.Parse(coords[1]);

            this.label = new LabelDTO
            {
                BackgroundColor = Color.White,
                PageHeight = ycoord,
                PageWidth = xcoord,
                LabelItems = new List<LabelItemDTO>()
            };

            panel1 = new Panel();
            this.Controls.Add(panel1);
            panel1.Location = new Point(5, 30);
            panel1.BackColor = Color.White;
            panel1.Size = new Size(label.PageWidth, label.PageHeight);
            myGraphics = panel1.CreateGraphics();
        }

        private void menuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            menuMain.BringToFront();
        }

        private void menuFormat_Click(object sender, EventArgs e)
        {

        }

        private void Header_Click(object sender, EventArgs e)
        {
            menuBoldFormat.Checked = true;
            fontSize = 18;
            sizeLarge.Checked = true;
            headerFont = new System.Drawing.Font("MS Sans Serif", 20, FontStyle.Bold);
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                if (lastClickedTextBox != null)
                {
                    lastClickedTextBox.Font = headerFont;
                }
                ChangeFont(lastClickedTextBox);
            }

        }

        private void Standard_Click(object sender, EventArgs e)
        {
            standardFont = new System.Drawing.Font("MS Sans Serif", 12, FontStyle.Regular);
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                if (lastClickedTextBox != null)
                {
                    lastClickedTextBox.Font = standardFont;
                }
            }
        }


        private void Generate_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first");
                return;
            }
            List<string> mystrlist = new List<string> { };
            Form messageForm = new Form();
            messageForm.Size = new System.Drawing.Size(400, 200);
            messageForm.Text = "Generated Code";

            RichTextBox messageTextBox = new RichTextBox();
            messageTextBox.Size = new System.Drawing.Size(375, 150);
            messageTextBox.Multiline = true;
            messageTextBox.ReadOnly = true;

            messageTextBox.Text = "using System.Drawing;\r\nusing System.Drawing.Drawing2D;\r\nusing System.Drawing.Imaging;\r\nusing ZXing;\r\nusing ZXing.Windows.Compatibility;\r\n\r\nnamespace denemeapp\r\n{\r\n    public class LabelBuilderInputDTO\r\n    {\r\n       }\r\n\r\n    public static class LabelBuilder\r\n    {\r\n        private static void AddLine(Graphics graphics, Pen pen, PointF point1, PointF point2)\r\n        {\r\n            graphics.DrawLine(pen, point1, point2);\r\n        }\r\nprivate static void AddImage(Graphics graphics, System.Drawing.Image image, float width, float height, float x, float y)\r\n        {\r\n            graphics.DrawImage(image, new RectangleF\r\n            {\r\n                Width = width,\r\n                Height = height,\r\n                X = x,\r\n                Y = y\r\n            });\r\n        }\r\nprivate Bitmap GenerateQR(BarcodeFormat barcodeFormat, int width, int height, string value)\r\n        {\r\n            var barcodeWriter = new BarcodeWriter<Bitmap>();\r\n            var renderer = new BitmapRenderer();\r\n            barcodeWriter.Format = barcodeFormat;\r\n            barcodeWriter.Options.Height = height;\r\n            barcodeWriter.Options.Width = width;\r\n            barcodeWriter.Options.PureBarcode = false;\r\n            barcodeWriter.Renderer = renderer;\r\n            var barcode = barcodeWriter.Write(value);\r\n            return barcode;\r\n        }\r\n        public static System.Drawing.Image Base64ToImage(string base64String)\r\n        {\r\n            byte[] imageBytes = Convert.FromBase64String(base64String);\r\n\r\n            using (var ms = new MemoryStream(imageBytes))\r\n            {\r\n                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);\r\n                return image;\r\n            }\r\n        }\r\n\r\n        private static void AddText(Graphics graphic, Rectangle rectangle, string value, TextType textType, bool fromTop = false)\r\n        {\r\n            graphic.FillRectangle(Brushes.White, rectangle);\r\n            StringFormat sf = new StringFormat();\r\n            sf.LineAlignment = fromTop ? StringAlignment.Near : StringAlignment.Center;\r\n            sf.Alignment = textType.Equals(TextType.MainHeader) ? StringAlignment.Center : StringAlignment.Near;\r\n            graphic.DrawString(value, new Font(\"Calibri\", GetFontHeightWithTextType(textType)), Brushes.Black, rectangle, sf);\r\n            graphic.ResetTransform();\r\n        }\r\n\r\n        private static int GetFontHeightWithTextType(TextType textType)\r\n        {\r\n            switch (textType)\r\n            {\r\n                case TextType.MainHeader:\r\n                    {\r\n                        return 21;\r\n                    }\r\n                case TextType.Header:\r\n                    {\r\n                        return 20;\r\n                    }\r\n                case TextType.SubHeader:\r\n                    {\r\n                        return 15;\r\n                    }\r\n                case TextType.Description:\r\n                    {\r\n                        return 12;\r\n                    }\r\n                default:\r\n                    {\r\n                        return 12;\r\n                    }\r\n            }\r\n        }\r\n\r\n        public static string BuildLabel(LabelBuilderInputDTO input)\r\n        {\r\n           Pen linePen = new Pen(Color.Black, 3);\r\nSystem.Drawing.Image background = new Bitmap((int)(" + label.PageWidth + " * 96 / 25.4), (int)(" + label.PageHeight + "* 96 / 25.4));\r\n            Graphics graphic = Graphics.FromImage(background);\r\n            graphic.Clear(Color.White);\r\n            graphic.SmoothingMode = SmoothingMode.AntiAlias;\r\n            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;\r\n            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;\r\n\r\n           string base64String = null;\r\n            using (MemoryStream ms = new MemoryStream())\r\n            {\r\n                background.Save(ms, ImageFormat.Jpeg);\r\n                base64String = Convert.ToBase64String(ms.ToArray());\r\n            }\r\n            return base64String;\r\n        }\r\n\r\n\r\n        public enum TextType\r\n        {\r\n            Description,\r\n            MainHeader,\r\n            Header,\r\n            SubHeader\r\n        }\r\n    }\r\n\r\n}\r\n";
            int i = 1; // for RichTextBox
            int j = 1; // for PictureBox
            int k = 1; // for Line
            int l = 1; // for Barcodes
            foreach (LabelItemDTO item in label.LabelItems)
            {
                if (item.ItemType is LabelItemType.RichText)
                {
                    string insertAfter = "graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;";
                    int index = messageTextBox.Text.IndexOf(insertAfter) + insertAfter.Length;

                    mystrlist.Add("public string Text" + i + " { get; set; }");
                    itemCode = "\n //input.Text" + i + "=" + "\"" + item.Value + "\"" + ";\n" +
                        "Rectangle recText" + i + " = new Rectangle(" + item.X + "," + item.Y + "," + item.Width + "," + item.Height + ");\r\n      " +
                      "       AddText(graphic,  recText" + i + "," + "input.Text" + i + ", TextType.Description);\n";

                    messageTextBox.Text = messageTextBox.Text.Insert(index, itemCode);
                    i++;
                }
                else if (item.ItemType is LabelItemType.Image)
                {
                    string insertAfter = "graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;";
                    int index = messageTextBox.Text.IndexOf(insertAfter) + insertAfter.Length;

                    mystrlist.Add("public string ImageBase" + j + " { get; set; }");
                    itemCode = "\r\nSystem.Drawing.Image image" + j + " = null;\r\n            if (!string.IsNullOrEmpty(input.ImageBase" + j + "))\r\n            {\r\n             byte[] imageBytes = Convert.FromBase64String(input.ImageBase" + j + ");\r\n                using (MemoryStream ms = new MemoryStream(imageBytes))\r\n                {\r\n                image" + j + " = System.Drawing.Image.FromStream(ms);\r\n                }\r\n            }\r\n            AddImage(graphic, image" + j + "," + item.Width + "," + item.Height + "," + item.X + "," + item.Y + ");";
                    messageTextBox.Text = messageTextBox.Text.Insert(index, itemCode);
                    j++;
                }
                else if (item.ItemType is LabelItemType.StaticImage)
                {
                    string insertAfter = "graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;";
                    int index = messageTextBox.Text.IndexOf(insertAfter) + insertAfter.Length;

                    itemCode = "AddImage(graphic, Base64ToImage("+ item.Value +"), " + item.Width + ", " + item.Height + ", " + item.X + ", " + item.Y + ");";
                    messageTextBox.Text = messageTextBox.Text.Insert(index, itemCode);
                }
                else if(item.ItemType is LabelItemType.Line)
                {
                    string insertAfter = "graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;";
                    int index = messageTextBox.Text.IndexOf(insertAfter) + insertAfter.Length;

                    mystrlist.Add("public string Line" + k + " { get; set; }");

                    // insert code here
                    itemCode = "\r\nPointF startPoint" + k + " = new PointF(" + item.X + "," + item.Y + ");\r\n" +
                    "PointF endPoint" + k + " = new PointF(" + (item.X + item.Width) + "," + (item.Y + item.Height)+ ");\r\n"+
                    "AddLine(graphic, linePen, startPoint" + k + ", endPoint" + k + ");\r\n";

                    messageTextBox.Text = messageTextBox.Text.Insert(index, itemCode);
                    k++;

                }
                else if(item.ItemType is LabelItemType.Barcode)
                {
                    string insertAfter = "graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;";
                    int index = messageTextBox.Text.IndexOf(insertAfter) + insertAfter.Length;

                    mystrlist.Add("public string Barcode" + l + " { get; set; }");
                    //insert code here
                    if(item.BarcodeFormat is BarcodeFormat.QR_CODE)
                    {
                        itemCode = "GenerateQR(BarcodeFormat.QR_CODE, " + item.Width + " , " + item.Height + " , " + "\"" + item.BarcodeText + "\"" + ")\r\n ";
                    }
                    else if (item.BarcodeFormat is BarcodeFormat.CODE_128)
                    {
                        itemCode = "GenerateQR(BarcodeFormat.CODE_128, " + item.Width + " , " + item.Height + " , " + "\"" + item.BarcodeText + "\"" + ")\r\n ";
                    }
                        
                    messageTextBox.Text = messageTextBox.Text.Insert(index, itemCode);
                    l++;
                }

            }
            string strlistcomp = String.Join("\n ", mystrlist);
            messageTextBox.Text = messageTextBox.Text.Insert(157, strlistcomp);

            messageTextBox.SelectAll();
            messageForm.Controls.Add(messageTextBox);
            messageForm.ShowDialog();
        }

        private void SubHeader_Click(object sender, EventArgs e)
        {
            subHeaderFont = new System.Drawing.Font("MS Sans Serif", 15, FontStyle.Bold);
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                if (lastClickedTextBox != null)
                {
                    lastClickedTextBox.Font = subHeaderFont;
                }
            }
        }

        private void newBarcode_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first");
                return;
            }
            string message, title , defaultValue;
            string myValue;
            message = "Please enter the QR barcode text and length respectively (separated with comma) :";
            title = "New Barcode";
            defaultValue = "New QR Barcode,100";
            myValue = Interaction.InputBox(message, title, defaultValue);
            if ((string)myValue == "")
            {
                myValue = defaultValue;
                Interaction.MsgBox("myValue = " + myValue.ToString(), MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "msgbox demonstration");
            }
            var values = myValue.Split(',');
            var barcodeText = values[0];
            var barcodeLength = Int32.Parse(values[1]);

            Bitmap barcode = GenerateBarcode(BarcodeFormat.QR_CODE, barcodeLength, barcodeLength, barcodeText);
            // Create a new PictureBox control
            Barcodes barcodeQR = new Barcodes();
            barcodeQR.BarcodeFormat = BarcodeFormat.QR_CODE;

            barcodeQR.MouseDown += Control_MouseDown;

            // Set the image of the PictureBox control to the bitmap
            barcodeQR.Image = barcode;

            // Set the size mode of the PictureBox control to stretch the image to fit
            barcodeQR.SizeMode = PictureBoxSizeMode.StretchImage;

            // Set the parent of the PictureBox control to the Panel control
            barcodeQR.Parent = panel1;

            // Set the position of the PictureBox control relative to the panel
            barcodeQR.Location = new Point(30, 30);

            barcodeQR.Size = new Size(barcodeLength,barcodeLength);

            // Save the barcode bitmap to a temporary file
            string tempFileName = System.IO.Path.GetTempFileName();
            barcode.Save(tempFileName, ImageFormat.Png);
            this.Controls.Add(barcodeQR);
            // Set the image location of the PictureBox control to the file path of the saved image
            barcodeQR.ImageLocation = tempFileName;
            var image = System.Drawing.Image.FromFile(barcodeQR.ImageLocation);
            var base64 = image.ToBase64();

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                base64 = Convert.ToBase64String(ms.ToArray());
            }

            var newQRItem = new LabelItemDTO
            {
                Font = null,
                ItemType = LabelItemType.Barcode,
                Value = base64,
                BarcodeText = barcodeText,
                Y = 70,
                X = 415,
                Width = barcodeQR.Width,
                Height = barcode.Height,
                ID = barcodeQR.ID,
                BarcodeFormat = BarcodeFormat.QR_CODE,
                Angle = 0,
                ItemReference = barcodeQR
            };

            barcodeQR.BringToFront();
            barcodeQR.Top = newQRItem.Y;
            barcodeQR.Left = newQRItem.X;
            barcodeQR.Size = new Size(newQRItem.Width, newQRItem.Height);

            this.label.LabelItems.Add(newQRItem);

        }

        private void newLine_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first");
                return;
            }

            Tile3 line = new Tile3();
            line.MouseDown += Control_MouseDown;
            var newLineItem = new LabelItemDTO
            {
                ItemType = LabelItemType.Line,
                Value = null,
                Y = 70,
                X = 415,
                Width = 200,
                Height = 2,
                ID = line.ID,
                ItemReference = line
            };

            line.Visible = true;

            this.Controls.Add(line);

            line.BringToFront();
            line.Top = newLineItem.Y;
            line.Left = newLineItem.X;
            line.Size = new Size(newLineItem.Width, newLineItem.Height);
            line.Location = new Point(50, 50);
            line.Visible = true;

            this.label.LabelItems.Add(newLineItem);

        }

        private Dictionary<string, string> rotatedImages = new Dictionary<string, string>();

        private void Rotate_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first");
                return;
            }
            if (previousClicked is Tile tile) 
            {
                var tileLocation = tile.ImageLocation;
                // Get the current image of the last clicked PictureBox
                Image currentImage = tile.Image;

                // Rotate the image by 90 degrees clockwise
                currentImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Set the rotated image as the new image of the last clicked PictureBox
                tile.Image = currentImage;

                //tile.ImageLocation = tileLocation;

                // Refresh the PictureBox to show the rotated image
                tile.Refresh();

                var index = label.LabelItems.FindIndex(x => x.ID == tile.ID);
                var desiredItem = label.LabelItems[index];
                desiredItem.Angle += 90 % 360;
                
                UpdateLabelItemDTO(tile, currentImage);

            }
            else if (previousClicked is Tile3 line)
            {
                foreach (var control in this.Controls)
                {
                    if (!(control is Tile3))
                    {
                        continue;
                    }

                    if (!line.Selected)
                    {
                        continue;
                    }

                    line.RotateLine();
                    line.Refresh();
                    foreach (LabelItemDTO item in label.LabelItems)
                    {
                        if (item.ID == line.ID)
                        {
                            item.Width= line.Width;
                            item.Height= line.Height;
                        }
                    }
                }
            }
            else if (previousClicked is Barcodes barcode)
            {
                // Get the current image of the last clicked PictureBox
                Image currentImage = barcode.Image;

                // Rotate the image by 90 degrees clockwise
                currentImage.RotateFlip(RotateFlipType.Rotate90FlipNone);

                // Set the rotated image as the new image of the last clicked PictureBox
                barcode.Image = currentImage;

                // Refresh the PictureBox to show the rotated image
                barcode.Refresh();

                UpdateLabelItemRotatedBarcode(barcode,barcode.Image);

            }
            else
            {
                if (previousClicked != null)
                {
                    
                }
            }
        }

        private GraphicsPath path;

        private void exit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Exit Application", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                Close();
            }
            else
            {
                return;
            }
        }

        private void newFile_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to start a new file?", "New File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                newFileIsClicked = true;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\my_file.txt";
                File.WriteAllText(path, String.Empty);
                txtEditor.Text = "";
                if (label != null)
                {
                    label.LabelItems.Clear();
                }
                foreach (Control c in Controls)
                {
                    Action<Control.ControlCollection> func = null;

                    func = (controls) =>
                    {
                        foreach (Control control in controls)
                            if (control is Tile || control is Tile2 || control is Panel || control is Tile3 || control is Barcodes)
                                this.Controls.Remove(control);
                            else
                                func(control.Controls);
                    };

                    func(Controls);
                }
            }
        }

        private void openTextFile_Click(object sender, EventArgs e)
        {
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                StreamReader inputFile = new StreamReader(dlgOpen.FileName);
                txtEditor.Text = inputFile.ReadToEnd();
                txtEditor.BringToFront();
                inputFile.Close();
                txtEditor.SelectionLength = 0;
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            SaveData(label);
        }

        private void boldFont_Click(object sender, EventArgs e)
        {
            menuBoldFormat.Checked = !menuBoldFormat.Checked;

            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                ChangeFont(obj);
            }
        }

        private void Font_Click(object sender, EventArgs e)
        {
            fontDialog.ShowDialog();
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                obj.SelectAll();
                obj.Font = fontDialog.Font;
            }
        }

        private void italicFont_Click(object sender, EventArgs e)
        {
            menuItalicFormat.Checked = !menuItalicFormat.Checked;
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                ChangeFont(obj);
            }
        }

        private void underlineFont_Click(object sender, EventArgs e)
        {
            menuUnderlineFormat.Checked = !menuUnderlineFormat.Checked;
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                ChangeFont(obj);

            }
        }

        private void leftAlign_Click(object sender, EventArgs e)
        {
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                obj.SelectionLength = obj.Text.Length;
                obj.SelectionAlignment = HorizontalAlignment.Left;
                obj.HAlignType = Tile2.HAlignment.Left;
            }
        }

        private void centerAlign_Click(object sender, EventArgs e)
        {
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                obj.SelectionLength = obj.Text.Length;
                obj.SelectionAlignment = HorizontalAlignment.Center;
                obj.HAlignType = Tile2.HAlignment.Center;
            }
        }

        private void rightAlign_Click(object sender, EventArgs e)
        {
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                obj.SelectionLength = obj.Text.Length;
                obj.SelectionAlignment = HorizontalAlignment.Right;
                obj.HAlignType = Tile2.HAlignment.Right;
            }
        }

        private void alignToTop_Click(object sender, EventArgs e)
        {
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                obj.SelectionStart = 0;
                // Get the line number of the current selection
                int lineNumber = obj.GetLineFromCharIndex(obj.SelectionStart);

                // Get the index of the first character of the current line
                int lineStartIndex = obj.GetFirstCharIndexFromLine(lineNumber);

                // Get the index of the first character of the next line
                int nextLineStartIndex = obj.GetFirstCharIndexFromLine(lineNumber + 1);

                // Calculate the length of the current line in characters
                int lineLength = nextLineStartIndex - lineStartIndex;


                if (lineLength >= 0)
                {
                    obj.SelectionLength = lineLength;
                }

                obj.SelectionCharOffset = 0;

                obj.VAlignType = Tile2.VAlignment.Top;

            }
        }

        private void alignToCenter_Click(object sender, EventArgs e)
        {
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                obj.SelectionStart = 0;
                // Get the line number of the current selection
                int lineNumber = obj.GetLineFromCharIndex(obj.SelectionStart);

                // Get the index of the first character of the current line
                int lineStartIndex = obj.GetFirstCharIndexFromLine(lineNumber);

                // Get the index of the first character of the next line
                int nextLineStartIndex = obj.GetFirstCharIndexFromLine(lineNumber + 1);

                // Calculate the length of the current line in characters
                int lineLength = nextLineStartIndex - lineStartIndex;


                if (lineLength >= 0)
                {
                    obj.SelectionLength = lineLength;
                }

                // Set the vertical alignment of the selected text to Middle
                obj.SelectionCharOffset = -(obj.ClientSize.Height - obj.GetPositionFromCharIndex(obj.TextLength).Y - obj.Font.Height) / 2;

                obj.VAlignType = Tile2.VAlignment.Center;
            }
        }

        private void alignToBottom_Click(object sender, EventArgs e)
        {
            foreach (var control in this.Controls)
            {
                if (!(control is Tile2))
                {
                    continue;
                }

                var obj = control as Tile2;

                if (!obj.Selected)
                {
                    continue;
                }

                obj.SelectionStart = 0;
                obj.SelectAll();
                // Get the line number of the current selection
                int lineNumber = obj.GetLineFromCharIndex(obj.SelectionStart);

                // Get the index of the first character of the current line
                int lineStartIndex = obj.GetFirstCharIndexFromLine(lineNumber);

                // Get the index of the first character of the next line
                int nextLineStartIndex = obj.GetFirstCharIndexFromLine(lineNumber + 1);

                // Calculate the length of the current line in characters
                int lineLength = nextLineStartIndex - lineStartIndex;


                if (lineLength >= 0)
                {
                    obj.SelectionLength = lineLength;
                }

                --lineNumber;

                obj.SelectionCharOffset = -(obj.ClientSize.Height - lineNumber);

                obj.VAlignType = Tile2.VAlignment.Bottom;
            }
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            List<Control> controlsToRemove = new List<Control>();

            foreach (Control control in this.Controls)
            {
                if (control is Tile2 obj && obj.Selected)
                {
                    label.LabelItems.RemoveAll(x => x.ID == obj.ID);
                    controlsToRemove.Add(obj);
                }
                else if (control is Tile objTile && objTile.Selected)
                {
                    label.LabelItems.RemoveAll(x => x.ID == objTile.ID);
                    controlsToRemove.Add(objTile);
                }
                else if (control is Tile3 objLine && objLine.Selected)
                {
                    label.LabelItems.RemoveAll(x => x.ID == objLine.ID);
                    controlsToRemove.Add(objLine);
                }
                else if (control is Barcodes objBarcode && objBarcode.Selected)
                {
                    label.LabelItems.RemoveAll(x => x.ID == objBarcode.ID);
                    controlsToRemove.Add(objBarcode);
                }
            }

            foreach (Control controlToRemove in controlsToRemove)
            {
                this.Controls.Remove(controlToRemove);
                controlToRemove.Dispose();
            }

        }

        private void newLabel_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first.");
                return;
            }

            OpenFileDialog ofd2 = new OpenFileDialog();
            ofd2.Filter = "Image Files (*.bmp;*.jpg;*.jpeg,*.png)|*.BMP;*.JPG;*.JPEG;*.PNG";
            if (ofd2.ShowDialog() == DialogResult.OK)
            {
                Tile staticPic = new Tile();
                this.Controls.Add(staticPic);
                staticPic.MouseDown += Control_MouseDown;
                staticPic.BorderStyle = BorderStyle.FixedSingle;
                staticPic.SizeMode = PictureBoxSizeMode.Zoom;
                staticPic.BackColor = Color.Transparent;
                staticPic.ImageLocation = ofd2.FileName;
                var image = System.Drawing.Image.FromFile(staticPic.ImageLocation);
                var base64 = image.ToBase64();

                using (var ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Jpeg);
                    base64 = Convert.ToBase64String(ms.ToArray());
                }

                var newImageItem = new LabelItemDTO
                {
                    Font = null,
                    ItemType = LabelItemType.StaticImage,
                    Value = base64,
                    Y = 70,
                    X = 415,
                    Width = staticPic.Width,
                    Height = staticPic.Height,
                    ID = staticPic.ID,
                    Angle = 0,
                    ItemReference = staticPic
                };

                staticPic.BringToFront();
                staticPic.Top = newImageItem.Y;
                staticPic.Left = newImageItem.X;
                staticPic.Size = new Size(newImageItem.Width, newImageItem.Height);

                this.label.LabelItems.Add(newImageItem);
            }
        }

        private void newCode128Barcode_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first");
                return;
            }
            string message, title , defaultValue;
            string myValue;
            message = "Please enter the Code-128 barcode text, width , and height respectively (separated with comma) :";
            title = "New Barcode";
            defaultValue = "New Code 128 Barcode,350,70";
            myValue = Interaction.InputBox(message, title, defaultValue);
            if ((string)myValue == "")
            {
                myValue = defaultValue;
                Interaction.MsgBox("myValue = " + myValue.ToString(), MsgBoxStyle.OkOnly | MsgBoxStyle.Information, "msgbox demonstration");

            }
            var values = myValue.Split(',');
            var barcodeText = values[0];
            var barcodeWidth = Int32.Parse(values[1]);
            var barcodeHeight = Int32.Parse(values[2]);

            Bitmap barcode = GenerateBarcode(BarcodeFormat.CODE_128, barcodeWidth, barcodeHeight, barcodeText);
            // Create a new PictureBox control
            Barcodes barcode128 = new Barcodes();
            barcode128.BarcodeFormat = BarcodeFormat.CODE_128;
            barcode128.MouseDown += Control_MouseDown;

            // Set the image of the PictureBox control to the bitmap
            barcode128.Image = barcode;

            // Set the size mode of the PictureBox control to stretch the image to fit
            barcode128.SizeMode = PictureBoxSizeMode.StretchImage;

            // Set the parent of the PictureBox control to the Panel control
            barcode128.Parent = panel1;

            // Set the position of the PictureBox control relative to the panel
            barcode128.Location = new Point(30, 30);

            barcode128.Size = new Size(barcodeWidth, barcodeHeight);
            // Save the barcode bitmap to a temporary file
            string tempFileName = System.IO.Path.GetTempFileName();
            barcode.Save(tempFileName, ImageFormat.Png);
            this.Controls.Add(barcode128);
            // Set the image location of the PictureBox control to the file path of the saved image
            barcode128.ImageLocation = tempFileName;
            var image = System.Drawing.Image.FromFile(barcode128.ImageLocation);
            var base64 = image.ToBase64();

            using (var ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Jpeg);
                base64 = Convert.ToBase64String(ms.ToArray());
            }

            var new128Item = new LabelItemDTO
            {
                Font = null,
                ItemType = LabelItemType.Barcode,
                Value = base64,
                BarcodeText = barcodeText,
                Y = 70,
                X = 415,
                Width = barcode128.Width,
                Height = barcode.Height,
                ID = barcode128.ID,
                BarcodeFormat = BarcodeFormat.CODE_128,
                Angle = 0,
                ItemReference = barcode128
            };

            barcode128.BringToFront();
            barcode128.Top = new128Item.Y;
            barcode128.Left = new128Item.X;
            barcode128.Size = new Size(new128Item.Width, new128Item.Height);

            this.label.LabelItems.Add(new128Item);
        }
    }
}

