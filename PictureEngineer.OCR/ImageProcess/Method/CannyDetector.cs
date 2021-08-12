using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Text;

namespace PictureEngineer.OCR.ImageProcess.Method
{
    public static class CannyDetector
    {
        public static Image<Gray, byte> AutoProcessed(Image<Gray, byte> gray, double sigma = 0.33)
        {
            var threshold = gray.Copy();
            var otsu = CvInvoke.Threshold(gray, threshold, 127, 255, ThresholdType.Otsu);
            double lower = Math.Max(0, (1.0 - sigma) * otsu);
            double upper = Math.Max(255, (1.0 - sigma) * otsu);
            var edged = gray.Copy();
            CvInvoke.Canny(gray, edged, lower, upper);
            return edged;
        }
    }
}
