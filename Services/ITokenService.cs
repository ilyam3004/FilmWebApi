namespace FirstWebApi.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}