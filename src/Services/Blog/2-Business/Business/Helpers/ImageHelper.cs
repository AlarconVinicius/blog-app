﻿using Business.Models.Image;

namespace Business.Helpers;
public class ImageHelper
{
    public static bool UploadImage(ImageAddDto image)
    {
        if (string.IsNullOrEmpty(image.File))
        {
            throw new Exception("Forneça uma imagem para este produto!");
        }

        var imageDataByteArray = Convert.FromBase64String(image.File);

        var path = image.GetImagePath();
        var filePath = Path.Combine(path, image.Name);

        if (File.Exists(filePath))
        {
            throw new Exception("Já existe um arquivo com este nome!");
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
            //filePath = Path.Combine(path, "07a91ade-f956-4c03-879c-4e2b21a593e0_BoloDeCenoura.jpg");
            throw new FileNotFoundException("A imagem não foi encontrada!");
        }

        byte[] imageDataByteArray = File.ReadAllBytes(filePath);
        string base64String = Convert.ToBase64String(imageDataByteArray);
        imageDto.File = base64String;
        return imageDto;
    }
}
