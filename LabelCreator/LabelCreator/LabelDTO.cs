using System.Collections.Generic;
using System.Drawing;

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
