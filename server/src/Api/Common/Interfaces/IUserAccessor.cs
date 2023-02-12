using Api.Domain.Models;

namespace Api.Common.Interfaces;

public interface IUserAccessor
{
    int GetCurrentUserId();
    GenderType GetCurrentUsersGender();
}
