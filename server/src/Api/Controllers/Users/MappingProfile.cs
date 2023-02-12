using System;
using Api.Controllers.Users.Common;
using Api.Domain.Models;
using AutoMapper;

namespace Api.Controllers.Users
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<decimal, decimal>().ConvertUsing(x => Math.Round(x, 2));
            CreateMap<AppUser, AppUserDTO>().ForMember(x => x.MeasurementPreference, opt => opt.Ignore());
        }
    }
}
