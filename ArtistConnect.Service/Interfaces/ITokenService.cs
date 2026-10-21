using ArtistConnect.Model.Entities;
using ArtistConnect.Service.Contracts;

namespace ArtistConnect.Service.Interfaces;

public interface ITokenService
{
    AuthTokenResponse GerarToken(Usuario usuario);
}
