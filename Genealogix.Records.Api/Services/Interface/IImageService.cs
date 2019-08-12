using System.Threading.Tasks;

namespace Genealogix.Records.Api.Services 
{
    public interface IImageService
    {
        /// <summary>
        /// Saves the image to the image store.
        /// Generates a thumbnail and saves the thumbnail as well.
        /// </summary>
        /// <param name="fileName">Original file name.</param>
        /// <param name="image">Array of bytes that form the image.</param>
        /// <returns>Unique key that identifies the image in the image store.</returns>
        Task<string> SaveImage(string fileName, byte[] image);

        /// <summary>
        /// Removes image from the image store.
        /// If a thumbnail exists, it is also removed.
        /// </summary>
        /// <param name="key">Unique identifier of the image to remove.</param>
        void DeleteImage(string key);
    }
}