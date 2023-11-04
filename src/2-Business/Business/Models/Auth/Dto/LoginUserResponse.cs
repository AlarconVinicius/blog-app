namespace Business.Models.Auth.Dto;

public class LoginUserResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public double ExpiresIn { get; set; }
    public UserToken UserToken { get; set; } = new UserToken();
}
