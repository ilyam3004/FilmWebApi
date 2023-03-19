namespace UserService.Dtos.Responses;

public class RegisterResponse
{
    //public string Login { get; set; }
    //public string Token { get; set; }
    public Guid UserId { get; set; }
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;

}

