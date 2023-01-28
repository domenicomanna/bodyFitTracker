using System.Linq;
using System.Net;
using Api.ApplicationLogic.Errors;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Persistence;
using AutoMapper;

namespace Api.ApplicationLogic.Users.Handlers
{
    public class GetUserHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetUserHandler(BodyFitTrackerContext bodyFitTrackerContext, IMapper mapper, IUserAccessor userAccessor)
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public AppUserDTO Handle()
        {
            int currentUserId = _userAccessor.GetCurrentUserId();

            AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();
            AppUserDTO appUserDto = _mapper.Map<AppUser, AppUserDTO>(appUser);
            appUserDto.MeasurementPreference = new MeasurementSystemDTO(appUser.MeasurementSystemPreference);
            appUserDto.Height = MeasurementConverter.ConvertLength(appUser.Height, MeasurementSystem.Imperial, appUser.MeasurementSystemPreference);
            return appUserDto;
        }
    }
}