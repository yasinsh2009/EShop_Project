using Microsoft.AspNetCore.Http;

namespace EShop.Application.Utilities;

public static class ImageCreator
{
    public static async Task<string> CreateImage(IFormFile image, string name)
    {
            var uploadDirectory = 
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "content", name);

            if (!Directory.Exists(uploadDirectory))
            {
                Directory.CreateDirectory(uploadDirectory);
            }

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
            var filePath = Path.Combine(uploadDirectory, fileName);

            await using var stream = new FileStream(filePath, FileMode.Create);
            await image.CopyToAsync(stream);

            return fileName;
    }
}