using Api.Domain.Models;

namespace Api.ApplicationLogic.Interfaces
{
    public interface IUserAccessor
    {
        int GetCurrentUserId();
        GenderType GetCurrentUsersGender();
    }
}