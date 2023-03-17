namespace UserService.Dtos;

public class UserRegisterDto
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}