using Business.Models.Image;
using static System.Net.Mime.MediaTypeNames;

namespace Business.Helpers;
public class ImageHelper
{
    public static bool UploadImage(ImageAddDto image)
    {
        if (string.IsNullOrEmpty(image.File))
        {
            throw new Exception("Forneça uma imagem para este post!");
        }

        var imageDataByteArray = Convert.FromBase64String(image.File);

        var path = image.GetImagePath();
        var filePath = Path.Combine(path, image.Name);

        if (File.Exists(filePath))
        {
            throw new Exception("Já existe uma imagem com este nome!");
        }

        File.WriteAllBytes(filePath, imageDataByteArray);

        return true;
    }

    public static ImageViewDto GetImage(string imageName)
    {
        var imageDto = new ImageViewDto(imageName, "");
        var path = imageDto.GetImagePath();
        var filePath = Path.Combine(path, imageName);

        if (!File.Exists(filePath))
        {
            var defaultPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\default");
            filePath = Path.Combine(defaultPath, "07a91ade-f956-4c03-879c-4e2b21a593e0_Default.jpg");
            //throw new FileNotFoundException("A imagem não foi encontrada!");
        }

        byte[] imageDataByteArray = File.ReadAllBytes(filePath);
        string base64String = Convert.ToBase64String(imageDataByteArray);
        imageDto.File = base64String;
        return imageDto;
    }
    public static bool UpdateImage(ImageAddDto newImage, string oldImage)
    {
        if (string.IsNullOrEmpty(newImage.File))
        {
            throw new Exception("Forneça uma imagem para este post!");
        }

        var imageDataByteArray = Convert.FromBase64String(newImage.File);

        var path = newImage.GetImagePath();
        var newFilePath = Path.Combine(path, newImage.Name);
        var oldFilePath = Path.Combine(path, oldImage);

        if (File.Exists(newFilePath))
        {
            throw new Exception("Já existe um arquivo com este nome!");
        }

        File.WriteAllBytes(newFilePath, imageDataByteArray);

        if (File.Exists(oldFilePath))
        {
            File.Delete(oldFilePath);
        }
        return true;
    }
    public static bool DeleteImage(string image)
    {
        if (string.IsNullOrEmpty(image))
        {
            return false;
        }

        var path = new ImageViewDto("", "").GetImagePath();
        var filePath = Path.Combine(path, image);

        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
        return true;
    }
}
