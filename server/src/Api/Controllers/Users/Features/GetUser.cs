using System.Linq;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Database;
using AutoMapper;
using Api.Controllers.Users.Common;
using Api.Common.Attributes;
using Api.Services;

namespace Api.Controllers.Users.Features;

[Inject]
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
        appUserDto.Height = MeasurementConverter.ConvertLength(
            appUser.Height,
            MeasurementSystem.Imperial,
            appUser.MeasurementSystemPreference
        );
        return appUserDto;
    }
}
