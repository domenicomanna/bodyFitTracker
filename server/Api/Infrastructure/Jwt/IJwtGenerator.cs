using Api.Domain.Models;

namespace Api.Infrastructure.Jwt
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser appUser);
    }
}