using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.PDFProcessing
{
    public class SplitMultiplePdf
    {
        public byte[] SplitPdf(byte[] pdfInput, int startPage, int endPage)
        {

            try
            {
                PdfReader reader = null;
                Document sourceDocument = null;
                PdfCopy pdfCopyProvider = null;
                PdfImportedPage importedPage = null;

                reader = new PdfReader(pdfInput);

                if (startPage < 0) startPage = 1;
                if (endPage > reader.NumberOfPages) endPage = reader.NumberOfPages;

                sourceDocument = new Document(reader.GetPageSizeWithRotation(startPage));

                MemoryStream memoryStream = new MemoryStream();
                pdfCopyProvider = new PdfCopy(sourceDocument, memoryStream);

                sourceDocument.Open();

                for (int i = startPage; i <= endPage; i++)
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, i);
                    pdfCopyProvider.AddPage(importedPage);
                }

                sourceDocument.Close();
                reader.Close();

                byte[] data = memoryStream.ToArray();
                return data;

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
