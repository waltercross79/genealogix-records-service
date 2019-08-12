using Microsoft.VisualStudio.TestTools.UnitTesting;
using Genealogix.Records.Api.Services;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System.IO;
using SixLabors.ImageSharp.Formats;

namespace Genealogix.Records.Api.Tests
{
    [TestClass]
    public class ImageResizerTests
    {
        private ImageResizer _resizer;
        private static ImageInfo s_image;

        private const string FILE_NAME = "test-image.jpg";

        [ClassInitialize]
        public static void InitClass(TestContext context)
        {
            using (FileStream fs = new FileStream(FILE_NAME, FileMode.Open)) 
            {            
                int length = (int)fs.Length; // This is a thumbnail so the size will be well within Int32 range.
                var image = new byte[length];
                fs.Read(image, 0, length);

                s_image = new ImageInfo(image, FILE_NAME);
            }
        }

        [TestInitialize]
        public void Initialize() 
        {
            _resizer = new ImageResizer();
        }

        /// <summary>
        /// Simple test to verify I have the implementation right.
        /// </summary>
        [TestMethod]
        public void test_ResizeReturnsImageOfExpectedSize() 
        {
            int height = 10;
            int width = 10;
            var imageInfo = _resizer.GetThumbnail(s_image, width, height);

            Image<Rgba32> img = Image.Load(imageInfo.Image);

            Assert.AreEqual(height, img.Height);
            Assert.AreEqual(width, img.Width);

            img.Dispose();
        }
    }
}