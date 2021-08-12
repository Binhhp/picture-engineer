using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using PictureEngineer.OCR.ImageProcess;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace PictureEngineer.OCR
{
    public class EastDetector
    {
        /// <summary>
        /// Ocr text un structure
        /// </summary>
        /// <param name="path">db fronzen east detector</param>
        /// <param name="trainPath">path data train</param>
        /// <param name="data">image input</param>
        /// <returns></returns>
        public static string Run(string path, string trainPath, byte[] data)
        {
            try
            {
                var memoryStream = new MemoryStream(data);
                var bitmapInput = new Bitmap(memoryStream);
                bitmapInput.SetResolution(300, 300);
                var imageProcess = new ProcessedImage();
                var image = imageProcess.ConvertBitmapToImageBGR(bitmapInput);
                memoryStream.SetLength(0);
                memoryStream.Close();

                data = imageProcess.ConvertImageToByteArray(image.ToBitmap());
                Double confThreshold = 0.5;
                Double nmsThreshold = 0.4;
                int inpWidth = 320;
                int inpHeight = 320;

                Net net = DnnInvoke.ReadNet(path);

                VectorOfMat outs = new VectorOfMat();
                string[] outNames = new string[2];
                outNames[0] = "feature_fusion/Conv_7/Sigmoid";
                outNames[1] = "feature_fusion/concat_3";
                Mat frame, blob;
                frame = new Mat(4000, 6000, DepthType.Cv8U, 3);
                CvInvoke.Imdecode(data, ImreadModes.Color, frame);

                if (frame.IsEmpty)
                {
                    return "Error data empty";
                }
                else
                {
                    blob = DnnInvoke.BlobFromImage(frame, 1, new Size(inpWidth, inpHeight), new MCvScalar(123.68, 116.78, 103.94), true, false);
                    net.SetInput(blob);
                    net.Forward(outs, outNames);
                    Mat scores = outs[0];
                    Mat geometry = outs[1];

                    //List<RotatedRect> detections = new List<RotatedRect>();

                    VectorOfRect boxes = new VectorOfRect();
                    VectorOfFloat confidences = new VectorOfFloat();

                    EastDetector.Decode_box(scores, geometry, confThreshold, boxes, confidences);

                    VectorOfInt indices = new VectorOfInt();
                    DnnInvoke.NMSBoxes(boxes, confidences, (float)confThreshold, (float)nmsThreshold, indices);

                    PointF ratio = new PointF((float)((float)frame.Cols / (float)inpWidth), (float)((float)frame.Rows / (float)inpHeight));

                    string text = "";

                    for (int i = 0; i < indices.Size; i++)
                    {
                        Rectangle box = boxes[indices[i]];

                        var vertices_x = box.X * ratio.X;
                        var vertices_y = box.Y * ratio.Y;
                        var vertices_width = box.Width * ratio.X;
                        var vertices_height = box.Height * ratio.Y;

                        var p_x = vertices_x - 0.5 * vertices_width;
                        var p_y = vertices_y - 0.5 * vertices_height;

                        Rectangle box_in = new Rectangle(new Point((int)(p_x), (int)(p_y)), new Size((int)vertices_width, (int)vertices_height));
                        image.ROI = box_in;
                        //empty roi
                        image.ROI = Rectangle.Empty;
                    }

                    return text;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Decode_box(Mat scores, Mat geometry, double scoreThresh, VectorOfRect detections, VectorOfFloat confidences)
        {
            if (detections.Size > 0)
            {
                detections.Clear();
            }

            //CV_Assert(scores.dims == 4); CV_Assert(geometry.dims == 4); CV_Assert(scores.size[0] == 1);
            //CV_Assert(geometry.size[0] == 1); CV_Assert(scores.size[1] == 1); CV_Assert(geometry.size[1] == 5);
            //CV_Assert(scores.size[2] == geometry.size[2]); CV_Assert(scores.size[3] == geometry.size[3]);

            CvException.Equals(scores.Dims, 4);
            CvException.Equals(geometry.Dims, 4);

            var s = scores.SizeOfDimension;
            var sdata = scores.GetData();
            //var x = sdata.GetValue(0,0,0,0);
            var gdata = geometry.GetData();
            //var y = sdata.GetValue(0, 0, 0, 0);

            List<RotatedRect> t_detections = new List<RotatedRect>();
            List<float> t_confidences = new List<float>();
            List<Rectangle> r_detections = new List<Rectangle>();

            for (int y = 0; y < s[2]; ++y)
            {
                for (int x = 0; x < s[3]; x++)
                {
                    float score = (float)sdata.GetValue(0, 0, y, x);
                    if (score < scoreThresh)
                    {
                        continue;
                    }

                    float offsetX = x * 4.0f, offsetY = y * 4.0f;
                    float angle = (float)gdata.GetValue(0, 4, y, x);
                    float cosA = (float)Math.Cos(angle);
                    float sinA = (float)Math.Sin(angle);
                    float h = (float)gdata.GetValue(0, 0, y, x) + (float)gdata.GetValue(0, 2, y, x);
                    float w = (float)gdata.GetValue(0, 1, y, x) + (float)gdata.GetValue(0, 3, y, x);

                    PointF offset = new PointF(offsetX + cosA * (float)gdata.GetValue(0, 1, y, x) + sinA * (float)gdata.GetValue(0, 2, y, x),
                                    offsetY - sinA * (float)gdata.GetValue(0, 1, y, x) + cosA * (float)gdata.GetValue(0, 2, y, x));

                    PointF p1 = new PointF();
                    p1.X = -sinA * h + offset.X;
                    p1.Y = -cosA * h + offset.Y;

                    PointF p3 = new PointF();
                    p3.X = -cosA * w + offset.X;
                    p3.Y = sinA * w + offset.Y;

                    PointF center = new PointF();
                    center.X = (float)0.5 * (p1.X + p3.X);
                    center.Y = (float)0.5 * (p1.Y + p3.Y);

                    SizeF size = new SizeF(w, h);
                    float box_angle = -angle * 180.0f / (float)Math.PI;

                    RotatedRect r = new RotatedRect(center, size, box_angle);
                    Point i_center = new Point((int)(center.X), (int)(center.Y));
                    Size i_size = new Size((int)w, (int)h);

                    Rectangle r_d = new Rectangle(i_center, i_size);

                    r_detections.Add(r_d);
                    t_detections.Add(r);
                    t_confidences.Add(score);

                }
            }

            Rectangle[] k_detections = r_detections.ToArray();
            detections.Push(k_detections);

            float[] k_confidences = t_confidences.ToArray();
            confidences.Push(k_confidences);

        }
    }
}
