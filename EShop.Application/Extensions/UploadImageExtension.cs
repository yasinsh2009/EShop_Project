using EShop.Application.Utilities;
using Microsoft.AspNetCore.Http;

namespace EShop.Application.Extensions
{
    public static class UploadImageExtension
    {
        public static void AddImageToServer(this IFormFile image, string fileName, string originalPath, int? width, int? height, string thumbPath = null, string deleteFileName = null)
        {
            if (image != null && image.IsImage())
            {
                if (!Directory.Exists(originalPath))
                    Directory.CreateDirectory(originalPath);

                if (!string.IsNullOrEmpty(deleteFileName))
                {
                    if (File.Exists(originalPath + deleteFileName))
                        File.Delete(originalPath + deleteFileName);

                    if (!string.IsNullOrEmpty(thumbPath))
                    {
                        if (File.Exists(thumbPath + deleteFileName))
                            File.Delete(thumbPath + deleteFileName);
                    }
                }

                string OriginPath = originalPath + fileName;

                using (var stream = new FileStream(OriginPath, FileMode.Create))
                {
                    if (!Directory.Exists(OriginPath)) image.CopyTo(stream);
                }


                if (!string.IsNullOrEmpty(thumbPath))
                {
                    if (!Directory.Exists(thumbPath))
                        Directory.CreateDirectory(thumbPath);

                    ImageOptimizer resizer = new ImageOptimizer();

                    if (width != null && height != null)
                        resizer.ImageResizer(originalPath + fileName, thumbPath + fileName, width, height);
                }
            }
        }

        public static void DeleteImage(this string imageName, string OriginPath, string ThumbPath)
        {
            if (!string.IsNullOrEmpty(imageName))
            {
                if (File.Exists(OriginPath + imageName))
                    File.Delete(OriginPath + imageName);

                if (!string.IsNullOrEmpty(ThumbPath))
                {
                    if (File.Exists(ThumbPath + imageName))
                        File.Delete(ThumbPath + imageName);
                }
            }
        }
    }
}
