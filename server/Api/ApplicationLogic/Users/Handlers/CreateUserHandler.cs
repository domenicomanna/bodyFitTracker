using System.Collections.Generic;
using System.Linq;
using Api.ApplicationLogic.Interfaces;
using Api.ApplicationLogic.Users.DataTransferObjects;
using Api.ApplicationLogic.Users.Requests;
using Domain.Models;
using Persistence;

namespace Api.ApplicationLogic.Users.Handlers
{
    public class CreateUserHandler
    {
        private readonly BodyFitTrackerContext _bodyFitTrackerContext;
        private readonly IPasswordHasher _passwordHasher;

        public CreateUserHandler(BodyFitTrackerContext bodyFitTrackerContext, IPasswordHasher passwordHasher)
        {
            _bodyFitTrackerContext = bodyFitTrackerContext;
            _passwordHasher = passwordHasher;
        }

        public CreateUserResult Handle(CreateUserRequest request)
        {
            Dictionary<string, string> errors = new Dictionary<string, string>();

            bool emailIsTaken = _bodyFitTrackerContext.AppUsers.Where(a => a.Email == request.Email).Any();

            if (emailIsTaken)
            {
                errors.Add(nameof(request.Email), "That email address is not available");
                return new CreateUserResult { Errors = errors };
            }
            
            (string hashedPassword, string salt) = _passwordHasher.GeneratePassword(request.Password);

            AppUser appUser = new AppUser(request.Email, hashedPassword, salt, request.Weight, request.Height, request.Gender, request.MeasurementPreference);

            // _bodyFitTrackerContext.AppUsers.Add(appUser);

            return new CreateUserResult
            {
                Succeeded = true,
                Token = "abc"
            };
        }
    }
}