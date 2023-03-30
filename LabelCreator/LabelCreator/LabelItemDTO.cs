using Newtonsoft.Json;
using System;
using System.Drawing;
using System.Windows.Forms;
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

        public LabelItemType ItemType { get; set; }

        public Font Font { get; set; }

        public Guid ID { get; set; }

        [JsonIgnore]
        public Control ItemReference { get; set; }
    }
}
