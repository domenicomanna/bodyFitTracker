namespace Api.Common.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage emailMessage);
    }
}
