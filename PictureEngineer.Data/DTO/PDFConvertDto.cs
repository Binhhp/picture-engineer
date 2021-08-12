using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.Data.DTO
{
    public class PDFTranslateDto
    { 
        public string FilePath { get; set; }
        public string SourceLanguage { get; set; }
        public string TextLanguage { get; set; }
    }
    public class PDFExtractPagesDto
    {
        public int[] PageNumber { get; set; }
        public string FilePath { get; set; }
    }

    public class PDFSplitDto
    {
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public string FilePath { get; set; }
    }

    public class PDFConvertDto
    {
        public string FilePath { get; set; }
        public string FileFormatConvert { get; set; }
    }

    public class DocxToPdf
    {
        public string FilePath { get; set; }
    }
}
