using SautinSoft.Document;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PictureEngineer.PDFConversion
{
    public class DocxToPDF
    {
        /// <summary>
        /// Docx to Pdf
        /// </summary>
        /// <param name="inputData">input docx</param>
        /// <returns>Docx to Pdf</returns>
        public byte[] Run(byte[] inputData)
        {
            using(MemoryStream stream = new MemoryStream(inputData))
            {
                DocumentCore dc = DocumentCore.Load(stream, new DocxLoadOptions());

                using(MemoryStream output = new MemoryStream())
                {
                    dc.Save(output, new PdfSaveOptions());
                    return output.ToArray();
                }
            }
        }
    }
}
