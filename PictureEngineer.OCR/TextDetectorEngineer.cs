using System.IO;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using PictureEngineer.OCR.ImageProcess;
using PictureEngineer.OCR.ImageProcess.Method;
using System.Linq;
using Tesseract;
using PictureEngineer.OCR.Response;
using System.Text;
using PictureEngineer.PDFConversion.Models;
using PictureEngineer.PDFConversion;

namespace PictureEngineer.OCR
{
    public class TextDetectorEngineer
    {
        /// <summary>
        /// scan ocr tesseract
        /// </summary>
        /// <param name="image">image input</param>
        /// <param name="dataPath">train data path</param>
        /// <param name="scanLanguage">language</param>
        /// <returns>Get string text ocr from image scan</returns>
        public ResponseObjectDetector Run(byte[] imageInput, string dataPath, string scanLanguage, string weightYolov3, string cfgYolov3)
        {
            try
            {
                string text = "";
                Bitmap input;
                using(MemoryStream memoryStream = new MemoryStream(imageInput))
                {
                    input = new Bitmap(memoryStream);
                }
                ProcessedImage processedImage = new ProcessedImage();

                Image<Bgr, byte> imgInput = processedImage.ConvertBitmapToImageBGR(input);

                //perpective transform
                Image<Bgr, byte> transform = processedImage.Process(imgInput);
                Image<Bgr, byte> transformCopy = transform.Copy();

                //Object detector by yolov3
                var yolov3Engineer = new Yolov3Detector();

                List<string> classLabels = yolov3Engineer.ClassLabels;
                //Giá trị yolo trả về gồm list ảnh đã phát hiện(conf, box, class)
                var result = yolov3Engineer.Run(transform.ToBitmap(), cfgYolov3, weightYolov3, classLabels);
                //Bắt đầu tách các ảnh đã lọc ra khỏi ảnh đã qua xử lý tiền xử lý ảnh (input: transform)
                var imageDetectors = new List<ResponseDetectorBase>();

                List<ResultObj> resultObjs = new List<ResultObj>();


                var clearBoxes = transform.Copy();
                var gray = clearBoxes.Convert<Gray, byte>();

                var thresholdOtsu = gray.Copy();

                double threshold = CvInvoke.Threshold(gray, thresholdOtsu, 127, 255, ThresholdType.Otsu);
                if (threshold > 185)
                {
                    CvInvoke.Threshold(gray, gray, 127, 255, ThresholdType.Binary);
                }
                else
                {
                    gray = thresholdOtsu.Copy();
                }

                int widthTransform = transform.Width / 2;

                if (result.ObjectDetectedDtos != null && result.ObjectDetectedDtos.Count > 0)
                {

                    var imageRoi = transform.Copy();

                    foreach (var obj in result.ObjectDetectedDtos)
                    {
                        //Lấy danh sách ảnh phát hiện theo class
                        transform.ROI = obj.Boxes;
                        imageRoi = transform.Copy();

                        var total_center_boxes = obj.Boxes.X + obj.Boxes.Y;
                        if (obj.Boxes.X > widthTransform)
                        {
                            total_center_boxes += widthTransform;
                        }

                        var itemObject = new ResultObj
                        {
                            TotalCenter = total_center_boxes
                        };

                        if (imageDetectors.Exists(x => x.ClassLabels.Equals(obj.ClassLabels)))
                        {
                            var img = imageDetectors.FirstOrDefault(x => x.ClassLabels.Equals(obj.ClassLabels));
                            if (img != null)
                            {
                                var imageOutputByDectected = imageRoi.ToBitmap();
                                var imageByteArray = processedImage.ConvertImageToByteArray(imageOutputByDectected);
                                img.Images.Add(imageByteArray);
                                itemObject.Image = imageByteArray;
                            }
                        }
                        else
                        {
                            var imageOutputByDectected = imageRoi.ToBitmap();
                            var imageByteArray = processedImage.ConvertImageToByteArray(imageOutputByDectected);

                            var imageObject = new ResponseDetectorBase();
                            imageObject.ClassLabels = obj.ClassLabels;
                            imageObject.Images.Add(imageByteArray);

                            imageDetectors.Add(imageObject);

                            itemObject.Image = imageByteArray;
                        }

                        resultObjs.Add(itemObject);

                        transform.ROI = Rectangle.Empty;
                        //Xóa ảnh phát hiện ra khỏi ảnh gốc
                        CvInvoke.Rectangle(gray, obj.Boxes, new MCvScalar(255, 255, 255), -1);
                    }
                }
                //Lấy ảnh gốc trnsform đã xóa các đối tượng trong ảnh
                //Tìm kiếm các contour và đưa tesseract nhận diện
                var canny = CannyDetector.AutoProcessed(gray);
                //dilate
                var dilate = canny.Copy();
                var kernel = CvInvoke.GetStructuringElement(ElementShape.Rectangle, new Size(9, 9), new Point(-1, -1));
                CvInvoke.Dilate(canny, dilate, kernel, new Point(-1, -1), 2, BorderType.Default, new MCvScalar(0, 255, 0));

                VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
                CvInvoke.FindContours(dilate, contours, null, RetrType.External, ChainApproxMethod.ChainApproxSimple);

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
                var imageDetectorTesseract = transform.Copy();

                var result1 = new ResultObj
                {
                    TotalCenter = 0
                };
                //Danh sách các contour văn bản giảm dần
                if (item.Count > 0)
                {
                    foreach (var i in item)
                    {

                        byte[] imageByteArray;

                        int key = int.Parse(i.Key.ToString());

                        Point[] points = (contours[key]).ToArray();

                        Rectangle rectangle = CvInvoke.BoundingRectangle(contours[key]);

                        var start_x = rectangle.X;
                        var start_y = rectangle.Y;

                        var total_center = start_x + start_y;
                        if (start_x > widthTransform)
                        {
                            total_center += widthTransform;
                        }

                        var itemObj = new ResultObj
                        {
                            TotalCenter = total_center
                        };

                        transform.ROI = rectangle;
                        imageDetectorTesseract = transform.Copy();

                        imageByteArray = processedImage.ConvertImageToByteArray(imageDetectorTesseract.ToBitmap());

                        using (TesseractEngine tesseractEngine = new TesseractEngine(dataPath, scanLanguage))
                        {

                            var process = tesseractEngine.Process(Pix.LoadFromMemory(imageByteArray), PageSegMode.Auto);
                            string textFromTesseract = process.GetText();
                            text += textFromTesseract;

                            itemObj.Text = textFromTesseract;
                        }

                        transform.ROI = Rectangle.Empty;

                        if (!string.IsNullOrWhiteSpace(itemObj.Text))
                        {
                            resultObjs.Add(itemObj);
                        }
                    }
                }

                //Nếu không tìm thấy contour thực hiện nhận diện với ảnh transform
                else
                {
                    var imageByteArrayConvert = processedImage.ConvertImageToByteArray(imageDetectorTesseract.ToBitmap());


                    using (TesseractEngine tesseractEngine = new TesseractEngine(dataPath, scanLanguage))
                    {

                        var process = tesseractEngine.Process(Pix.LoadFromMemory(imageByteArrayConvert), PageSegMode.Auto);
                        string textFromTesseract = process.GetText();
                        text += textFromTesseract;

                        result1.Text = textFromTesseract;
                    }

                    if (!string.IsNullOrWhiteSpace(text))
                    {
                        resultObjs.Add(result1);
                    }
                }
                //Nếu không nhận diện được chuyển sang tesseract nhận diện ảnh gốc
                if (string.IsNullOrWhiteSpace(text))
                {
                    Image<Bgr, byte> unsharp_Output = processedImage.UnsharpMaskImage(imgInput);
                    var imgArray = processedImage.ConvertImageToByteArray(unsharp_Output.ToBitmap());

                    using (TesseractEngine tesseractEngine = new TesseractEngine(dataPath, scanLanguage))
                    {

                        var process = tesseractEngine.Process(Pix.LoadFromMemory(imgArray), PageSegMode.Auto);
                        string textFromTesseract = process.GetText();
                        text += textFromTesseract;

                        result1.Text = textFromTesseract;
                    }

                    resultObjs.Add(result1);
                }

                var listResult = resultObjs.OrderBy(x => x.TotalCenter).ToList();
                //return docx
                ResponseFromCreateDocx output = new ResponseFromCreateDocx();
                if (listResult.Count() > 0)
                {
                    output = CreateDocx.Run(listResult);
                    text = output.TextDetector;
                }

                return new ResponseObjectDetector { Text = text, ImageDetector = imageDetectors, Result = output.Docx };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
