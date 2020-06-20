using System;
using Api.Domain.Models;
using Api.Infrastructure.Security;
using FluentValidation;

namespace Api.ApplicationLogic.BodyMeasurements.Requests
{
    public class CreateBodyMeasurementRequest
    {
        public decimal NeckCircumference { get; set; }
        public decimal WaistCircumference { get; set; }
        public decimal? HipCircumference { get; set; }
        public decimal Weight { get; set; }
        public DateTime CreationDate { get; set; }
    }


    public class CreateBodyMeasurementRequestValidator : AbstractValidator<CreateBodyMeasurementRequest>
    {
        private readonly IUserAccessor _userAccessor;

        public CreateBodyMeasurementRequestValidator(IUserAccessor userAccessor)
        {
            this._userAccessor = userAccessor;

            When(GenderTypeIsFemale, () =>
            {
                RuleFor(x => x.HipCircumference).GreaterThan(0).LessThanOrEqualTo(1000).NotNull();
            });

            RuleFor(x => x.NeckCircumference).GreaterThan(0).LessThanOrEqualTo(1000).NotNull();
            RuleFor(x => x.WaistCircumference).GreaterThan(0).LessThanOrEqualTo(1000).NotNull();
            RuleFor(x => x.Weight).GreaterThan(0).LessThanOrEqualTo(1000).NotNull();
            RuleFor(x => x.CreationDate).LessThanOrEqualTo(DateTime.Today).NotNull();
        }

        public bool GenderTypeIsFemale(CreateBodyMeasurementRequest _)
        {
            GenderType usersGender = _userAccessor.GetCurrentUsersGender();
            return usersGender == GenderType.Female;
        }

    }
}