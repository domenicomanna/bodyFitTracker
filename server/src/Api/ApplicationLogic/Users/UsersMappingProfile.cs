using System;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.Domain.Models;
using AutoMapper;

namespace Api.ApplicationLogic.Users
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
