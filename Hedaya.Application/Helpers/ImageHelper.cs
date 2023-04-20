using Hedaya.Common;
using Microsoft.AspNetCore.Hosting;
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

        public static async Task<string>SaveImage(IFormFile image, IHostingEnvironment _environment)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\images\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\images\\");
            }
            string ImagePath = "\\uploads\\images\\" + RandomHelper.GenerateUniqueID(25) + Path.GetExtension(image.FileName).ToLower();
            using (FileStream filestream = File.Create(_environment.WebRootPath + ImagePath))
            {
                image.CopyTo(filestream);
                filestream.Flush();
                var fileExtention = Path.GetExtension(image.FileName).ToLower();
            }
            return ImagePath;
        }

        public static string SaveVideo(IFormFile image, IHostingEnvironment _environment)
        {
            if (!Directory.Exists(_environment.WebRootPath + "\\uploads\\videos\\"))
            {
                Directory.CreateDirectory(_environment.WebRootPath + "\\uploads\\videos\\");
            }
            string magePath = "\\uploads\\videos\\" + RandomHelper.GenerateUniqueID(25) + Path.GetExtension(image.FileName).ToLower();
            using (FileStream filestream = File.Create(_environment.WebRootPath + magePath))
            {
                image.CopyTo(filestream);
                filestream.Flush();
                var fileExtention = Path.GetExtension(image.FileName).ToLower();
            }
            return magePath;
        }
    }
}
