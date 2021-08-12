using System.IO;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using PictureEngineer.OCR;
using System.Net.Mime;
using PictureEngineer.Data.DTO;
using PictureEngineer.Data.Common;
using System;

namespace PictureEngineer.Controllers
{
    [ApiController]
    [Consumes(MediaTypeNames.Application.Json)]
    [Produces(MediaTypeNames.Application.Json)]
    [Route("api/ocr")]
    public class OCRController : ControllerBase
    {
        private readonly IHostingEnvironment _hostingEvironment;

        public OCRController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEvironment = hostingEnvironment;
        }

        /// <summary>
        /// Ocr from image scan
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<string> ScanImage([FromBody]ResponseImageDto model)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(model.FilePath) || string.IsNullOrWhiteSpace(model.Language))
                    return BadRequest();

                string extension = Path.GetExtension(model.FilePath);
                var checkExtensionJpg = extension.IndexOf(FileExtensions.JPG);
                var checkExtensionPng = extension.IndexOf(FileExtensions.PNG);
                var checkExtennsionJpeg = extension.IndexOf(FileExtensions.JPEG);

                if (checkExtennsionJpeg != 1 && checkExtensionJpg != 1 && checkExtensionPng != 1)
                {
                    var message = string.Format("File has not been format image {0}|{1}|{2}.", 
                        FileExtensions.JPG, FileExtensions.JPEG, FileExtensions.PNG);

                    return BadRequest(message);
                }

                if (model.Language != LanguageCode.Vietnamese && model.Language != LanguageCode.English) 
                    return BadRequest("Language has not been supported.");

                WebClient webClient = new WebClient();

                byte[] image = webClient.DownloadData(model.FilePath);
                string dataPath, cfgPath, weightsPath;
                try
                {
                    dataPath = Path.Combine(_hostingEvironment.ContentRootPath, "traindata");
                    cfgPath = Path.Combine(Path.Combine(_hostingEvironment.ContentRootPath, "detector"), "yolov3.cfg");
                    weightsPath = Path.Combine(Path.Combine(_hostingEvironment.ContentRootPath, "detector"), "yolov3.weights");
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
                //ocr image
                var OcrEngineer = new TextDetectorEngineer();
                var textDetector = OcrEngineer.Run(image, dataPath, model.Language, weightsPath, cfgPath);

                return Ok(textDetector);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}