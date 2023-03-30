using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;

namespace LabelCreator
{
    public partial class formEditor : Form
    {
        LabelDTO label = null;
        System.Drawing.Font headerFont;
        System.Drawing.Font subHeaderFont;
        System.Drawing.Font standardFont;
        private Control lastClicked;
        private string itemCode;
        private Tile2 lastClickedTextBox;
        private Control previousClicked;
        private string result;
        private Control tempClicked;
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
            if (!(lastClicked is System.Windows.Forms.Button))
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
            var initialText = "Write Here";

            if (this.label == null)
            {
                MessageBox.Show("Please create a new project first");
                return;
            }

            Tile2 txtbox = new Tile2();
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
                ItemReference = txtbox
            };
           
            txtbox.Visible = true;
            panel1.Controls.Add(txtbox);

            this.Controls.Add(txtbox);

            txtbox.MouseDown += TextBox_MouseDown;

            txtbox.BorderStyle = BorderStyle.None;
            txtbox.BringToFront();
            txtbox.Top = newItem.Y;
            txtbox.Left = newItem.X;
            txtbox.Font = newItem.Font;
            txtbox.Size = new Size(newItem.Width, newItem.Height);
            txtbox.Text = initialText;
            ChangeFont(txtbox);

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
                            if (control is Tile|| control is Tile2|| control is Panel)
                                this.Controls.Remove(control);
                           // else if (control is Tile2)
                             //   this.Controls.Remove(control);
                            //else if (control is Panel)
                                //this.Controls.Remove(control);
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
                panel1.Controls.Add(picBox);
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

                var newItem = new LabelItemDTO
                {
                    Font = null,
                    ItemType = LabelItemType.Image,
                    Value = base64,
                    Y = 70,
                    X = 415,
                    Width = picBox.Width,
                    Height = picBox.Height,
                    ID = picBox.ID,
                    ItemReference = picBox
                };

                picBox.BringToFront();
                picBox.Top = newItem.Y;
                picBox.Left = newItem.X;
                picBox.Size = new Size(newItem.Width, newItem.Height);

                this.label.LabelItems.Add(newItem);
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
            panel1.BackColor = label.BackgroundColor;
            panel1.Location = new Point(5, 30);
            panel1.Size = new Size(label.PageWidth, label.PageHeight);
          
        }

        private void menuFile_Click(object sender, EventArgs e)
        {

        }

        private void menuMain_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            menuMain.BringToFront();
        }

        private void menuFormat_Click(object sender, EventArgs e)
        {

        }

        // Save button
        private void savetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData(label);
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
                    lastClickedTextBox.SelectionFont = headerFont;
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
                    lastClickedTextBox.SelectionFont = standardFont;
                }
            }
        }


        private void Generate_Click(object sender, EventArgs e)
        {
            List<string> mystrlist = new List<string> { };
            Form messageForm = new Form();
            messageForm.Size = new System.Drawing.Size(400, 200);
            messageForm.Text = "Generated Code";

            RichTextBox messageTextBox = new RichTextBox();
            messageTextBox.Size = new System.Drawing.Size(375, 150);
            messageTextBox.Multiline = true;
            messageTextBox.ReadOnly = true;

            messageTextBox.Text = "using System.Drawing;\r\nusing System.Drawing.Drawing2D;\r\nusing System.Drawing.Imaging;\r\n\r\nnamespace denemeapp\r\n{\r\n    public class LabelBuilderInputDTO\r\n    {\r\n       }\r\n\r\n    public static class LabelBuilder\r\n    {\r\n        private static void AddImage(Graphics graphics, System.Drawing.Image image, float width, float height, float x, float y)\r\n        {\r\n            graphics.DrawImage(image, new RectangleF\r\n            {\r\n                Width = width,\r\n                Height = height,\r\n                X = x,\r\n                Y = y\r\n            });\r\n        }\r\n\r\n        public static System.Drawing.Image Base64ToImage(string base64String)\r\n        {\r\n            byte[] imageBytes = Convert.FromBase64String(base64String);\r\n\r\n            using (var ms = new MemoryStream(imageBytes))\r\n            {\r\n                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);\r\n                return image;\r\n            }\r\n        }\r\n\r\n        private static void AddText(Graphics graphic, Rectangle rectangle, string value, TextType textType, bool fromTop = false)\r\n        {\r\n            graphic.FillRectangle(Brushes.White, rectangle);\r\n            StringFormat sf = new StringFormat();\r\n            sf.LineAlignment = fromTop ? StringAlignment.Near : StringAlignment.Center;\r\n            sf.Alignment = textType.Equals(TextType.MainHeader) ? StringAlignment.Center : StringAlignment.Near;\r\n            graphic.DrawString(value, new Font(\"Calibri\", GetFontHeightWithTextType(textType)), Brushes.Black, rectangle, sf);\r\n            graphic.ResetTransform();\r\n        }\r\n\r\n        private static int GetFontHeightWithTextType(TextType textType)\r\n        {\r\n            switch (textType)\r\n            {\r\n                case TextType.MainHeader:\r\n                    {\r\n                        return 21;\r\n                    }\r\n                case TextType.Header:\r\n                    {\r\n                        return 20;\r\n                    }\r\n                case TextType.SubHeader:\r\n                    {\r\n                        return 15;\r\n                    }\r\n                case TextType.Description:\r\n                    {\r\n                        return 12;\r\n                    }\r\n                default:\r\n                    {\r\n                        return 12;\r\n                    }\r\n            }\r\n        }\r\n\r\n        public static string BuildLabel(LabelBuilderInputDTO input)\r\n        {\r\n           System.Drawing.Image background = new Bitmap((int)(" + label.PageWidth + " * 96 / 25.4), (int)(" + label.PageHeight+ "* 96 / 25.4));\r\n            Graphics graphic = Graphics.FromImage(background);\r\n            graphic.Clear(Color.White);\r\n            graphic.SmoothingMode = SmoothingMode.AntiAlias;\r\n            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;\r\n            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;\r\n\r\n           string base64String = null;\r\n            using (MemoryStream ms = new MemoryStream())\r\n            {\r\n                background.Save(ms, ImageFormat.Jpeg);\r\n                base64String = Convert.ToBase64String(ms.ToArray());\r\n            }\r\n            return base64String;\r\n        }\r\n\r\n\r\n        public enum TextType\r\n        {\r\n            Description,\r\n            MainHeader,\r\n            Header,\r\n            SubHeader\r\n        }\r\n    }\r\n\r\n}\r\n";
            int i = 1;
            int j = 1;
            foreach (LabelItemDTO item in label.LabelItems)
            {
                if (item.ItemType is LabelItemType.RichText)
                {
                    string insertAfter = "graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;";
                    int index = messageTextBox.Text.IndexOf(insertAfter) + insertAfter.Length;

                    mystrlist.Add("public string Text"+ i+ " { get; set; }");
                    itemCode = "\n //input.Text" + i + "=" + "\"" + item.Value + "\"" + ";\n" +
                        "Rectangle recText" + i + " = new Rectangle(" + item.X + "," + item.Y + "," + item.Width + "," + item.Height + ");\r\n      " +
                      "       AddText(graphic,  recText" + i + "," + "input.Text" + i + ", TextType.Description);\n";

                    messageTextBox.Text = messageTextBox.Text.Insert(index,itemCode);
                    i++;
                }
                else if (item.ItemType is LabelItemType.Image)
                {
                    string insertAfter = "graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;";
                    int index = messageTextBox.Text.IndexOf(insertAfter) + insertAfter.Length;

                    mystrlist.Add("public string ImageBase" + j + " { get; set; }");
                    itemCode = "System.Drawing.Image image" + j + " = null;\r\n            if (!string.IsNullOrEmpty(input.ImageBase" + j + "))\r\n            {\r\n             byte[] imageBytes = Convert.FromBase64String(input.ImageBase" + j + ");\r\n                using (MemoryStream ms = new MemoryStream(imageBytes))\r\n                {\r\n                image" + j + " = System.Drawing.Image.FromStream(ms);\r\n                }\r\n            }\r\n            AddImage(graphic, image" + j + "," + item.Width + "," + item.Height + "," + item.X + "," + item.Y + ");";
                    messageTextBox.Text = messageTextBox.Text.Insert(index, itemCode);
                    j++;
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
                    lastClickedTextBox.SelectionFont = subHeaderFont;
                }
            }
        }

    }
}

