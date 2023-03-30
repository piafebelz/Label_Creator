using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace LabelCreator
{
    public partial class formEditor : Form
    {
        private LabelDTO openData;
        int fontSize = 8;
        string fontName = "MS Sans Serif";
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

            t.SelectionFont = new System.Drawing.Font(fontName, fontSize, newFont);

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
