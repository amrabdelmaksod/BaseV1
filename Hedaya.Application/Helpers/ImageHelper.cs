using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Hedaya.Application.Helpers
{
    public class ImageHelper
    {

        public ImageHelper()
        {
        }

        public static async Task<string> SaveImageAsync(IFormFile image, string imagePath)
        {
            if (image == null || image.Length == 0)
                return null;

            var fileName = Path.GetFileNameWithoutExtension(image.FileName);
            var fileExtension = Path.GetExtension(image.FileName);
            fileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + fileExtension;

            var fullPath = Path.Combine(imagePath, fileName);

            using (var fileStream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
