namespace Api.ApplicationLogic.Users.DataTransferObjects
{
    public class ResetPasswordValidationResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public ResetPasswordValidationResult(bool succeeded, string errorMessage = "")
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }
    }
}