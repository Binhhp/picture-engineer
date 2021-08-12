using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.PDFConversion.Models
{
    public class ResultObj
    {
        public int TotalCenter { get; set; }
        public string Text { get; set; }
        public byte[] Image { get; set; }
    }
}
