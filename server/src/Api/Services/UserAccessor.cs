using System;
using System.Linq;
using System.Security.Claims;
using Api.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace Api.Services;

public interface IUserAccessor
{
    int GetCurrentUserId();
    GenderType GetCurrentUsersGender();
}

public class UserAccessor : IUserAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public int GetCurrentUserId()
    {
        string userId = _httpContextAccessor.HttpContext.User.Claims
            .First(c => c.Type == ClaimTypes.NameIdentifier)
            .Value;
        return Convert.ToInt32(userId);
    }

    public GenderType GetCurrentUsersGender()
    {
        string usersGender = _httpContextAccessor.HttpContext.User.Claims.First(c => c.Type == ClaimTypes.Gender).Value;
        Enum.TryParse(usersGender, out GenderType genderType);
        return genderType;
    }
}
