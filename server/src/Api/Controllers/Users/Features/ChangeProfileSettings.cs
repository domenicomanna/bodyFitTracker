using System.Linq;
using Api.Common.Attributes;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Infrastructure.Database;
using FluentValidation;

namespace Api.Controllers.Users.Features;

public class ChangeProfileSettingsRequest
{
    public string Email { get; set; }
    public double Height { get; set; }
    public MeasurementSystem UnitsOfMeasure { get; set; }
}

public class ChangeProfileSettingsRequestValidator : AbstractValidator<ChangeProfileSettingsRequest>
{
    public ChangeProfileSettingsRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Height).GreaterThan(0);
        RuleFor(x => x.UnitsOfMeasure).IsInEnum();
    }
}

[Inject]
public class ChangeProfileSettingsHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IUserAccessor _userAccessor;

    public ChangeProfileSettingsHandler(BodyFitTrackerContext bodyFitTrackerContext, IUserAccessor userAccessor)
    {
        this._bodyFitTrackerContext = bodyFitTrackerContext;
        this._userAccessor = userAccessor;
    }

    public void Handle(ChangeProfileSettingsRequest changeProfileSettingsRequest)
    {
        int currentUserId = _userAccessor.GetCurrentUserId();
        AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

        appUser.Email = changeProfileSettingsRequest.Email;
        // all units must be in imperial in the database
        appUser.Height = MeasurementConverter.ConvertLength(
            changeProfileSettingsRequest.Height,
            changeProfileSettingsRequest.UnitsOfMeasure,
            MeasurementSystem.Imperial
        );
        appUser.MeasurementSystemPreference = changeProfileSettingsRequest.UnitsOfMeasure;

        _bodyFitTrackerContext.SaveChanges();
    }
}
