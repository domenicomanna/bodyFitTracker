using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.Controllers.BodyMeasurements.Common;
using Api.Common.Errors;
using Api.Common.Interfaces;
using Api.Domain.Models;
using Api.Domain.Services;
using Api.Infrastructure.Database;
using AutoMapper;

namespace Api.Controllers.BodyMeasurements.Features;

public class GetBodyMeasurementHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IMapper _mapper;
    private readonly IUserAccessor _userAccessor;

    public GetBodyMeasurementHandler(
        BodyFitTrackerContext bodyFitTrackerContext,
        IMapper mapper,
        IUserAccessor userAccessor
    )
    {
        _bodyFitTrackerContext = bodyFitTrackerContext;
        _mapper = mapper;
        _userAccessor = userAccessor;
    }

    /// <summary>
    /// Returns the <see cref="BodyMeasurementDTO"/> with the given <paramref name="bodyMeasurementId"/> for the current user.
    /// If a body measurement is not found, then a <see cref="RestException"/> will be thrown.
    /// </summary>
    public BodyMeasurementDTO Handle(int bodyMeasurementId)
    {
        int userId = _userAccessor.GetCurrentUserId();
        Dictionary<string, string> errors = new Dictionary<string, string>();
        AppUser currentUser = _bodyFitTrackerContext.AppUsers.Find(userId);
        BodyMeasurement bodyMeasurement = currentUser.BodyMeasurements
            .Where(x => x.BodyMeasurementId == bodyMeasurementId)
            .FirstOrDefault();

        if (bodyMeasurement == null)
        {
            errors.Add("", $"The body measurement with id {bodyMeasurementId} was not found");
            throw new RestException(HttpStatusCode.NotFound, errors);
        }

        BodyMeasurement measurementInUsersPreferredUnits = BodyMeasurementConverter.Convert(
            bodyMeasurement,
            MeasurementSystem.Imperial,
            currentUser.MeasurementSystemPreference
        );

        return _mapper.Map<BodyMeasurement, BodyMeasurementDTO>(measurementInUsersPreferredUnits);
    }
}
