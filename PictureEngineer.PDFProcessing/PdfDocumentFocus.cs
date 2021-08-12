using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureEngineer.PDFProcessing
{
    public class PDFDocumentFocus
    {
        /// <summary>
        /// get page size pdf
        /// </summary>
        /// <param name="pdf">pdf input</param>
        /// <returns>Get page size pdf</returns>
        public int NumberPagePDF(byte[] pdf)
        {
            try
            {
                var reader = new PdfReader(pdf);
                int pageNumber = reader.NumberOfPages;
                return pageNumber;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// extract pdf single page
        /// </summary>
        /// <param name="pdf">pdf input format byte array</param>
        /// <param name="pageNumber">page number extract in pdf</param>
        /// <returns>Extract single page pdf</returns>
        public byte[] ExtractSinglePage(byte[] pdf, int pageNumber)
        {
            try
            {

                var reader = new PdfReader(pdf);
                PdfImportedPage importedPage = null;

                if (pageNumber > reader.NumberOfPages) pageNumber = reader.NumberOfPages;
                if (pageNumber < 1) pageNumber = 1;
                var document = new Document(reader.GetPageSizeWithRotation(pageNumber));
                
                MemoryStream memoryStream = new MemoryStream();
                PdfCopy pdfCopy = new PdfCopy(document, memoryStream);

                document.Open();
                
                importedPage = pdfCopy.GetImportedPage(reader, pageNumber);
                pdfCopy.AddPage(importedPage);

                document.Close();
                reader.Close();


                byte[] pdfResult = memoryStream.ToArray();
                return pdfResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// extract multiple pages from pdf input
        /// </summary>
        /// <param name="pdf">pdf input format byte array</param>
        /// <param name="extractsPages">array page number pdf</param>
        /// <returns>Extrac multiple pages pdf</returns>
        public byte[] ExtractMultiplePages(byte[] pdf, int[] extractsPages)
        {
            try
            {
                var reader = new PdfReader(pdf);
                PdfImportedPage importedPage;
                var document = new Document(reader.GetPageSizeWithRotation(extractsPages[0]));

                MemoryStream memoryStream = new MemoryStream();
                var pdfCopyProvider = new PdfCopy(document, memoryStream);

                document.Open();
                foreach(int pageNumber in extractsPages)
                {
                    importedPage = pdfCopyProvider.GetImportedPage(reader, pageNumber);
                    pdfCopyProvider.AddPage(importedPage);
                }

                document.Close();
                reader.Close();
                byte[] result = memoryStream.ToArray();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// convert pdf to string
        /// </summary>
        /// <param name="pdf">pdf input format byte array</param>
        /// <returns>Convert pdf object</returns>
        public string ConvertPDFToString(byte[] pdf)
        {
            try
            {
                var sb = new StringBuilder();
                var reader = new PdfReader(pdf);
                var numberPage = reader.NumberOfPages;

                for(var i = 1; i <= numberPage; i++)
                {
                    sb.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return sb.ToString();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
