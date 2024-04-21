using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace LabelCreator
{
    public partial class RotatedRTB : UserControl
    {
        public RotatedRTB()
        {
            InitializeComponent();
            this.Paint += RotatedRichTextBox_Paint;
        }


        private void RotatedRichTextBox_Paint(object sender, PaintEventArgs e)
        {
            // Save the current graphics state
            GraphicsState state = e.Graphics.Save();

            // Rotate the graphics context by 90 degrees
            e.Graphics.RotateTransform(90);

            // Translate the graphics context to the origin of the control
            e.Graphics.TranslateTransform(0, -Width);

            // Draw the rotated rich text box
            e.Graphics.DrawString(richTextBox1.Text, richTextBox1.Font, Brushes.Black, new PointF(0, 0));

            // Restore the graphics state
            e.Graphics.Restore(state);
        }

    }
}
