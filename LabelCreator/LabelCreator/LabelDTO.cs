using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static LabelCreator.formEditor;

namespace LabelCreator
{
    [Serializable]
    public class LabelDTO
    {
        public int PageWidth { get; set; }

        public int PageHeight { get; set; }

        public Color BackgroundColor { get; set; }

        public List<LabelItemDTO> LabelItems { get; set; }
    }

    public class LabelItemDTO
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public string Value { get; set; }

        public LabelItemType ItemType { get; set; }

        public Font Font { get; set; }

        public Guid ID { get; set; }

        [JsonIgnore]
        public Control ItemReference { get; set; }
    }

    public enum LabelItemType
    {
        None = 0,
        Image = 1,
        RichText = 2,
    }
}
