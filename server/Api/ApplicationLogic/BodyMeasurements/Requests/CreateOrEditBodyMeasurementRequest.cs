using System;
using Api.Domain.Models;
using Api.Infrastructure.Security;
using FluentValidation;

namespace Api.ApplicationLogic.BodyMeasurements.Requests
{
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

            When(GenderTypeIsFemale, () =>
            {
                RuleFor(x => x.HipCircumference).GreaterThan(0).LessThanOrEqualTo(1000).NotEmpty();
            });

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
}