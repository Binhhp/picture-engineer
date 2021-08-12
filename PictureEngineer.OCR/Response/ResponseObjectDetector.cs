using PictureEngineer.OCR.Response;
using System.Collections.Generic;

namespace PictureEngineer.OCR.Response
{
    public class ResponseObjectDetector
    {
        public string Text { get; set; }
        public List<ResponseDetectorBase> ImageDetector { get; set; } = new List<ResponseDetectorBase>();
        public byte[] Result { get; set; }
    }
}