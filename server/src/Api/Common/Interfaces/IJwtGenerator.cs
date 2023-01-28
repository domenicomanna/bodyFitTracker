using Api.Domain.Models;

namespace Api.Common.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser appUser);
    }
}
