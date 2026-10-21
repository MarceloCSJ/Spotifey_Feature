using ArtistConnect.Service.Contracts;

namespace ArtistConnect.Service.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
    Task<AuthTokenResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
}
