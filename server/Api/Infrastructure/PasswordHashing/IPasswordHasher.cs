namespace Api.Infrastructure.PasswordHashing
{
    public interface IPasswordHasher
    {
        (string hashedPassword, string salt) GeneratePassword(string plainTextPassword);
        bool ValidatePlainTextPassword(string plainTextPassword, string hashedPassword, string salt);
    }
}