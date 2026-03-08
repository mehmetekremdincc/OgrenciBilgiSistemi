using OgrenciBilgiSistemiProject.Models;

namespace OgrenciBilgiSistemiProject.Services.Abstract
{
    public interface ITokenService
    {
        string CreateToken(User user);
    }
}