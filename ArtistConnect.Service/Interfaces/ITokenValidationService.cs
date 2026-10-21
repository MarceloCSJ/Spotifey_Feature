using Microsoft.IdentityModel.Tokens;

namespace ArtistConnect.Service.Interfaces;

public interface ITokenValidationService
{
    TokenValidationParameters CreateValidationParameters();
}
