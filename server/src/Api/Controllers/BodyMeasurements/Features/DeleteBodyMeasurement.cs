using System.Collections.Generic;
using System.Linq;
using System.Net;
using Api.Common.Attributes;
using Api.Common.Exceptions;
using Api.Domain.Models;
using Api.Database;
using Api.Services;

namespace Api.Controllers.BodyMeasurements.Features;

[Inject]
public class DeleteBodyMeasurementHandler
{
    private readonly BodyFitTrackerContext _bodyFitTrackerContext;
    private readonly IUserAccessor _userAccessor;

    public DeleteBodyMeasurementHandler(BodyFitTrackerContext bodyFitTrackerContext, IUserAccessor userAccessor)
    {
        _bodyFitTrackerContext = bodyFitTrackerContext;
        _userAccessor = userAccessor;
    }

    /// <summary>
    /// Deletes the measurement with the id <paramref name="bodyMeasurementIdToDelete"/>. If no measurement is found then a RestException will be thrown. If
    /// the measurement being deleted does not belong to the current user, then a RestException will be thrown.
    /// </summary>
    /// <param name="bodyMeasurementIdToDelete"></param>
    public void Handle(int bodyMeasurementIdToDelete)
    {
        Dictionary<string, string> errors = new Dictionary<string, string>();

        BodyMeasurement bodyMeasurementToRemove = _bodyFitTrackerContext.BodyMeasurements
            .Where(b => b.BodyMeasurementId == bodyMeasurementIdToDelete)
            .FirstOrDefault();

        if (bodyMeasurementToRemove == null)
        {
            errors.Add("", $"The bodymeasurement with id {bodyMeasurementIdToDelete} was not found");
            throw new RestException(HttpStatusCode.NotFound, errors);
        }

        int currentUserId = _userAccessor.GetCurrentUserId();

        if (currentUserId != bodyMeasurementToRemove.AppUserId)
        {
            errors.Add("", "Access to another user's body measurement is denied");
            throw new RestException(HttpStatusCode.Forbidden, errors);
        }

        _bodyFitTrackerContext.BodyMeasurements.Remove(bodyMeasurementToRemove);
        _bodyFitTrackerContext.SaveChanges();
    }
}
