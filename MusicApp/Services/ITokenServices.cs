namespace MusicApp.Services
{
    public interface ITokenServices
    {
        string GenerateToken(string userEmail);
    }
}
