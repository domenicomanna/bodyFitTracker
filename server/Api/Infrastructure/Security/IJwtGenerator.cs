using Api.Domain.Models;

namespace Api.Infrastructure.Security
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser appUser);
    }
}