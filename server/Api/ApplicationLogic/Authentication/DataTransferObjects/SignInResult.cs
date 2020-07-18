namespace Api.ApplicationLogic.Authentication.DataTransferObjects
{
    public class SignInResult
    {
        public bool SignInWasSuccessful { get; set; }
        public string ErrorMessage { get; set; }
        public string Token { get; set; }
    }
}