using System.Text.Json.Serialization;

namespace Business.Models.Image;
public abstract class Image
{
    [JsonIgnore]
    private string ImagePath;

    public string Name { get; set; }

    protected Image(string name)
    {
        Name = name;
        ImagePath = GenerateImagePath();
    }

    public string GenerateImagePath()
    {
        var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\assets\\images");


        if (!Directory.Exists(imagePath))
        {
            Directory.CreateDirectory(imagePath);
        }
        return imagePath;
    }

    public string GetImagePath()
    {
        return ImagePath;
    }
}
