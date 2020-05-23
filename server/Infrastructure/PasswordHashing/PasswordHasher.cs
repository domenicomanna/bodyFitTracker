using System;
using System.Security.Cryptography;
using Api.ApplicationLogic.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.PasswordHashing
{
    public class PasswordHasher : IPasswordHasher
    {
        const KeyDerivationPrf psuedoRandomFunction = KeyDerivationPrf.HMACSHA1; // default for Rfc2898DeriveBytes
        const int iterationCount = 1000; // default for Rfc2898DeriveBytes
        const int keyLength = 256 / 8; // 256 bits
        const int saltSize = 128 / 8; // 128 bits

        /// <summary>
        /// Generates a hashed version of the given <paramref name="plainTextPassword"/>. Both the hashed password and the salt that is used to hash the password will be returned.
        /// </summary>
        public (string hashedPassword, string salt) GeneratePassword(string plainTextPassword)
        {
            byte[] salt = GenerateSalt();
            string hashedPassword = GenerateHashedPassword(plainTextPassword, salt);
            return (hashedPassword, Convert.ToBase64String(salt));
        }

        /// <summary>
        /// Returns a randomized byte array of size <see cref="saltSize"/> representing the salt to use for password hashing
        /// </summary>
        private byte[] GenerateSalt()
        {
            byte[] salt = new byte[saltSize];
            using (var randomNumberGenerator = RandomNumberGenerator.Create())
            {
                randomNumberGenerator.GetBytes(salt);
            }
            return salt;
        }

        /// <summary>
        /// Returns true if the given <paramref name="plainTextPassword"/>, once hashed with the given <paramref name="salt"/>, is equal to the <paramref name="hashedPassword"/>. 
        /// Note, for this method to return true, the value of the <paramref name="salt"/> must be the same value as
        /// the salt that was used to generate the <paramref name="hashedPassword"/>.
        /// <param name="salt"> This must be a valid base64 string, or else a <see cref="FormatException"/> will be thrown. </param>
        /// </summary>
        public bool ValidatePlainTextPassword(string plainTextPassword, string hashedPassword, string salt)
        {
            byte[] saltBytes = Convert.FromBase64String(salt);
            string regeneratedHashPassword = GenerateHashedPassword(plainTextPassword, saltBytes);
            return hashedPassword.Equals(regeneratedHashPassword);
        }

        /// <summary>
        /// Hashes the given <paramref name="plainTextPassword"/> with the given <paramref name="salt"/>
        /// </summary>
        private string GenerateHashedPassword(string plainTextPassword, byte[] salt)
        {
            byte[] hashedBytes = KeyDerivation.Pbkdf2(password: plainTextPassword, salt: salt,
                prf: psuedoRandomFunction, iterationCount: iterationCount, numBytesRequested: keyLength);
            return Convert.ToBase64String(hashedBytes);
        }

    }
}