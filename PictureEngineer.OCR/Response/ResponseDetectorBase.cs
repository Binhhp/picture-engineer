using System.Collections.Generic;

namespace PictureEngineer.OCR.Response
{
    public class ResponseDetectorBase
    {
        public List<byte[]> Images { get; set; } = new List<byte[]>();
        public string ClassLabels { get; set; }
    }
}