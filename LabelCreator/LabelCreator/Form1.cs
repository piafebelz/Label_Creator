using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Image = System.Drawing.Image;
using System.Windows.Documents;
using System.Windows.Shapes;
using Path = System.IO.Path;
using Org.BouncyCastle.Utilities.Encoders;
using System.Reflection;

namespace LabelCreator
{

    public partial class formEditor : Form
    {
        LabelDTO label = null;
        System.Drawing.Font headerFont;
        System.Drawing.Font subHeaderFont;
        System.Drawing.Font standardFont;
        private bool isHeader,isStandard,isSubHeader;
        private Control lastClicked;
        private string itemCode;
        private Tile2 lastClickedTextBox;
        private Control previousClicked;
        private PrintDocument printDocument1 = new PrintDocument();
        private PrintPreviewDialog previewdlg = new PrintPreviewDialog();
        private string result;
        private LabelDTO openData;

        public LabelDTO getLabel()
        {
            return label;
        }
        private List<Tile2> richTextBoxList = new List<Tile2>();
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
        private Panel panel1;

        //Saves the current panel (stored in label)
        public void SaveData(LabelDTO label)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\my_file.txt";
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            LabelDTO savedData = new LabelDTO();
            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    savedData = (LabelDTO)serializer.Deserialize(file, typeof(LabelDTO));
                }
            }

            foreach (LabelItemDTO item in label.LabelItems)
            {
                if (item.ItemType is LabelItemType.Image)
                {
                    var reference = item.ItemReference as Tile;
                    item.X = reference.Location.X;
                    item.Y = reference.Location.Y;
                    item.Width = reference.Width;
                    item.Height = reference.Height;
                    item.Value = reference.Image.ToBase64();
                }
                else if (item.ItemType is LabelItemType.RichText)
                {
                    var reference = item.ItemReference as Tile2;
                    item.X = reference.Location.X;
                    item.Y = reference.Location.Y;
                    item.Width = reference.Width;
                    item.Height = reference.Height;
                    item.Font = reference.Font;
                    item.Value = reference.Text;
                }
            }
            if (savedData != null)
            {
                foreach (LabelItemDTO item in savedData.LabelItems)
                {
                    if (!label.LabelItems.Any(x => x.ID == item.ID))
                    {
                        label.LabelItems.Add(item);
                    }
                }
            }
            

            string result = Newtonsoft.Json.JsonConvert.SerializeObject(label);
            Console.WriteLine(result);
            File.WriteAllText(path, result);
        }

        //Opens the saved data when application starts
        public void openSavedData()
        {
            
                string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\my_file.txt";
                if (File.Exists(path))
                {
                    string result = File.ReadAllText(path);
                    openData = JsonConvert.DeserializeObject<LabelDTO>(result);

                    if (openData != null)
                    {
                        var panel1 = new Panel();
                        panel1.BackColor = openData.BackgroundColor;
                        panel1.Location = new Point(5, 30);
                        panel1.Size = new Size(openData.PageWidth, openData.PageHeight);
                        this.Controls.Add(panel1);
                        panel1.BringToFront();


                        foreach (LabelItemDTO lbtype in openData.LabelItems)
                        {
                            if (lbtype.ItemType == LabelItemType.Image)
                            {
                                Tile picbox = new Tile();
                                picbox.Size = new Size((int)lbtype.Width, (int)lbtype.Height);
                                picbox.Location = new Point((int)lbtype.X, (int)lbtype.Y);

                                // Gets the Base64 encoded image data
                                string imageData = (string)lbtype.Value;

                                // Decodes the Base64 encoded image data
                                byte[] imageBytes = Convert.FromBase64String(imageData);

                                // Converts the byte array into an Image object
                                MemoryStream memoryStream = new MemoryStream(imageBytes);
                                var image = System.Drawing.Image.FromStream(memoryStream);
                                picbox.Image = image;
                                picbox.SizeMode = PictureBoxSizeMode.Zoom;

                                this.Controls.Add(picbox);
                                picbox.BringToFront();
                                lbtype.ItemReference = picbox;
                            }
                            else if (lbtype.ItemType == LabelItemType.RichText)
                            {
                                Tile2 txtbox = new Tile2();

                                txtbox.Size = new Size((int)lbtype.Width, (int)lbtype.Height);
                                txtbox.Location = new Point((int)lbtype.X, (int)lbtype.Y);
                                txtbox.Text = lbtype.Value;
                                txtbox.Font = lbtype.Font;
                                this.Controls.Add(txtbox);
                                txtbox.BringToFront();
                                lbtype.ItemReference = txtbox;
                            }
                        }

                        this.label = openData;
                    }
                }
            
        }

        int fontSize = 8;
        string fontName = "MS Sans Serif";

        // Changes the font of the selected text
        public void ChangeFont(System.Windows.Forms.RichTextBox t)
        {
            FontStyle newFont;
            newFont = FontStyle.Regular;
            if (menuBoldFormat.Checked)
            {
                newFont = newFont | FontStyle.Bold;
            }
            if (menuItalicFormat.Checked)
            {
                newFont = newFont | FontStyle.Italic;
            }
            if (menuUnderlineFormat.Checked)
            {
                newFont = newFont | FontStyle.Underline;
            }

            t.SelectionFont = new System.Drawing.Font(fontName, fontSize, newFont);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            openSavedData();


            foreach (Tile2 rtb in richTextBoxList)
            {
                rtb.SelectionChanged += richTextBox_SelectionChanged;
            }
            try
            {
                StreamReader inputFile = new StreamReader(Application.StartupPath + "\\note.ini");
                menuBoldFormat.Checked = Convert.ToBoolean(inputFile.ReadLine());
                menuItalicFormat.Checked = Convert.ToBoolean(inputFile.ReadLine());
                menuUnderlineFormat.Checked = Convert.ToBoolean(inputFile.ReadLine());
                int i = Convert.ToInt32(inputFile.ReadLine());
                switch (i)
                {
                    case 1:
                        sizeSmall.PerformClick();
                        break;
                    case 2:
                        sizeMedium.PerformClick();
                        break;
                    case 3:
                        sizeLarge.PerformClick();
                        break;
                }
                inputFile.Close();
            }
            catch (FileNotFoundException)
            {
                MessageBox.Show("Config file not found.", "Default reset", MessageBoxButtons.OK, MessageBoxIcon.Error);
                menuBoldFormat.Checked = false;
                menuItalicFormat.Checked = false;
                menuUnderlineFormat.Checked = false;
                sizeSmall.PerformClick();
                throw;
            }

            ChangeFont(txtEditor);
        }
        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            lastClickedTextBox = sender as Tile2;
        }

        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Tile2 rtb = sender as Tile2;
            int start = rtb.SelectionStart;
            int length = rtb.SelectionLength;

            if (length > 0)
            {

                rtb.Selected = true;
                System.Drawing.Font currentFont = rtb.SelectionFont;
                ChangeFont(rtb);
            }
        }

        public void btnDynText_Click(object sender, EventArgs e)
        {
            var initialText = "Write Here";

            if (this.label == null)
            {
                MessageBox.Show("Lütfen ilk önce proje oluşturun");
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
            txtbox.BringToFront();
            ChangeFont(txtbox);

            this.label.LabelItems.Add(newItem);
        }

        private void MyNewTxtbox_Click(object sender, EventArgs e)
        {

        }
        private void propertyGrid1_Click(object sender, EventArgs e)
        {

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
        public void ClearFile(Panel p)
        {
            p.Controls.Clear();
        }

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
                            if (control is Tile)
                                this.Controls.Remove(control);
                            else if (control is Tile2)
                                this.Controls.Remove(control);
                            else if (control is Panel)
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
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.label == null)
            {
                MessageBox.Show("Lütfen ilk önce proje oluşturun");
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

        private void tile1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtEditor_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
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

        private Control tempClicked;

        public Guid ID { get; private set; }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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

        // Draggable and Resizeable pictureboxes
        [Serializable]
        public class Tile : PictureBox
        {
            public Guid ID { get; }

            [DllImport("user32.dll")]
            static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("User32.dll")]
            static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            #region events

            public event EventHandler TileClicked;
            public event EventHandler TileRightClicked;
            public event EventHandler TileMoved;



            private void TileClickedFunction()
            {
                if (TileClicked != null)
                    TileClicked(this, new EventArgs());
            }

            private void TileRightClickedFunction()
            {
                if (TileRightClicked != null)
                    TileRightClicked(this, new EventArgs());
            }

            private void TileMovedFunction()
            {
                if (TileMoved != null)
                    TileMoved(this, new EventArgs());
            }

            #endregion

            #region windows messages

            protected override void WndProc(ref Message m)
            {
                const int WM_NCPAINT = 133;
                if (m.Msg == WM_NCPAINT)
                {
                    IntPtr hdc = GetWindowDC(m.HWnd);
                    Graphics g = Graphics.FromHdc(hdc);
                    System.Drawing.Rectangle rDraw = new System.Drawing.Rectangle(0, 0, this.Width - 1, this.Height - 1);
                    Pen pBottom = new Pen(Color.Gray, 3);
                    Pen pTop = new Pen(Color.White, 3);
                    g.DrawRectangle(pBottom, rDraw);
                    Point[] pts = new Point[3];

                    pts[0] = new Point(0, this.Height - 1);
                    pts[1] = new Point(0, 0);
                    pts[2] = new Point(this.Width - 1, 0);


                    g.DrawLines(pTop, pts);
                    ReleaseDC(this.Handle, hdc);
                }
                else
                {

                    base.WndProc(ref m);
                }
            }

            #endregion

            #region init & load

            public Tile()
            {
                if (this.Tag == null) this.Tag = string.Empty;

                #region init variables
                ID = Guid.NewGuid();
                //moving part
                bool Dragging = false;
                bool Dragged = false;
                Point DragStart = Point.Empty;

                bool Resizing = false;
                bool Resized = false;
                bool MouseIsInLeftEdge = false;
                bool MouseIsInRightEdge = false;
                bool MouseIsInTopEdge = false;
                bool MouseIsInBottomEdge = false;
                Point CursorStartPoint = new Point();
                Size CurrentStartSize = new Size();


                //make the shadow
                this.BorderStyle = BorderStyle.Fixed3D;
                Label label1 = new Label();
                label1.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                label1.Location = new Point(5, 5);
                label1.AutoSize = true;
                this.Controls.Add(label1);

                #endregion

                #region visible changed

                this.VisibleChanged += delegate (object sender, EventArgs e)
                {
                    TileMovedFunction();
                };

                #endregion

                #region paint

                this.Paint += delegate (object sender, PaintEventArgs e)
                {
                    Graphics g = this.Parent.CreateGraphics();
                    Matrix mx = new Matrix(1F, 0, 0, 1F, 4, 4);
                    System.Drawing.Rectangle rdraw = new System.Drawing.Rectangle(this.Left, this.Top, this.Width, this.Height);
                    g.Transform = mx;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), rdraw);
                    g.Dispose();

                    label1.Text = this.Tag.ToString();
                };

                #endregion


                #region mouse down 

                this.MouseDown += delegate (object sender, MouseEventArgs e)
                {
                    MouseIsInLeftEdge = Math.Abs(e.X) <= 5;
                    MouseIsInRightEdge = Math.Abs(e.X - this.Width) <= 5;
                    MouseIsInTopEdge = Math.Abs(e.Y) <= 5;
                    MouseIsInBottomEdge = Math.Abs(e.Y - this.Height) <= 5;

                    if (MouseIsInLeftEdge || MouseIsInRightEdge || MouseIsInTopEdge || MouseIsInBottomEdge)
                    {
                        Resizing = true;
                        Resized = false;

                        CursorStartPoint = new Point(e.X, e.Y);
                        CurrentStartSize = this.Size;

                        if (MouseIsInLeftEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Cursor = Cursors.SizeNWSE;
                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Cursor = Cursors.SizeNESW;
                            }
                            else
                            {
                                this.Cursor = Cursors.SizeWE;
                            }
                        }
                        else if (MouseIsInRightEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Cursor = Cursors.SizeNESW;
                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Cursor = Cursors.SizeNWSE;
                            }
                            else
                            {
                                this.Cursor = Cursors.SizeWE;
                            }

                        }
                        else if (MouseIsInTopEdge || MouseIsInBottomEdge)
                        {
                            this.Cursor = Cursors.SizeNS;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                        }

                    }
                    else
                    {
                        Dragging = true;
                        Dragged = false;
                        DragStart = new Point(e.X, e.Y);
                    }
                    this.Capture = true;
                };

                #endregion

                #region mouse up

                this.MouseUp += delegate (object sender, MouseEventArgs e)
                {
                    Dragging = false;
                    Resizing = false;

                    this.Capture = false;

                    if (Dragged || Resized)
                    {
                        TileMovedFunction();
                    }
                    else
                    {
                        if (e.Button == MouseButtons.Right)
                            TileRightClickedFunction();
                        else
                            TileClickedFunction();
                    }
                };

                #endregion

                #region mouse move

                this.MouseMove += delegate (object sender, MouseEventArgs e)
                {
                    if (Resizing)
                    {
                        Resized = false;

                        if (MouseIsInLeftEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Width -= (e.X - CursorStartPoint.X);
                                this.Left += (e.X - CursorStartPoint.X);
                                this.Height -= (e.Y - CursorStartPoint.Y);
                                this.Top += (e.Y - CursorStartPoint.Y);
                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Width -= (e.X - CursorStartPoint.X);
                                this.Left += (e.X - CursorStartPoint.X);
                                this.Height = (e.Y - CursorStartPoint.Y)
                                         + CurrentStartSize.Height;
                            }
                            else
                            {
                                this.Width -= (e.X - CursorStartPoint.X);
                                this.Left += (e.X - CursorStartPoint.X);
                            }
                        }
                        else if (MouseIsInRightEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Width = (e.X - CursorStartPoint.X)
                                                + CurrentStartSize.Width;
                                this.Height -= (e.Y - CursorStartPoint.Y);
                                this.Top += (e.Y - CursorStartPoint.Y);

                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Width = (e.X - CursorStartPoint.X)
                                                + CurrentStartSize.Width;
                                this.Height = (e.Y - CursorStartPoint.Y)
                                                + CurrentStartSize.Height;
                            }
                            else
                            {
                                this.Width = (e.X - CursorStartPoint.X)
                                               + CurrentStartSize.Width;
                            }
                        }
                        else if (MouseIsInTopEdge)
                        {
                            this.Height -= (e.Y - CursorStartPoint.Y);
                            this.Top += (e.Y - CursorStartPoint.Y);
                        }
                        else if (MouseIsInBottomEdge)
                        {
                            this.Height = (e.Y - CursorStartPoint.Y)
                                       + CurrentStartSize.Height;
                        }
                        else
                        {
                            Resizing = false;
                        }
                    }
                    else if (Dragging)
                    {
                        Dragged = true;
                        this.Left = Math.Max(0, e.X + this.Left - DragStart.X);
                        this.Top = Math.Max(0, e.Y + this.Top - DragStart.Y);
                    }
                };
                #endregion
            }
            #endregion
        }

        // Draggable and Resizeable richtextboxes
        [Serializable]
        public class Tile2 : RichTextBox
        {
            public Guid ID { get; }
            [DllImport("user32.dll")]
            static extern IntPtr GetWindowDC(IntPtr hWnd);
            [DllImport("User32.dll")]
            static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

            #region events

            public event EventHandler TileClicked;
            public event EventHandler TileRightClicked;
            public event EventHandler TileMoved;
            public bool Selected { get; set; }



            private void TileClickedFunction()
            {
                if (TileClicked != null)
                    TileClicked(this, new EventArgs());
            }

            private void TileRightClickedFunction()
            {
                if (TileRightClicked != null)
                    TileRightClicked(this, new EventArgs());
            }

            private void TileMovedFunction()
            {
                if (TileMoved != null)
                    TileMoved(this, new EventArgs());
            }

            #endregion

            #region windows messages

            protected override void WndProc(ref Message m)
            {
                const int WM_NCPAINT = 133;
                if (m.Msg == WM_NCPAINT)
                {
                    IntPtr hdc = GetWindowDC(m.HWnd);
                    Graphics g = Graphics.FromHdc(hdc);
                    System.Drawing.Rectangle rDraw = new System.Drawing.Rectangle(0, 0, this.Width - 1, this.Height - 1);
                    Pen pBottom = new Pen(Color.Gray, 3);
                    Pen pTop = new Pen(Color.White, 3);
                    g.DrawRectangle(pBottom, rDraw);
                    Point[] pts = new Point[3];

                    pts[0] = new Point(0, this.Height - 1);
                    pts[1] = new Point(0, 0);
                    pts[2] = new Point(this.Width - 1, 0);


                    g.DrawLines(pTop, pts);
                    ReleaseDC(this.Handle, hdc);
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            bool Dragging = false;
            bool Dragged = false;
            #endregion

            #region init & load

            public Tile2()
            {
                if (this.Tag == null) this.Tag = string.Empty;
                ID = Guid.NewGuid();
                #region init variables

                //moving part
                Dragging = false;
                Dragged = false;
                Point DragStart = Point.Empty;

                bool Resizing = false;
                bool Resized = false;
                bool MouseIsInLeftEdge = false;
                bool MouseIsInRightEdge = false;
                bool MouseIsInTopEdge = false;
                bool MouseIsInBottomEdge = false;
                Point CursorStartPoint = new Point();
                Size CurrentStartSize = new Size();
               

                //make the shadow
                this.BorderStyle = BorderStyle.Fixed3D;
                Label label = new Label();
                label.Font = new System.Drawing.Font("Arial", 10, FontStyle.Bold);
                label.Location = new Point(5, 5);
                label.AutoSize = true;
                this.Controls.Add(label);
                #endregion

                #region visible changed

                this.VisibleChanged += delegate (object sender, EventArgs e)
                {
                    TileMovedFunction();
                };

                #endregion

                #region paint

                this.Paint += delegate (object sender, PaintEventArgs e)
                {
                    Graphics g = this.Parent.CreateGraphics();
                    Matrix mx = new Matrix(1F, 0, 0, 1F, 4, 4);
                    System.Drawing.Rectangle rdraw = new System.Drawing.Rectangle(this.Left, this.Top, this.Width, this.Height);
                    g.Transform = mx;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), rdraw);
                    g.Dispose();

                    label.Text = this.Tag.ToString();
                };

                #endregion

                this.MouseClick += delegate (object sender, MouseEventArgs e)
                 {
                     this.Selected = !this.Selected;
                 };

                #region mouse down 

                this.MouseDown += delegate (object sender, MouseEventArgs e)
                {
                    MouseIsInLeftEdge = Math.Abs(e.X) <= 5;
                    MouseIsInRightEdge = Math.Abs(e.X - this.Width) <= 5;
                    MouseIsInTopEdge = Math.Abs(e.Y) <= 5;
                    MouseIsInBottomEdge = Math.Abs(e.Y - this.Height) <= 5;

                    if (MouseIsInLeftEdge || MouseIsInRightEdge || MouseIsInTopEdge || MouseIsInBottomEdge)
                    {
                        Resizing = true;
                        Resized = false;

                        CursorStartPoint = new Point(e.X, e.Y);
                        CurrentStartSize = this.Size;

                        if (MouseIsInLeftEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Cursor = Cursors.SizeNWSE;
                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Cursor = Cursors.SizeNESW;
                            }
                            else
                            {
                                this.Cursor = Cursors.SizeWE;
                            }
                        }
                        else if (MouseIsInRightEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Cursor = Cursors.SizeNESW;
                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Cursor = Cursors.SizeNWSE;
                            }
                            else
                            {
                                this.Cursor = Cursors.SizeWE;
                            }

                        }
                        else if (MouseIsInTopEdge || MouseIsInBottomEdge)
                        {
                            this.Cursor = Cursors.SizeNS;
                        }
                        else
                        {
                            this.Cursor = Cursors.Default;
                        }

                    }
                    else
                    {
                        Dragging = true;
                        Dragged = false;
                        DragStart = new Point(e.X, e.Y);
                    }
                    this.Capture = true;
                };

                #endregion

                #region mouse up

                this.MouseUp += delegate (object sender, MouseEventArgs e)
                {
                    Dragging = false;
                    Resizing = false;

                    this.Capture = false;

                    if (Dragged || Resized)
                    {
                        TileMovedFunction();
                    }
                    else
                    {
                        if (e.Button == MouseButtons.Right)
                            TileRightClickedFunction();
                        else
                            TileClickedFunction();
                    }
                };

                #endregion

                #region mouse move

                this.MouseMove += delegate (object sender, MouseEventArgs e)
                {
                    if (Resizing)
                    {
                        Resized = false;

                        if (MouseIsInLeftEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Width -= (e.X - CursorStartPoint.X);
                                this.Left += (e.X - CursorStartPoint.X);
                                this.Height -= (e.Y - CursorStartPoint.Y);
                                this.Top += (e.Y - CursorStartPoint.Y);
                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Width -= (e.X - CursorStartPoint.X);
                                this.Left += (e.X - CursorStartPoint.X);
                                this.Height = (e.Y - CursorStartPoint.Y)
                                         + CurrentStartSize.Height;
                            }
                            else
                            {
                                this.Width -= (e.X - CursorStartPoint.X);
                                this.Left += (e.X - CursorStartPoint.X);
                            }
                        }
                        else if (MouseIsInRightEdge)
                        {
                            if (MouseIsInTopEdge)
                            {
                                this.Width = (e.X - CursorStartPoint.X)
                                                + CurrentStartSize.Width;
                                this.Height -= (e.Y - CursorStartPoint.Y);
                                this.Top += (e.Y - CursorStartPoint.Y);

                            }
                            else if (MouseIsInBottomEdge)
                            {
                                this.Width = (e.X - CursorStartPoint.X)
                                                + CurrentStartSize.Width;
                                this.Height = (e.Y - CursorStartPoint.Y)
                                                + CurrentStartSize.Height;
                            }
                            else
                            {
                                this.Width = (e.X - CursorStartPoint.X)
                                               + CurrentStartSize.Width;
                            }
                        }
                        else if (MouseIsInTopEdge)
                        {
                            this.Height -= (e.Y - CursorStartPoint.Y);
                            this.Top += (e.Y - CursorStartPoint.Y);
                        }
                        else if (MouseIsInBottomEdge)
                        {
                            this.Height = (e.Y - CursorStartPoint.Y)
                                       + CurrentStartSize.Height;
                        }
                        else
                        {
                            Resizing = false;
                        }
                    }
                    else if (Dragging)
                    {
                        Dragged = true;
                        this.Left = Math.Max(0, e.X + this.Left - DragStart.X);
                        this.Top = Math.Max(0, e.Y + this.Top - DragStart.Y);
                    }
                };
                #endregion


            }
            #endregion
        }

        // Save button
        private void savetryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveData(label);
        }

        private void Header_Click(object sender, EventArgs e)
        {
           
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
        public static class ImageHelper
        {
            // converts image to base64 string
            public static string ToBase64(this Image image)
            {
                using (var ms = new MemoryStream())
                {
                    var bmp = image as Bitmap;
                    bmp.Save(ms, bmp.RawFormat);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }
        }
}

