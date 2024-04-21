using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;
using ZXing;
using static LabelCreator.formEditor;

namespace LabelCreator
{
    public class LabelItemDTO
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Value { get; set; }

        public string BarcodeText { get; set; }

        public LabelItemType ItemType { get; set; }

        public Font Font { get; set; }

        public Guid ID { get; set; }

        public VAlignment VAlign { get; set; }

        public HAlignment HAlign { get; set; }

        public Angle Angle { get; set; }

        public bool isRotated { get; set; }

        public BarcodeFormat BarcodeFormat { get; set; }

        [JsonIgnore]
        public Control ItemReference { get; set; }
    }
}
