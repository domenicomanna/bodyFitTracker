using System;
using System.Linq;
using System.Security.Claims;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Persistence;
using Microsoft.AspNetCore.Http;

namespace Api.Infrastructure.Security
{
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
            string usersGender = _httpContextAccessor.HttpContext.User.Claims
                .First(c => c.Type == ClaimTypes.Gender)
                .Value;
            Enum.TryParse(usersGender, out GenderType genderType);
            return genderType;
        }
    }
}
