using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace GrayPasportTest2SobelStyle
{
    class Program
    {
        static void Main(string[] args)
        {

           


            using (IplImage image = new IplImage("data/0174.png", LoadMode.GrayScale))
            {

                var sw = System.Diagnostics.Stopwatch.StartNew();
                IplImage rgb = new IplImage(Cv.Size(image.Width/2, image.Height/2), image.Depth, image.NChannels);

                //Вспомогательные изображения
                IplImage imageY = new IplImage(Cv.Size(rgb.Width, rgb.Height), rgb.Depth, rgb.NChannels);
                IplImage imageX = new IplImage(Cv.Size(rgb.Width, rgb.Height), rgb.Depth, rgb.NChannels);

                Cv.Resize(image, rgb);
             rgb.SaveImage("_source.png");

                //Вычисляем градиенты
                rgb.Sobel(imageY,0,1);
            imageY.SaveImage("_Y.png");
                rgb.Sobel(imageX, 1, 0);
      imageX.SaveImage("_X.png");


                //добавим компоненту
                imageX.Add(imageX,imageX);

                imageX.Abs(imageX);
                imageY.Abs(imageY);
               

                imageY.Sub(imageX,rgb);
                rgb.SaveImage("_sobel.png");

                rgb.Threshold(rgb,192,255,ThresholdType.Binary);
              rgb.SaveImage("_binary.png");

                	double a = rgb.Width/155;
		double b = rgb.Height/232;

               

	IplConvKernel kernDilate = new IplConvKernel(11,11,5,5,ElementShape.Ellipse);
		IplConvKernel kernErode = new IplConvKernel(23,23,11,11,ElementShape.Ellipse);

	 rgb.Dilate(rgb,kernDilate);

		
	  rgb.SaveImage("_dialtex1.png");

      rgb.Erode(rgb,kernErode);
      rgb.SaveImage("_erode1.png");

      sw.Stop();
      Console.WriteLine("wasted:{0}", sw.ElapsedMilliseconds);

             //   rgb.Dispose();
            //    imageX.Dispose();
             //   imageY.Dispose();
            }




          

        }
    }
}
