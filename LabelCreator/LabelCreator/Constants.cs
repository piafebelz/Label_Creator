using System.Windows.Forms;

namespace LabelCreator
{
    public partial class formEditor : Form
    {
        public enum LabelItemType
        {
            None = 0,
            Image = 1,
            RichText = 2,
            Barcode = 3,
            Line = 4,
            StaticImage = 5,
        }

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

        public enum Angle
        {
            _0 = 0,
            _90 = 1,
            _180 = 2,
            _270 = 3,
        }
    }
}
