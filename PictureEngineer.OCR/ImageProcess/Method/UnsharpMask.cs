using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace PictureEngineer.OCR.ImageProcess.Method
{
    public static class UnsharpMask
    {
        /// <summary>
        /// Applying a Convolution Matrix filter
        /// </summary>
        /// <param name="sourceBitmap">bitmap input</param>
        /// <param name="filterMatrix"></param>
        /// <param name="factor"></param>
        /// <param name="bias"></param>
        /// <param name="grayscale"></param>
        /// <returns></returns>
        public static Bitmap ConvolutionFilter(
            Bitmap sourceBitmap,
            double[,] filterMatrix,
            double factor = 1,
            int bias = 0,
            bool grayscale = false)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);


            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];


            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);


            if (grayscale == true)
            {
                float rgb = 0;


                for (int k = 0; k < pixelBuffer.Length; k += 4)
                {
                    rgb = pixelBuffer[k] * 0.11f;
                    rgb += pixelBuffer[k + 1] * 0.59f;
                    rgb += pixelBuffer[k + 2] * 0.3f;


                    pixelBuffer[k] = (byte)rgb;
                    pixelBuffer[k + 1] = pixelBuffer[k];
                    pixelBuffer[k + 2] = pixelBuffer[k];
                    pixelBuffer[k + 3] = 255;
                }
            }


            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;


            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);


            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;


            int byteOffset = 0;


            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;


                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;


                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {


                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);


                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                 filterX + filterOffset];


                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                  filterX + filterOffset];


                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                filterX + filterOffset];
                        }
                    }


                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;


                    if (blue > 255)
                    { blue = 255; }
                    else if (blue < 0)
                    { blue = 0; }


                    if (green > 255)
                    { green = 255; }
                    else if (green < 0)
                    { green = 0; }


                    if (red > 255)
                    { red = 255; }
                    else if (red < 0)
                    { red = 0; }


                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }


            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width,
                                             sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        /// <summary>
        /// Subtracting and Adding Images
        /// </summary>
        /// <param name="subtractFrom"></param>
        /// <param name="subtractValue"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Bitmap SubtractAddFactorImage(
                              Bitmap subtractFrom,
                                  Bitmap subtractValue,
                                   float factor = 1.0f)
        {
            BitmapData sourceData =
                       subtractFrom.LockBits(new Rectangle(0, 0,
                       subtractFrom.Width, subtractFrom.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);


            byte[] sourceBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0,
                                        sourceBuffer.Length);


            byte[] resultBuffer = new byte[sourceData.Stride *
                                           sourceData.Height];


            BitmapData subtractData =
                       subtractValue.LockBits(new Rectangle(0, 0,
                       subtractValue.Width, subtractValue.Height),
                       ImageLockMode.ReadOnly,
                       PixelFormat.Format32bppArgb);


            byte[] subtractBuffer = new byte[subtractData.Stride *
                                             subtractData.Height];


            Marshal.Copy(subtractData.Scan0, subtractBuffer, 0,
                                         subtractBuffer.Length);


            subtractFrom.UnlockBits(sourceData);
            subtractValue.UnlockBits(subtractData);


            double blue = 0;
            double green = 0;
            double red = 0;


            for (int k = 0; k < resultBuffer.Length &&
                           k < subtractBuffer.Length; k += 4)
            {
                blue = sourceBuffer[k] +
                      (sourceBuffer[k] -
                       subtractBuffer[k]) * factor;


                green = sourceBuffer[k + 1] +
                       (sourceBuffer[k + 1] -
                        subtractBuffer[k + 1]) * factor;


                red = sourceBuffer[k + 2] +
                     (sourceBuffer[k + 2] -
                      subtractBuffer[k + 2]) * factor;


                blue = (blue < 0 ? 0 : (blue > 255 ? 255 : blue));
                green = (green < 0 ? 0 : (green > 255 ? 255 : green));
                red = (red < 0 ? 0 : (red > 255 ? 255 : red));


                resultBuffer[k] = (byte)blue;
                resultBuffer[k + 1] = (byte)green;
                resultBuffer[k + 2] = (byte)red;
                resultBuffer[k + 3] = 255;
            }


            Bitmap resultBitmap = new Bitmap(subtractFrom.Width,
                                             subtractFrom.Height);


            BitmapData resultData =
                       resultBitmap.LockBits(new Rectangle(0, 0,
                       resultBitmap.Width, resultBitmap.Height),
                       ImageLockMode.WriteOnly,
                       PixelFormat.Format32bppArgb);


            Marshal.Copy(resultBuffer, 0, resultData.Scan0,
                                       resultBuffer.Length);


            resultBitmap.UnlockBits(resultData);


            return resultBitmap;
        }

        /// <summary>
        /// class matrix
        /// </summary>
        public static class Matrix
        {
            public static double[,] Gaussian3x3
            {
                get
                {
                    return new double[,]
                    { { 1, 2, 1, },
              { 2, 4, 2, },
              { 1, 2, 1, }, };
                }
            }


            public static double[,] Gaussian5x5Type1
            {
                get
                {
                    return new double[,]
                    { { 2, 04, 05, 04, 2 },
              { 4, 09, 12, 09, 4 },
              { 5, 12, 15, 12, 5 },
              { 4, 09, 12, 09, 4 },
              { 2, 04, 05, 04, 2 }, };
                }
            }


            public static double[,] Mean3x3
            {
                get
                {
                    return new double[,]
                    { { 1, 1, 1, },
              { 1, 1, 1, },
              { 1, 1, 1, }, };
                }
            }


            public static double[,] Mean5x5
            {
                get
                {
                    return new double[,]
                    { { 1, 1, 1, 1, 1 },
              { 1, 1, 1, 1, 1 },
              { 1, 1, 1, 1, 1 },
              { 1, 1, 1, 1, 1 },
              { 1, 1, 1, 1, 1 }, };
                }
            }
        }



        /// <summary>
        /// unsharp using gaussian 3x3
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Bitmap UnsharpGaussian3x3(
                                 Bitmap sourceBitmap,
                                 float factor = 1.0f)
        {
            Bitmap blurBitmap = ConvolutionFilter(
                                          sourceBitmap,
                                          Matrix.Gaussian3x3,
                                          1.0 / 16.0);


            Bitmap resultBitmap = SubtractAddFactorImage(sourceBitmap, blurBitmap);


            return resultBitmap;
        }

        /// <summary>
        /// unsharp using gaussian 5x5
        /// </summary>
        /// <param name="sourceBitmap"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static Bitmap UnsharpGaussian5x5(
                                         this Bitmap sourceBitmap,
                                         float factor = 1.0f)
        {
            Bitmap blurBitmap = ConvolutionFilter(
                                          sourceBitmap,
                                          Matrix.Gaussian5x5Type1,
                                          1.0 / 159.0);


            Bitmap resultBitmap = SubtractAddFactorImage(sourceBitmap, blurBitmap);


            return resultBitmap;
        }

    }
}
