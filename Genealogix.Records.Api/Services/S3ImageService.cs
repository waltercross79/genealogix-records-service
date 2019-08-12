using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Genealogix.Records.Api.Services 
{
    /// <summary>
    /// Provides access to image data store.
    /// </summary>
    internal sealed class S3ImageService : IImageService
    {
        IAmazonS3 _client;
        ImageResizer _imageResizer;

        private const int THUMBNAIL_SIZE = 150;
        private const string THUMBNAIL_SUFFIX = "_thumb";
        private const string BUCKET_NAME = "genealogix-images-dev";

        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="client">Instance of Amazon S3 client.</param>
        /// <param name="imageResizer">Instance of image processing helper.</param>
        public S3ImageService(IAmazonS3 client, ImageResizer imageResizer)
        {
            _client = client;
            _imageResizer = imageResizer;
        }

        public async Task<string> SaveImage(string fileName, byte[] image)
        {
            string key = Guid.NewGuid().ToString();
            
            // Create memory stream from the bytes of the image body.
            var status = await SaveImageToS3Bucket(image, key, fileName);

            if(status != HttpStatusCode.OK)
                return null;
                        
            // Generate thumbnail and store under key_thum key.
            var thumbnail = _imageResizer.GetThumbnail(new ImageInfo(image, fileName), THUMBNAIL_SIZE);

            string thumbnailKey = key + THUMBNAIL_SUFFIX;
            status = await SaveImageToS3Bucket(thumbnail.Image, thumbnailKey, fileName);
            
            if(status != HttpStatusCode.OK)
            {
                // Delete original image.
                DeleteImage(key);

                return null;
            }
                        
            return key;
        }

        public void DeleteImage(string key)
        {
            DeleteImageFromS3Bucket(key);
        }

        private async Task<HttpStatusCode> SaveImageToS3Bucket(byte[] image, string key, string fileName) 
        {
            using (Stream stream = new MemoryStream(image)) 
            {
                PutObjectRequest request = new PutObjectRequest
                {
                    BucketName = BUCKET_NAME,
                    InputStream = stream,
                    AutoCloseStream = true,
                    Key = key
                };
                request.Metadata.Add("x-amz-meta-title", fileName);

                var response = await _client.PutObjectAsync(request);

                return response.HttpStatusCode;
            }
        }

        private void DeleteImageFromS3Bucket(string key) 
        {
            var keys = new System.Collections.Generic.List<KeyVersion>();
            keys.Add(new KeyVersion{Key = key});
            keys.Add(new KeyVersion{Key = key + THUMBNAIL_SUFFIX});

            var request = new DeleteObjectsRequest {
                BucketName = BUCKET_NAME,
                Objects = keys
            };

            var response = _client.DeleteObjectsAsync(request);
        }
    }
}