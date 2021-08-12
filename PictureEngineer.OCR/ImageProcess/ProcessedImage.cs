using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.IntensityTransform;
using Emgu.CV.Structure;
using PictureEngineer.OCR.ImageProcess.Method;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PictureEngineer.OCR.ImageProcess
{
    public class ProcessedImage
    {
        public Image<Bgr, byte> Process(Image<Bgr, byte> input)
        {
            try
            {
                //Resize
                var inputCopy = input.Copy();
                var orgin = UnsharpMaskImage(inputCopy);

                var image = ResizeBGR(input);
                //Unsharp
                Image<Bgr, byte> unsharp = UnsharpMaskImage(image);
                //Histogram
                var gray = unsharp.Convert<Gray, byte>();

                var histogram = gray.Copy();
                CvInvoke.EqualizeHist(gray, histogram);
                //Gamma correction
                Image<Gray, byte> image_gamma = GammaCorrection(histogram);

                //Auto canny
                var bilater = image_gamma.Copy();

                CvInvoke.BilateralFilter(image_gamma, bilater, 11, 17, 17);
                var median = bilater.Copy();
                CvInvoke.MedianBlur(bilater, median, 5);

                var canny = CannyDetector.AutoProcessed(median);
                //perpective transform
                Image<Bgr, byte> transform = PerpectiveTransformation.Run(canny, orgin);
                return transform;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// convert image to byte array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public byte[] ConvertImageToByteArray(Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }
        
        /// <summary>
        /// convert bitmap to image bgr
        /// </summary>
        /// <param name="bmap"></param>
        /// <returns></returns>
        public Image<Bgr, byte> ConvertBitmapToImageBGR(Bitmap bmap)
        {
            Image<Bgr, byte> ImageEmgu = new Image<Bgr, byte>(bmap.Width, bmap.Height);
            ImageEmgu = BitmapExtension.ToImage<Bgr, byte>(bmap);
            return ImageEmgu;
        }
        /// <summary>
        /// Resize image
        /// </summary>
        /// <param name="input">image bgr</param>
        /// <returns>Resize image</returns>
        public Image<Bgr, byte> ResizeBGR(Image<Bgr, byte> input)
        {
            var p = Math.Abs((input.Height - 500.00) / input.Height);

            var widthChange = input.Width - Convert.ToInt32(Math.Floor(input.Width * p));
            var imgInput = input.Resize(widthChange, 500, Inter.Area);
            return imgInput;
        }
        /// <summary>
        /// Unsharp mask
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public Image<Bgr, byte> UnsharpMaskImage(Image<Bgr, byte> input)
        {
            var blur = input.Copy();
            CvInvoke.GaussianBlur(input, blur, new Size(9, 9), 2.0);
            var unsharp_image = blur.Copy();
            CvInvoke.AddWeighted(input, 1.5, blur, -0.5, 0, unsharp_image);
            return unsharp_image;
        }
        /// <summary>
        /// Gamma correction
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        public Image<Gray, byte> GammaCorrection(Image<Gray, byte> input)
        {
            Mat output = input.Mat;

            MCvScalar scalar = CvInvoke.Mean(output);
            double mid = 0.5;
            double mean = Convert.ToDouble(scalar.V0.ToString());
            double gamma = Math.Log(mid * 255) / Math.Log(mean);

            IntensityTransformInvoke.GammaCorrection(input.Mat, output, (float)gamma);
            var image_gamma_corrected = output.ToImage<Gray, byte>();
            return image_gamma_corrected;
        }
    }
}
