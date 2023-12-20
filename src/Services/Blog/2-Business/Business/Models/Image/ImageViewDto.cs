namespace Business.Models.Image;
public class ImageViewDto : Image
{
    public string File { get; set; } = string.Empty;

    public ImageViewDto(string name, string file) : base(name)
    {
        File = file;
    }
}
