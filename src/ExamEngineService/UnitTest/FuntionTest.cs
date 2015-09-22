using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Exam.Service.Implement;
using Exam.Service.Interface;
using System.ComponentModel.Composition;
using Exam.Model;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing.Drawing2D;

namespace UnitTest
{
    [TestClass]
    public class FuntionTest
    {
        [TestMethod]
        public void PlayTest()
        {
            WorkflowCallWapper.WorkflowProxy proxy = new WorkflowCallWapper.WorkflowProxy();
            var imageData = proxy.GetProcessPictureToByte("参保人员新增");
            CompressImageData(imageData);
        }

        public void CompressImageData(byte[] data)
        {
            Image image = Image.FromStream(new MemoryStream(data));
            image.Save(@"c:\Full.jpg");
            // Get a bitmap.
            Bitmap bmp1 = new Bitmap(image);
            ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);

            // Create an Encoder object based on the GUID
            // for the Quality parameter category.
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object.
            // An EncoderParameters object has an array of EncoderParameter
            // objects. In this case, there is only one
            // EncoderParameter object in the array.
            EncoderParameters myEncoderParameters = new EncoderParameters(1);

            // Save the bitmap as a JPG file with zero quality level compression.
            var myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp1.Save(@"c:\I50L.jpg", jpgEncoder, myEncoderParameters);

            // Save the bitmap as a JPG file with zero quality level compression.
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp1.Save(@"c:\I100L.jpg", jpgEncoder, myEncoderParameters);

            myEncoderParameter = new EncoderParameter(myEncoder, 0L);
            myEncoderParameters.Param[0] = myEncoderParameter;

            bmp1.Save(@"c:\I0L.jpg", jpgEncoder, myEncoderParameters);
            //Bitmap b = new Bitmap(bmp1.Width, bmp1.Height);
            //Graphics g = Graphics.FromImage(bmp1);
            //g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //g.DrawImage(bmp1, new Rectangle(0, 0, bmp1.Width, bmp1.Height), new Rectangle(0, 0, bmp1.Width, bmp1.Height), GraphicsUnit.Pixel);
            //g.Dispose();
            //bmp1.Save(@"c:\OTHER.jpg", jpgEncoder, myEncoderParameters);
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}
