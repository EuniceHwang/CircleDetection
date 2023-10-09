using System;
using System.Drawing;
using OpenCvSharp;

namespace Project
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat src = Cv2.ImRead("circle.png");
            Mat image = new Mat();
            Mat dst = src.Clone();
            Mat ero = new Mat();
            Mat kernel = Cv2.GetStructuringElement(MorphShapes.Rect, new OpenCvSharp.Size(2, 2));

            Cv2.CvtColor(src, image, ColorConversionCodes.BGR2GRAY);
            Cv2.Canny(image, image, 197, 200);
            Cv2.Dilate(image, image, kernel, new OpenCvSharp.Point(-1, -1), 1);
            Cv2.Erode(image, ero, kernel, new OpenCvSharp.Point(-1, -1), 1);
            
            CircleSegment[] circles = Cv2.HoughCircles(ero, HoughModes.Gradient, 1, 25, 50, 10, 15, 18);
            // Cv2.HoughCircles(검출대상이미지, Hough 변환모드, 이미지 스케일링 비율, 원 검출 임계값, 원 중심 간의 최소거리, 최소 반지름, 최대 반지름, 중심 검출 임계값);
            for (int i = 0; i < circles.Length; i++)
            {
                OpenCvSharp.Point center = new OpenCvSharp.Point(circles[i].Center.X, circles[i].Center.Y);

                Cv2.Circle(dst, center, (int)circles[i].Radius, Scalar.Red, 2);
                Cv2.Circle(dst, center, 5, Scalar.AntiqueWhite, Cv2.FILLED);                
            }

            Cv2.PutText(dst, circles.Length.ToString() , new OpenCvSharp.Point(50, 50), HersheyFonts.HersheyComplex, 2.0, new Scalar(0, 0, 0), 3);
            
            Cv2.ImShow("dst", dst);
            Cv2.WaitKey(0);
            Cv2.DestroyAllWindows();
        }
    }
}
