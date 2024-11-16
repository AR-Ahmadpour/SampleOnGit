namespace Accreditation.Infrastructure.Services
{
    public interface IImageService
    {
        byte[] GetThumbnail(byte[] fileBytes, int size);
    }

    public class ImageService : IImageService
    {
        public byte[] GetThumbnail(byte[] fileBytes, int size)
        {
       
        }
    }
}
