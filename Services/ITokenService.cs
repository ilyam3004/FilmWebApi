namespace FilmWebApi.Services
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}