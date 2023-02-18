using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.Common.Attributes;
using Api.Common.Errors;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Database;
using FluentValidation;
using Api.Services;

namespace Api.Controllers.BodyMeasurements.Features;

public class CreateOrEditBodyMeasurementRequest
{
    public int? IdOfBodyMeasurementToEdit { get; set; } // if a measurement is being created this will be null
    public double NeckCircumference { get; set; }
    public double WaistCircumference { get; set; }
    public double? HipCircumference { get; set; }
    public double Height { get; set; }
    public double Weight { get; set; }
    public DateTime DateAdded { get; set; }
}

public class CreateOrEditBodyMeasurementRequestValidator : AbstractValidator<CreateOrEditBodyMeasurementRequest>
{
    private readonly IUserAccessor _userAccessor;

    public CreateOrEditBodyMeasurementRequestValidator(IUserAccessor userAccessor)
    {
        this._userAccessor = userAccessor;

        When(
            GenderTypeIsFemale,
            () =>
            {
                RuleFor(x => x.HipCircumference).GreaterThan(0).LessThanOrEqualTo(1000).NotEmpty();
            }
        );

        RuleFor(x => x.NeckCircumference).GreaterThan(0).LessThanOrEqualTo(1000).NotEmpty();
        RuleFor(x => x.WaistCircumference).GreaterThan(0).LessThanOrEqualTo(1000).NotEmpty();
        RuleFor(x => x.Height).GreaterThan(0).LessThanOrEqualTo(1000).NotEmpty();
        RuleFor(x => x.Weight).GreaterThan(0).LessThanOrEqualTo(1000).NotEmpty();
        RuleFor(x => x.DateAdded).LessThanOrEqualTo(DateTime.Today).NotEmpty();
    }

    public bool GenderTypeIsFemale(CreateOrEditBodyMeasurementRequest _)
    {
        GenderType usersGender = _userAccessor.GetCurrentUsersGender();
        return usersGender == GenderType.Female;
    }
}

[Inject]
public class CreateOrEditBodyMeasurementHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IUserAccessor _userAccessor;

    public CreateOrEditBodyMeasurementHandler(BodyFitTrackerContext bodyFitTrackerContext, IUserAccessor userAccessor)
    {
        _bodyFitTrackerContext = bodyFitTrackerContext;
        _userAccessor = userAccessor;
    }

    /// <summary>
    /// Creates a new <see cref="BodyMeasurement"/> based off of the <paramref name="createOrEditBodyMeasurementRequest"/>, if the measurement
    /// does not already exist. If the measurement described in the request does exist, then the existing measurement will be edited.
    /// </summary>
    public void Handle(CreateOrEditBodyMeasurementRequest createOrEditBodyMeasurementRequest)
    {
        bool measurementIsBeingCreated = createOrEditBodyMeasurementRequest.IdOfBodyMeasurementToEdit == null;

        if (measurementIsBeingCreated)
        {
            BodyMeasurement bodyMeasurement = CreateBodyMeasurement(createOrEditBodyMeasurementRequest);

            _bodyFitTrackerContext.BodyMeasurements.Add(bodyMeasurement);
            _bodyFitTrackerContext.SaveChanges();
        }
        else
            TryEditingMeasurement(createOrEditBodyMeasurementRequest);
    }

    private BodyMeasurement CreateBodyMeasurement(CreateOrEditBodyMeasurementRequest request)
    {
        int currentUserId = _userAccessor.GetCurrentUserId();
        AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

        BodyMeasurement bodyMeasurement = new BodyMeasurement(
            appUser,
            request.NeckCircumference,
            request.WaistCircumference,
            request.HipCircumference,
            request.Height,
            request.Weight,
            request.DateAdded,
            appUser.MeasurementSystemPreference
        );

        BodyMeasurement bodyMeasurementConvertedToImperial = BodyMeasurementConverter.Convert(
            bodyMeasurement,
            appUser.MeasurementSystemPreference,
            MeasurementSystem.Imperial
        ); // all measurements in the database should be in imperial units

        return bodyMeasurementConvertedToImperial;
    }

    private void TryEditingMeasurement(CreateOrEditBodyMeasurementRequest request)
    {
        int currentUserId = _userAccessor.GetCurrentUserId();
        Dictionary<string, string> errors = new Dictionary<string, string>();
        AppUser appUser = _bodyFitTrackerContext.AppUsers.Where(x => x.AppUserId == currentUserId).First();

        BodyMeasurement bodyMeasurementToEdit = appUser.BodyMeasurements
            .Where(x => x.BodyMeasurementId == request.IdOfBodyMeasurementToEdit)
            .FirstOrDefault();

        if (bodyMeasurementToEdit == null)
        {
            errors.Add("", $"The bodymeasurement with id {request.IdOfBodyMeasurementToEdit} was not found");
            throw new RestException(HttpStatusCode.NotFound, errors);
        }

        // all measurements in the database should be in imperial units

        MeasurementSystem sourceUnits = appUser.MeasurementSystemPreference;
        MeasurementSystem destinationUnits = MeasurementSystem.Imperial;

        bodyMeasurementToEdit.NeckCircumference = MeasurementConverter.ConvertLength(
            request.NeckCircumference,
            sourceUnits,
            destinationUnits
        );

        bodyMeasurementToEdit.WaistCircumference = MeasurementConverter.ConvertLength(
            request.WaistCircumference,
            sourceUnits,
            destinationUnits
        );

        if (request.HipCircumference.HasValue)
        {
            bodyMeasurementToEdit.HipCircumference = MeasurementConverter.ConvertLength(
                (double)request.HipCircumference,
                sourceUnits,
                destinationUnits
            );
        }
        bodyMeasurementToEdit.Height = MeasurementConverter.ConvertLength(
            request.Height,
            sourceUnits,
            destinationUnits
        );

        bodyMeasurementToEdit.Weight = MeasurementConverter.ConvertWeight(
            request.Weight,
            sourceUnits,
            destinationUnits
        );

        bodyMeasurementToEdit.DateAdded = request.DateAdded;

        bodyMeasurementToEdit.BodyFatPercentage = BodyFatPercentageCalculator.CalculateBodyFatPercentage(
            bodyMeasurementToEdit
        );

        _bodyFitTrackerContext.SaveChanges();
    }
}
