namespace Api.Common.Interfaces
{
    public interface IPasswordResetTokenGenerator
    {
        string CreateResetToken();
    }
}
