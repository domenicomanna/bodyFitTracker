using System;
using System.Security.Cryptography;

namespace Api.Services;

public interface IPasswordResetTokenGenerator
{
    string CreateResetToken();
}

public class PasswordResetTokenGenerator : IPasswordResetTokenGenerator
{
    public string CreateResetToken()
    {
        using (RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create())
        {
            byte[] bytes = new byte[24];
            randomNumberGenerator.GetBytes(bytes);
            string bytesAsbase64String = Convert.ToBase64String(bytes);
            return bytesAsbase64String.Replace('/', '-');
        }
    }
}
