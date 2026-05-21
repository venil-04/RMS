namespace RMS.Application.Models.Auth;

public class LoginResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public LoggedInUserDto User { get; set; } = new();
}