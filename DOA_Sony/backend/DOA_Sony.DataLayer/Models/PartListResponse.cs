using DOA_Sony.DataLayer.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DOA_Sony.DataLayer.Models
{
    public class PartListResponse
    {
        public List<PartDTO> PartDTOs { get; set; }
        public bool IsSuccessful { get; set; }
        public string Error { get; set; }
    }
}
