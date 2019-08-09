using System.Threading.Tasks;

namespace Genealogix.Records.Api.Services 
{
    public interface IImageService
    {
        Task<string> SaveImage(string fileName, byte[] image);
    }
}