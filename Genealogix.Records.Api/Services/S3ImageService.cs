using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Genealogix.Records.Api.Services 
{
    /// <summary>
    /// Provides access to image data store.
    /// </summary>
    public sealed class S3ImageService : IImageService
    {
        IAmazonS3 _client;

        /// <summary>
        /// DI constructor
        /// </summary>
        /// <param name="client">instance of Amazon S3 client.</param>
        public S3ImageService(IAmazonS3 client)
        {
            _client = client;
        }

        public async Task<string> SaveImage(string fileName, byte[] image)
        {
            string key = Guid.NewGuid().ToString();
            
            // Create memory stream from the bytes of the image body.
            Stream stream = new MemoryStream(image);
            
            PutObjectRequest request = new PutObjectRequest
            {
                BucketName = "genealogix-images-dev",
                InputStream = stream,
                AutoCloseStream = true,
                Key = key
            };
            request.Metadata.Add("x-amz-meta-title", fileName);

            var response = await _client.PutObjectAsync(request);
            
            return key;
        }
    }
}