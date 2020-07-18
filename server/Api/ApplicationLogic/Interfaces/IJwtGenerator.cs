using Api.Domain.Models;

namespace Api.ApplicationLogic.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(AppUser appUser);
    }
}