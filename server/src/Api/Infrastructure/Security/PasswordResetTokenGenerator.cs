using System;
using System.Security.Cryptography;
using Api.Common.Interfaces;

namespace Api.Infrastructure.Security;

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
