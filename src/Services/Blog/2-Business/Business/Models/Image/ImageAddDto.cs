namespace Business.Models.Image;

public class ImageAddDto : Image
{
    public string File { get; set; } = string.Empty;
    public ImageAddDto(string name, string file) : base(name)
    {
        File = file;
    }
}
