namespace Api.ApplicationLogic.Users.DataTransferObjects
{
    public class ResetPasswordStepTwoResult
    {
        public bool Succeeded { get; set; }
        public string ErrorMessage { get; set; }

        public ResetPasswordStepTwoResult(bool succeeded, string errorMessage = "")
        {
            Succeeded = succeeded;
            ErrorMessage = errorMessage;
        }
    }
}