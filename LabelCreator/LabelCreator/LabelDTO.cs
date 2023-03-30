using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using static LabelCreator.formEditor;

namespace LabelCreator
{
    public class LabelDTO
    {
        public int PageWidth { get; set; }

        public int PageHeight { get; set; }

        public Color BackgroundColor { get; set; }

        public List<LabelItemDTO> LabelItems { get; set; }
    }
}
