using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.IntensityTransform;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace PictureEngineer.OCR.ImageProcess.Method
{
    public static class PerpectiveTransformation
    {
        /// <summary>
        /// Perpective tranform image
        /// </summary>
        /// <param name="image"></param>
        /// <returns>Perpective tranform image</returns>
        public static Image<Bgr, byte> Run(Image<Gray, byte> image, Image<Bgr, byte> image_orgin)
        {
            try
            {
                //Find contours
                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(image, contours, null, RetrType.List, ChainApproxMethod.ChainApproxSimple); 

                Dictionary<int, double> dict = new Dictionary<int, double>();

                if (contours.Size > 0)
                {
                    for (int i = 0; i < contours.Size; i++)
                    {
                        double area = CvInvoke.ContourArea(contours[i]);
                        if (area > 1000)
                        {
                            dict.Add(i, area);
                        }
                    }
                }

                var item = dict.OrderByDescending(x => x.Value).ToList();

                if (item.Count() > 0)
                {
                    var sufix = new VectorOfPointF();

                    foreach (var i in item)
                    {
                        int key = int.Parse(i.Key.ToString());

                        double perimate = CvInvoke.ArcLength(contours[key], true);
                        var approx = new VectorOfPointF();

                        CvInvoke.ApproxPolyDP(contours[key], approx, 0.02 * perimate, true);
                    
                        if (approx.Size == 4)
                        {
                            sufix = approx;
                            break;
                        }
                    }

                    if (sufix.Size > 0)
                    {
                        var wrapped = FourPointTransform(image_orgin, sufix);
                        return wrapped.ToImage<Bgr, byte>();
                    }
                    else
                    {
                        return image_orgin;
                    }

                }
                else
                {
                    return image_orgin;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// four transform
        /// </summary>
        /// <param name="src">image input</param>
        /// <param name="points">4 points</param>
        /// <returns>four transform</returns>
        private static Mat FourPointTransform(Image<Bgr, byte> src, VectorOfPointF points)
        {

            double radio = src.Height / (double)500;

            PointF[] pts = points.ToArray();

            pts[0] = new PointF(Convert.ToSingle(pts[0].X * radio), Convert.ToSingle(pts[0].Y * radio));
            pts[1] = new PointF(Convert.ToSingle(pts[1].X * radio), Convert.ToSingle(pts[1].Y * radio));
            pts[2] = new PointF(Convert.ToSingle(pts[2].X * radio), Convert.ToSingle(pts[2].Y * radio));
            pts[3] = new PointF(Convert.ToSingle(pts[3].X * radio), Convert.ToSingle(pts[3].Y * radio));

            PointF tl = new PointF();
            PointF tr = new PointF();
            PointF br = new PointF();
            PointF bl = new PointF();

            double sum_0 = pts[0].X + pts[0].Y;
            double minus_0 = pts[0].Y - pts[0].X;

            double sum_1 = (pts[1].X + pts[1].Y);
            double minus_1 = (pts[1].Y - pts[1].X);

            double sum_2 = (pts[2].X + pts[2].Y);
            double minus_2 = (pts[2].Y - pts[2].X);

            double sum_3 = (pts[3].X + pts[3].Y);
            double minus_3 = (pts[3].Y - pts[3].X);

            double top_left = Math.Min(Math.Min(sum_0, sum_1), Math.Min(sum_2, sum_3));
            if (top_left == sum_0) tl = pts[0];
            if (top_left == sum_1) tl = pts[1];
            if (top_left == sum_2) tl = pts[2];
            if (top_left == sum_3) tl = pts[3];

            double bottom_right = Math.Max(Math.Max(sum_0, sum_1), Math.Max(sum_2, sum_3));
            if (bottom_right == sum_0) br = pts[0];
            if (bottom_right == sum_1) br = pts[1];
            if (bottom_right == sum_2) br = pts[2];
            if (bottom_right == sum_3) br = pts[3];

            double top_right = Math.Min(Math.Min(minus_0, minus_1), Math.Min(minus_2, minus_3));
            if (top_right == minus_0) tr = pts[0];
            if (top_right == minus_1) tr = pts[1];
            if (top_right == minus_2) tr = pts[2];
            if (top_right == minus_3) tr = pts[3];

            double bottom_left = Math.Max(Math.Max(minus_0, minus_1), Math.Max(minus_2, minus_3));
            if (bottom_left == minus_0) bl = pts[0];
            if (bottom_left == minus_1) bl = pts[1];
            if (bottom_left == minus_2) bl = pts[2];
            if (bottom_left == minus_3) bl = pts[3];

            double widthA = Math.Sqrt(Math.Pow(br.X - bl.X, 2) + Math.Pow(br.Y - bl.Y, 2));
            double widthB = Math.Sqrt(Math.Pow(tr.X - tl.X, 2) + Math.Pow(tr.Y - tl.Y, 2));

            double dw = Math.Max(widthA, widthB);
            float maxWidth = Convert.ToSingle(dw);

            double heightA = Math.Sqrt(Math.Pow(tr.X - br.X, 2) + Math.Pow(tr.Y - br.Y, 2));
            double heightB = Math.Sqrt(Math.Pow(tl.X - bl.X, 2) + Math.Pow(tl.Y - bl.Y, 2));

            double dh = Math.Max(heightA, heightB);
            float maxHeight = Convert.ToSingle(dh);

            Mat doc = new Mat(Convert.ToInt32(maxHeight), Convert.ToInt32(maxWidth), DepthType.Cv8U, 4);

            PointF[] src_mat = new PointF[4];
            src_mat[0] = new PointF(tl.X, tl.Y);
            src_mat[1] = new PointF(tr.X, tr.Y);
            src_mat[2] = new PointF(br.X, br.Y);
            src_mat[3] = new PointF(bl.X, bl.Y);

            PointF[] dst_mat = new PointF[4];
            dst_mat[0] = new PointF(0, 0);
            dst_mat[1] = new PointF(maxWidth - 1, 0);
            dst_mat[2] = new PointF(maxWidth - 1, maxHeight - 1);
            dst_mat[3] = new PointF(0, maxHeight - 1);

            var m = CvInvoke.GetPerspectiveTransform(src_mat, dst_mat);
            CvInvoke.WarpPerspective(src, doc, m, doc.Size);
            return doc;
        }
    }
}
