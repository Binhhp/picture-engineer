using SautinSoft;
using System;

namespace PictureEngineer.PDFConversion
{
    public class PDFDocumentConvert
    {
        /// <summary>
        /// convert pdf to docx
        /// </summary>
        /// <param name="pdf">pdf format input</param>
        /// <returns>Convert pdf to docx</returns>
        public byte[] ConvertDOCX(byte[] pdf)
        {
            try
            {
                PdfFocus sautinSoft = new PdfFocus();
                sautinSoft.OpenPdf(pdf);
                if (sautinSoft.PageCount > 0)
                {
                    sautinSoft.WordOptions.Format = PdfFocus.CWordOptions.eWordDocument.Docx;
                    var docx = sautinSoft.ToWord();

                    return docx;
                }
                return pdf;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        
        /// <summary>
        /// convert pdf to html
        /// </summary>
        /// <param name="pdf">pdf format input</param>
        /// <returns>Convert pdf to html</returns>
        public byte[] ConvertExcel(byte[] pdf)
        {
            try
            {
                PdfFocus sautinSoft = new PdfFocus();
                sautinSoft.OpenPdf(pdf);
                if (sautinSoft.PageCount > 0)
                {
                    var excel = sautinSoft.ToExcel();

                    return excel;
                }
                return pdf;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
