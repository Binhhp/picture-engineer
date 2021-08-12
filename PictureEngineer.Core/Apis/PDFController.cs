using System.Net;
using System.IO;
using System;
using System.Text;
using SautinSoft;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PictureEngineer.PDFConversion;
using PictureEngineer.PDFProcessing;
using System.Net.Mime;
using PictureEngineer.Data.Common;
using PictureEngineer.Data.DTO;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/pdfs")]
    public class PDFController : ControllerBase
    {

        /// <summary>
        /// pdf to docx api
        /// </summary>
        /// <param name="input">url file storange</param>
        /// <returns>Pdf to docx</returns>
        [HttpPost("convert")]
        public ActionResult<byte[]> PdfConvert([FromBody]PDFConvertDto input)
        {
            try
            {
                using(WebClient webClient = new WebClient())
                {
                    byte[] file = webClient.DownloadData(input.FilePath);

                    PDFDocumentConvert pDFDocumentConvert = new PDFDocumentConvert();
                    byte[] result;

                    if (input.FileFormatConvert == FileExtensions.DOCX)
                    {

                        result = pDFDocumentConvert.ConvertDOCX(file);
                        return Ok(result);
                    }

                    result = pDFDocumentConvert.ConvertExcel(file);
                    return Ok(result);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Split pdf
        /// </summary>
        /// <param name="input">input param</param>
        /// <returns>Split pdf</returns>
        [HttpPost("split")]
        public ActionResult<byte[]> SplitPdf([FromBody]PDFSplitDto input)
        {
            try
            {
                using(WebClient webClient = new WebClient())
                {
                    byte[] pdf = webClient.DownloadData(input.FilePath);

                    var splitPdf = new SplitMultiplePdf();

                    var result = splitPdf.SplitPdf(pdf, input.StartPage, input.EndPage);

                    return Ok(result);
                }

            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// get page size pdf
        /// </summary>
        /// <param name="file"></param>
        /// <returns>get page size</returns>
        [HttpPost]
        public ActionResult<int> GetPageNumberPdf([FromBody]FileDownload file)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] pdf = webClient.DownloadData(file.FilePath);

                    var pdfDocumentFocus = new PDFDocumentFocus();
                    int page = pdfDocumentFocus.NumberPagePDF(pdf);

                    return Ok(page);
                }
                
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Extract single pdf
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("pages")]
        public ActionResult<byte[]> ExtractPagesPdf([FromBody]PDFExtractPagesDto input)
        {
            try
            {

                using(WebClient webClient = new WebClient())
                {
                    byte[] pdf = webClient.DownloadData(input.FilePath);

                    var pdfDocument = new PDFDocumentFocus();

                    byte[] output;

                    if (input.PageNumber.Length > 1)
                    {
                        output = pdfDocument.ExtractMultiplePages(pdf, input.PageNumber);
                    }
                    else
                    {
                        output = pdfDocument.ExtractSinglePage(pdf, (input.PageNumber)[0]);
                    }

                    return Ok(output);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
        /// <summary>
        /// Convert docx to pdf
        /// </summary>
        /// <param name="input">file path</param>
        /// <returns>Convert docx to pdf</returns>
        [HttpPost("docx")]
        public IActionResult DocxToPdf([FromBody]DocxToPdf input)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] file = webClient.DownloadData(input.FilePath);

                    DocxToPDF docx = new DocxToPDF();

                    byte[] result = docx.Run(file);
                    return Ok(result);
                }
            }
            catch
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }

    public class FileDownload
    {
        public string FilePath { get; set; }
    }
}