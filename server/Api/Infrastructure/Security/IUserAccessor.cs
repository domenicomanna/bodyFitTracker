using Api.Domain.Models;

namespace Api.Infrastructure.Security
{
    public interface IUserAccessor
    {
        int GetCurrentUserId();
        GenderType GetCurrentUsersGender();
    }
}