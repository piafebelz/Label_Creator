using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using ZXing;
using ZXing.Windows.Compatibility;

namespace LabelCreator
{
    public partial class formEditor : Form
    {
        private LabelDTO openData;
        int fontSize = 8;
        string fontName = "MS Sans Serif";

        //Saves the data
        public void SaveData(LabelDTO label)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + @"\my_file.txt";
            Directory.CreateDirectory(System.IO.Path.GetDirectoryName(path));

            LabelDTO savedData = new LabelDTO();
            if (File.Exists(path))
            {
                using (StreamReader file = File.OpenText(path))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    savedData = (LabelDTO)serializer.Deserialize(file, typeof(LabelDTO));
                }
            }
            /*HashSet<LabelItemDTO> savedItems = new HashSet<LabelItemDTO>();
            if (savedData != null)
            {
                savedItems = new HashSet<LabelItemDTO>(savedData.LabelItems);
            }

            // Clear the current label items before adding new items
            label.LabelItems.Clear();

            /*foreach (LabelItemDTO savedItem in savedItems)
            {
                // Add the previously saved items to the label
                label.LabelItems.Add(savedItem);
            }*/
            foreach (LabelItemDTO item in label.LabelItems)
            {
                if (item.ItemType is LabelItemType.Image || item.ItemType is LabelItemType.StaticImage)
                {
                    var reference = item.ItemReference as Tile;
                    item.X = reference.Location.X;
                    item.Y = reference.Location.Y;
                    item.Width = reference.Width;
                    item.Height = reference.Height;
                    item.Value = reference.Image.ToBase64();
                    item.ID = reference.ID;
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
                    item.VAlign = (VAlignment)reference.VAlignType;
                    item.HAlign = (HAlignment)reference.HAlignType;
                    item.ID = reference.ID;
                }
                else if (item.ItemType is LabelItemType.Barcode)
                {
                    var reference = item.ItemReference as Barcodes;
                    item.X = reference.Location.X;
                    item.Y = reference.Location.Y;
                    item.Width = reference.Width;
                    item.Height = reference.Height;
                    item.Value = reference.Image.ToBase64();
                    item.ID = reference.ID;
                }
                else if (item.ItemType is LabelItemType.Line)
                {
                    var reference = item.ItemReference as Tile3;
                    item.X = reference.Location.X;
                    item.Y = reference.Location.Y;
                    item.Width = reference.Width;
                    item.Height = reference.Height;
                    item.isRotated = reference.isRotated;
                    item.ID = reference.ID;
                }
            }
            /*foreach (LabelItemDTO savedItem in savedItems)
            {
                if (!label.LabelItems.Any(x => x.ID == savedItem.ID))
                {
                    label.LabelItems.Add(savedItem);
                }
                else
                {
                    var index = label.LabelItems.FindIndex(x => x.ID == savedItem.ID);
                    var reference = label.LabelItems[index].ItemReference as Tile;
                    if (reference != null)
                    {
                        label.LabelItems[index].Value = reference.Image.ToBase64();
                    }
                }
            }*/
            if (savedData != null)
            {
                foreach (LabelItemDTO item in savedData.LabelItems)
                {
                    if (!label.LabelItems.Any(x => x.ID == item.ID))
                    {
                        label.LabelItems.Add(item);
                    }
                    else
                    {
                        var index = label.LabelItems.FindIndex(x => x.ID == item.ID);
                        var reference = label.LabelItems[index].ItemReference as Tile;
                        if (reference != null)
                        {
                            label.LabelItems[index].Value = reference.Image.ToBase64();
                        }
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
                        if (lbtype.ItemType == LabelItemType.Image || lbtype.ItemType == LabelItemType.StaticImage)
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
                            picbox.MouseDown += Control_MouseDown;
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
                            txtbox.MouseDown += Control_MouseDown;
                            txtbox.BringToFront();
                            lbtype.ItemReference = txtbox;
                            txtbox.VAlignType = (Tile2.VAlignment)lbtype.VAlign;
                            txtbox.HAlignType = (Tile2.HAlignment)lbtype.HAlign;
                            if (txtbox.VAlignType== Tile2.VAlignment.Top)
                            {
                                txtbox.SelectionStart = 0;
                                // Get the line number of the current selection
                                int lineNumber = txtbox.GetLineFromCharIndex(txtbox.SelectionStart);

                                // Get the index of the first character of the current line
                                int lineStartIndex = txtbox.GetFirstCharIndexFromLine(lineNumber);

                                // Get the index of the first character of the next line
                                int nextLineStartIndex = txtbox.GetFirstCharIndexFromLine(lineNumber + 1);

                                // Calculate the length of the current line in characters
                                int lineLength = nextLineStartIndex - lineStartIndex;


                                if (lineLength >= 0)
                                {
                                    txtbox.SelectionLength = lineLength;
                                }

                                txtbox.SelectionCharOffset = 0;
                            }
                            else if (txtbox.VAlignType == Tile2.VAlignment.Center)
                            {
                                txtbox.SelectionStart = 0;
                                // Get the line number of the current selection
                                int lineNumber = txtbox.GetLineFromCharIndex(txtbox.SelectionStart);

                                // Get the index of the first character of the current line
                                int lineStartIndex = txtbox.GetFirstCharIndexFromLine(lineNumber);

                                // Get the index of the first character of the next line
                                int nextLineStartIndex = txtbox.GetFirstCharIndexFromLine(lineNumber + 1);

                                // Calculate the length of the current line in characters
                                int lineLength = nextLineStartIndex - lineStartIndex;


                                if (lineLength >= 0)
                                {
                                    txtbox.SelectionLength = lineLength;
                                }

                                // Set the vertical alignment of the selected text to Middle
                                txtbox.SelectionCharOffset = -(txtbox.ClientSize.Height - txtbox.GetPositionFromCharIndex(txtbox.TextLength).Y - txtbox.Font.Height) / 2;

                            }
                            else if (txtbox.VAlignType == Tile2.VAlignment.Bottom)
                            {
                                txtbox.SelectionStart = 0;
                                txtbox.SelectAll();
                                // Get the line number of the current selection
                                int lineNumber = txtbox.GetLineFromCharIndex(txtbox.SelectionStart);

                                // Get the index of the first character of the current line
                                int lineStartIndex = txtbox.GetFirstCharIndexFromLine(lineNumber);

                                // Get the index of the first character of the next line
                                int nextLineStartIndex = txtbox.GetFirstCharIndexFromLine(lineNumber + 1);

                                // Calculate the length of the current line in characters
                                int lineLength = nextLineStartIndex - lineStartIndex;


                                if (lineLength >= 0)
                                {
                                    txtbox.SelectionLength = lineLength;
                                }

                                --lineNumber;

                                txtbox.SelectionCharOffset = -(txtbox.ClientSize.Height - lineNumber);
                            }

                            if (txtbox.HAlignType == Tile2.HAlignment.Left)
                            {
                                txtbox.SelectionLength = txtbox.Text.Length;
                                txtbox.SelectionAlignment = HorizontalAlignment.Left;
                            }
                            else if (txtbox.HAlignType == Tile2.HAlignment.Center)
                            {
                                txtbox.SelectionLength = txtbox.Text.Length;
                                txtbox.SelectionAlignment = HorizontalAlignment.Center;
                            }
                            else if (txtbox.HAlignType == Tile2.HAlignment.Right)
                            {
                                txtbox.SelectionLength = txtbox.Text.Length;
                                txtbox.SelectionAlignment = HorizontalAlignment.Right;
                            }

                        }
                        else if (lbtype.ItemType == LabelItemType.Barcode)
                        {
                            Barcodes barcode = new Barcodes();
                            barcode.Size = new Size((int)lbtype.Width, (int)lbtype.Height);
                            barcode.Location = new Point((int)lbtype.X, (int)lbtype.Y);
                            // Gets the Base64 encoded image data
                            string barcodeData = (string)lbtype.Value;

                            // Decodes the Base64 encoded image data
                            byte[] barcodeBytes = Convert.FromBase64String(barcodeData);

                            // Converts the byte array into an Image object
                            MemoryStream memoryStream = new MemoryStream(barcodeBytes);
                            var image = System.Drawing.Image.FromStream(memoryStream);
                            barcode.Image = image;
                            barcode.SizeMode = PictureBoxSizeMode.Zoom;
                            this.Controls.Add(barcode);
                            barcode.MouseDown += Control_MouseDown;
                            barcode.BringToFront();
                            lbtype.ItemReference = barcode;
                            //barcode.isRotated = lbtype.isRotated;
                        }
                        else if (lbtype.ItemType == LabelItemType.Line)
                        {
                            Tile3 line = new Tile3();

                            line.Size = new Size((int)lbtype.Width, (int)lbtype.Height);
                            line.Location = new Point((int)lbtype.X, (int)lbtype.Y);
                            this.Controls.Add(line);
                            line.MouseDown += Control_MouseDown;
                            line.BringToFront();
                            lbtype.ItemReference = line;
                            line.isRotated = lbtype.isRotated;
                        }
                        //this.label.LabelItems.Add(lbtype);
                    }

                    this.label = openData;
                }
            }

        }


        private void richTextBox_SelectionChanged(object sender, EventArgs e)
        {
            Tile2 rtb = sender as Tile2;
            rtb.Selected = true;
            System.Drawing.Font currentFont = rtb.Font;
            rtb.SelectAll();
            ChangeFont(rtb);

        }

        private void TextBox_MouseDown(object sender, MouseEventArgs e)
        {
            lastClickedTextBox = sender as Tile2;
        }


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
            t.SelectAll();
            t.Font = new System.Drawing.Font(fontName, fontSize, newFont);

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

                this.MouseClick += delegate (object sender, MouseEventArgs e)
                {
                    this.Selected = !this.Selected;
                    var thisIndex = this.Parent.Controls.GetChildIndex(this);
                    for (int i = 0; i < this.Parent.Controls.Count; i++)
                    {
                        if (i == thisIndex)
                        {
                            continue;
                        }

                        var child = this.Parent.Controls[i];
                        if (!(child is Tile))
                        {
                            continue;
                        }

                        var tile = child as Tile;
                        tile.Selected = false;
                    }
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
            private int _angle = 90;
            private double _radians;

            private ContentAlignment _alignment = ContentAlignment.TopLeft;

            private int _quadrant = 1;
            public bool Selected { get; set; }
            public enum VAlignment 
            {
                Top = 0,
                Center = 1,
                Bottom = 2,
            }
            public enum HAlignment 
            {
                Left = 0,
                Center = 1,
                Right = 2,
            }

            public VAlignment VAlignType { get; set; }
            public HAlignment HAlignType { get; set; }


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
                const int WM_PAINT = 15;
                const int WM_NCPAINT = 133;
                if (m.Msg == WM_PAINT && !a)
                {
                    // Prevent the control from painting itself when the text is rotated.
                    //using (Graphics graphic = base.CreateGraphics())
                      //  OnPaint(new PaintEventArgs(graphic,base.ClientRectangle));
                    Invalidate();
                }
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

                    if (hdc != IntPtr.Zero)
                    {
                        // Create a pen to draw the border.
                        using (Pen pen = new Pen(Color.Black, 2))
                        {
                            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, Width, Height);
                            g.DrawRectangle(pen, rect);
                            g.Dispose();
                        }
                    }
                    ReleaseDC(this.Handle, hdc);
                }
                else
                {
                    base.WndProc(ref m);
                }
            }
            private bool a = false;

            public bool A
            {
                set { a = value; }
            }

            bool Dragging = false;
            bool Dragged = false;
            #endregion
            private Label label;
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

                this.DoubleBuffered = true;
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
                label = new Label();
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
                    this.BorderStyle = BorderStyle.None;
                    Graphics g = this.Parent.CreateGraphics();
                    Matrix mx = new Matrix(1F, 0, 0, 1F, 4, 4);
                    System.Drawing.Rectangle rdraw = new System.Drawing.Rectangle(this.Left, this.Top, this.Width, this.Height);
                    g.Transform = mx;
                    g.FillRectangle(new SolidBrush(Color.FromArgb(128, Color.Black)), rdraw);
                    g.Dispose();
                    ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, ButtonBorderStyle.Solid);

                    label.Text = this.Tag.ToString();
                };
                


                #endregion

                this.MouseClick += delegate (object sender, MouseEventArgs e)
                {
                    this.Selected = !this.Selected;
                    var thisIndex = this.Parent.Controls.GetChildIndex(this);
                    for (int i = 0; i < this.Parent.Controls.Count; i++)
                    {
                        if (i == thisIndex)
                        {
                            continue;
                        }

                        var child = this.Parent.Controls[i];
                        if (!(child is Tile2))
                        {
                            continue;
                        }

                        var tile = child as Tile2;
                        tile.Selected = false;
                    }
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

        [Serializable]
        public class Tile3 : Control
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
            public bool isRotated { get; set; }
            

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

                    // Set the pen color and width for the line
                    Pen pLine = new Pen(Color.Black, 3);
                    
                    // Draw the line
                    g.DrawLine(pLine, 0, this.Height - 1, this.Width - 1, 0);

                    // Clean up
                    pLine.Dispose();
                    g.Dispose();
                    ReleaseDC(this.Handle, hdc);
                }
                else
                {
                    base.WndProc(ref m);
                }
            }

            #endregion

            #region init & load
            private Pen _pen;

            public Tile3()
            {
                if (this.Tag == null) this.Tag = string.Empty;
                _pen = new Pen(ForeColor, 3);
                #region init variables
                ID = Guid.NewGuid();
                //moving part
                bool Dragging = false;
                bool Dragged = false;
                Point DragStart = Point.Empty;
                this.isRotated = false;

                bool Resizing = false;
                bool Resized = false;
                bool MouseIsInLeftEdge = false;
                bool MouseIsInRightEdge = false;
                bool MouseIsInTopEdge = false;
                bool MouseIsInBottomEdge = false;
                Point CursorStartPoint = new Point();
                Size CurrentStartSize = new Size();

                //make the shadow
                Label label1 = new Label();
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

                GraphicsPath path = new GraphicsPath();
                this.Paint += delegate (object sender, PaintEventArgs e)
                {
                    Graphics g = e.Graphics;
                    Pen pen = new Pen(Color.Black, 1);
                    g.DrawLine(pen, this.Left, this.Top + this.Height / 2, this.Left + this.Width, this.Top + this.Height / 2);
                    pen.Dispose();

                    label1.Text = this.Tag.ToString();
                };

                this.MouseClick += delegate (object sender, MouseEventArgs e)
                {
                    this.Selected = !this.Selected;
                    var thisIndex = this.Parent.Controls.GetChildIndex(this);
                    for (int i = 0; i < this.Parent.Controls.Count; i++)
                    {
                        if (i == thisIndex)
                        {
                            continue;
                        }

                        var child = this.Parent.Controls[i];
                        if (!(child is Tile3))
                        {
                            continue;
                        }

                        var tile = child as Tile3;
                        tile.Selected = false;

                    }
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
                            Dragging = true;
                            Dragged = false;
                            DragStart = new Point(e.X, e.Y);
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
                        if (isRotated)
                        {
                            if (MouseIsInTopEdge)
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
                                this.Left = Math.Max(0, e.X + this.Left - DragStart.X);
                                this.Top = Math.Max(0, e.Y + this.Top - DragStart.Y);
                            }
                        }
                        else
                        {
                            if (MouseIsInLeftEdge)
                            {
                                if (MouseIsInTopEdge)
                                {
                                    this.Width -= (e.X - CursorStartPoint.X);
                                    this.Left += (e.X - CursorStartPoint.X);
                                }
                                else if (MouseIsInBottomEdge)
                                {
                                    this.Width -= (e.X - CursorStartPoint.X);
                                    this.Left += (e.X - CursorStartPoint.X);
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
                                }
                                else if (MouseIsInBottomEdge)
                                {
                                    this.Width = (e.X - CursorStartPoint.X)
                                                    + CurrentStartSize.Width;
                                }
                                else
                                {
                                    this.Width = (e.X - CursorStartPoint.X)
                                                   + CurrentStartSize.Width;
                                }
                            }
                            else
                            {
                                Resizing = false;
                            }
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
            public void GetOriginalSize()
            {
                var tempHKeeper = this.Height;
                var tempWKeeper = this.Width;
            }
            private int count = 0;
            public void RotateLine()
            {
                if(count == 0)
                {
                    var originalHeight = this.Height;
                    var originalWidth = this.Width;
                    isRotated = !isRotated;
                    this.Width = originalHeight;
                    this.Height = originalWidth;
                }
                else
                {
                    isRotated = !isRotated;
                    var temp = this.Width;
                    this.Width = this.Height;
                    this.Height = temp;
                }
                count++;
            }

            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);
                e.Graphics.DrawLine(_pen, new Point(0, Height / 2), new Point(Width, Height / 2));

                int borderWidth = 2;
                ControlPaint.DrawBorder(e.Graphics, this.ClientRectangle, Color.Black, borderWidth, ButtonBorderStyle.Solid, Color.Black, borderWidth, ButtonBorderStyle.Solid, Color.Black, borderWidth, ButtonBorderStyle.Solid, Color.Black, borderWidth, ButtonBorderStyle.Solid);

            }

        }

        [Serializable]
        public class Barcodes : PictureBox
        {
            public Guid ID { get; }
            public BarcodeFormat BarcodeFormat { get; set; }

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

            #endregion



            #region init & load

            public Barcodes()
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

                this.MouseClick += delegate (object sender, MouseEventArgs e)
                {
                    this.Selected = !this.Selected;
                    var thisIndex = this.Parent.Controls.GetChildIndex(this);
                    for (int i = 0; i < this.Parent.Controls.Count; i++)
                    {
                        if (i == thisIndex)
                        {
                            continue;
                        }

                        var child = this.Parent.Controls[i];
                        if (!(child is Barcodes))
                        {
                            continue;
                        }

                        var tile = child as Barcodes;
                        tile.Selected = false;
                    }
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
        private void UpdateLabelItemDTO(Tile tile, Image image)
        {
            foreach (LabelItemDTO item in label.LabelItems)
            {
                if (item.ID == tile.ID)
                {
                    var ms = new MemoryStream();
                    image.Save(ms, ImageFormat.Bmp);
                    string base64 = Convert.ToBase64String(ms.ToArray());
                    item.Value = base64;
                    return;
                }
            }
        }

        private void UpdateLabelItemRotatedBarcode(Barcodes barcode, Image barcodesImage)
        {
            foreach (LabelItemDTO item in label.LabelItems)
            {
                if (item.ID == barcode.ID)
                {
                    var newimage = barcodesImage;
                    var base64 = newimage.ToBase64();

                    using (var ms = new MemoryStream())
                    {
                        newimage.Save(ms, ImageFormat.Jpeg);
                        base64 = Convert.ToBase64String(ms.ToArray());
                    }
                    item.Value = base64;
                }
            }
        }
        private static Bitmap GenerateBarcode(BarcodeFormat barcodeFormat, int width, int height, string value)
        {
            var barcodeWriter = new BarcodeWriter<Bitmap>();
            var renderer = new BitmapRenderer();
            barcodeWriter.Format = barcodeFormat;
            barcodeWriter.Options.Height = height;
            barcodeWriter.Options.Width = width;
            barcodeWriter.Options.PureBarcode = false;
            barcodeWriter.Renderer = renderer;
            var barcode = barcodeWriter.Write(value);
            return barcode;
            
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
                ImageCodecInfo jpgEncoder =
                ImageCodecInfo.GetImageDecoders()
                .First(c => c.FormatID == ImageFormat.Jpeg.Guid);
                        Encoder encoder = Encoder.Quality;
                        EncoderParameters encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(encoder, (long)100);

                bmp.Save(ms, jpgEncoder, encoderParameters);
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    } 


#endregion