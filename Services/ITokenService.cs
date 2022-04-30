namespace FirstWebApi.Authentification
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}