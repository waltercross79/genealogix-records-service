using System;
using System.IO;
using System.Runtime.CompilerServices;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

[assembly: InternalsVisibleToAttribute("Genealogix.Records.Api.Tests")]

namespace Genealogix.Records.Api.Services 
{
    sealed class ImageResizer 
    {
        /// <summary>
        /// Returns square sized thumbnail.
        /// </summary>
        /// <param name="recordImage">Image details.</param>
        /// <param name="size">Width and height of the thumbnail.</param>
        /// <returns>Thumbnail byte array and original file name.</returns>
        public ImageInfo GetThumbnail(ImageInfo recordImage, int size) {
            return GetThumbnail(recordImage, size, size);
        } 

        /// <summary>
        /// Returns thumbnail of the given height and width.
        /// </summary>
        /// <param name="recordImage">Image details.</param>
        /// <param name="width">Widths of the thumbnail.</param>
        /// <param name="height">Height of the thumbnail.</param>
        /// <returns>Thumbnail byte array and original file name.</returns>
        public ImageInfo GetThumbnail(ImageInfo recordImage, int width, int height) 
        {
            IImageFormat imageFormat;

            using(Image<Rgba32> img = Image.Load(recordImage.Image, out imageFormat))
            {
                using (var thumbnail = img.Clone())
                {
                    thumbnail.Mutate(i => 
                            i.Resize(new ResizeOptions{ Size = new Size(width, height) }));

                    using (Stream s = new MemoryStream()) 
                    {
                        thumbnail.Save(s, imageFormat);

                        int length = (int)s.Length; // This is a thumbnail so the size will be well within Int32 range.
                        var thumbnailBytes = new byte[length];
                        s.Position = 0; // Reset position to beginning.
                        s.Read(thumbnailBytes, 0, length);

                        ImageInfo result = new ImageInfo(thumbnailBytes, recordImage.FileName);

                        return result;
                    }
                }
            }
        } 
    }

    /// <summary>
    /// Crate to transport record image and filename.
    /// </summary>
    sealed class ImageInfo 
    {
        public ImageInfo(byte[] image, string fileName)
        {
            Image = image;
            FileName = fileName;
        }

        /// <summary>
        /// Image bytes.
        /// </summary>
        public byte[] Image { get; set; }     

        /// <summary>
        /// Original filename of the image.
        /// </summary>
        /// <value></value>
        public string FileName { get; set; }
    }
}