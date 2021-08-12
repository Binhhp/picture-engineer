using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Dnn;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace PictureEngineer.OCR
{
    public class Yolov3Detector
    {
        public List<string> ClassLabels
        {
            get
            {
                return new List<string> { "table", "natural image", "math expression" };
            }
        }
        /// <summary>
        /// Yolo v3 text detector
        /// </summary>
        /// <param name="img">image input</param>
        /// <param name="pathCfg">file cfg</param>
        /// <param name="pathWeights">file weights</param>
        /// <param name="classLables">array classes labels</param>
        /// <returns>Yolo v3 text detector</returns>
        public YoloV3Dto Run(Bitmap img, string pathCfg, string pathWeights, List<string> classLables)
        {
            try
            {
                Net Model = null;

                Model = DnnInvoke.ReadNetFromDarknet(pathCfg, pathWeights);

                float confThreshold = 0.6f;
                float nmsThreshold = 0.4f;
                int imgDefaultSize = 608;

                Image<Bgr, byte> image = BitmapExtension.ToImage<Bgr, byte>(img);

                CvInvoke.Resize(image, image, new Size(imgDefaultSize, imgDefaultSize));

                var input = DnnInvoke.BlobFromImage(image, 1 / 255.0, swapRB: true);
                Model.SetInput(input);
                Model.SetPreferableBackend(Emgu.CV.Dnn.Backend.OpenCV);
                Model.SetPreferableTarget(Target.Cpu);

                VectorOfMat vectorOfMat = new VectorOfMat();
                Model.Forward(vectorOfMat, Model.UnconnectedOutLayersNames);

                //post processing
                VectorOfRect bboxes = new VectorOfRect();
                VectorOfFloat scores = new VectorOfFloat();
                VectorOfInt indices = new VectorOfInt();
                for (int k = 0; k < vectorOfMat.Size; k++)
                {
                    var mat = vectorOfMat[k];
                    var data = ArrayTo2DList(mat.GetData());

                    for (int i = 0; i < data.Count; i++)
                    {
                        var row = data[i];
                        var rowsscores = row.Skip(5).ToArray();
                        var classId = rowsscores.ToList().IndexOf(rowsscores.Max());
                        var confidence = rowsscores[classId];

                        if (confidence > confThreshold)
                        {
                            var center_x = (int)(row[0] * img.Width);
                            var center_y = (int)(row[1] * img.Height);

                            var width = (int)(row[2] * img.Width);
                            var height = (int)(row[3] * img.Height);

                            var x = (int)(center_x - (width / 2));
                            var y = (int)(center_y - (height / 2));

                            bboxes.Push(new Rectangle[] { new Rectangle(x, y, width, height) });
                            indices.Push(new int[] { classId });
                            scores.Push(new float[] { confidence });
                        }
                    }
                }

                var idx = DnnInvoke.NMSBoxes(bboxes.ToArray(), scores.ToArray(), confThreshold, nmsThreshold);

                var imgOutput = image.Clone();
                var list = new YoloV3Dto();

                for (int i = 0; i < idx.Length; i++)
                {
                    int index = idx[i];
                    var bbox = bboxes[index];

                    var objectDetector = new ObjectDetectedDto
                    {
                        Boxes = bbox,
                        Confidences = scores[index],
                        ClassLabels = classLables[indices[index]]
                    };

                    list.ObjectDetectedDtos.Add(objectDetector);

                    imgOutput.Draw(bbox, new Bgr(0, 255, 0), 2);
                    CvInvoke.PutText(imgOutput, classLables[indices[index]], new Point(bbox.X, bbox.Y + 20),
                        FontFace.HersheySimplex, 1.0, new MCvScalar(0, 0, 255), 2);
                }

                list.ImageOutput = imgOutput.AsBitmap();

                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private List<float[]> ArrayTo2DList(Array array)
        {
            try
            {
                IEnumerator enumable = array.GetEnumerator();

                int rows = array.GetLength(0);
                int cols = array.GetLength(1);
                List<float[]> list = new List<float[]>();
                List<float> temp = new List<float>();

                for (int i = 0; i < rows; i++)
                {
                    temp.Clear();
                    for (int j = 0; j < cols; j++)
                    {
                        temp.Add(float.Parse(array.GetValue(i, j).ToString()));
                    }
                    list.Add(temp.ToArray());
                }

                return list;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }

    public class ObjectDetectedDto
    {
        public Rectangle Boxes { get; set; }
        public float Confidences { get; set; }
        public string ClassLabels { get; set; }
    }

    public class YoloV3Dto
    {
        public List<ObjectDetectedDto> ObjectDetectedDtos { get; set; } = new List<ObjectDetectedDto>();
        public Bitmap ImageOutput { get; set; }
    }
}
