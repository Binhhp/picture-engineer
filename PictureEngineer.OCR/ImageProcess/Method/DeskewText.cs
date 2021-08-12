using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace PictureEngineer.OCR.ImageProcess.Method
{
    public class DeskewText
    {
        /// <summary>
        /// deskew text
        /// </summary>
        /// <param name="img">input image gray</param>
        public static Image<Gray, byte> Run(Image<Gray, byte> img)
        {
            var gray = img.ThresholdBinaryInv(new Gray(200), new Gray(255)).Dilate(5);

            VectorOfPoint points = new VectorOfPoint();
            CvInvoke.FindNonZero(gray, points);
            var minareaRect = CvInvoke.MinAreaRect(points);

            var rotationMatrix = new Mat(new Size(2, 3), DepthType.Cv32F, 1);
            var rotatedImage = img.CopyBlank();
            if(minareaRect.Angle < -45)
            {
                minareaRect.Angle = 90 + minareaRect.Angle;
            }

            CvInvoke.GetRotationMatrix2D(minareaRect.Center, minareaRect.Angle, 1.0, rotationMatrix);
            CvInvoke.WarpAffine(img, rotatedImage, rotationMatrix, img.Size, Inter.Cubic,
                borderMode: BorderType.Replicate);
            return rotatedImage;
        }
    }
}
