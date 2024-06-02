namespace AMST4.Carousel.MVC.Helper;

public static class ImageHelper
{
    public static async Task<string> SaveImageAsync(IFormFile image, string folderName)
    {
        if (image == null || image.Length == 0)
        {
            return null;
        }

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", folderName, fileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await image.CopyToAsync(stream);
        }

        return Path.Combine("images", folderName, fileName);
    }

    public static void DeleteImage(string imageUrl)
    {
        if (!string.IsNullOrEmpty(imageUrl))
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imageUrl);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
