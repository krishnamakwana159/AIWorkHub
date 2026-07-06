using AIWorkHub.Domain.Entities;

namespace AIWorkHub.Application.Common.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(User user);

    string GenerateRefreshToken();
}
