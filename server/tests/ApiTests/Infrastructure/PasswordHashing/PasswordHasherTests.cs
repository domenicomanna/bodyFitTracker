using Api.Infrastructure.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiTests.Infrastructure.PasswordHashing;

[TestClass]
public class PasswordHasherTests
{
    PasswordHasher _passwordHasher;

    [TestInitialize]
    public void SetUp()
    {
        _passwordHasher = new PasswordHasher();
    }

    [TestMethod]
    public void SaltGenerationOfSamePasswordsShouldNotMatch()
    {
        string plainTextPassword = "abc";
        (string hashedPasswordOne, string saltOne) = _passwordHasher.GeneratePassword(plainTextPassword);
        (string hashedPasswordTwo, string saltTwo) = _passwordHasher.GeneratePassword(plainTextPassword);
        Assert.AreNotEqual(saltOne, saltTwo);
    }

    [TestMethod]
    public void HashGenerationOfSamePasswordsShouldNotMatch()
    {
        string plainTextPassword = "abc";
        (string hashedPasswordOne, string saltOne) = _passwordHasher.GeneratePassword(plainTextPassword);
        (string hashedPasswordTwo, string saltTwo) = _passwordHasher.GeneratePassword(plainTextPassword);
        Assert.AreNotEqual(hashedPasswordOne, hashedPasswordTwo);
    }

    [TestMethod]
    public void ValidationShouldSucceedWhenTheSamePlainTextPasswordAndSaltAreUsed()
    {
        string plainTextPassword = "abc";
        (string hashedPassword, string salt) = _passwordHasher.GeneratePassword(plainTextPassword);
        bool plainTextPasswordIsValid = _passwordHasher.ValidatePlainTextPassword(
            plainTextPassword,
            hashedPassword,
            salt
        );
        Assert.IsTrue(plainTextPasswordIsValid);
    }

    [TestMethod]
    public void ValidationShouldFailWhenADifferentSaltIsUsed()
    {
        string plainTextPassword = "abc";
        (string hashedPasswordOne, string originalSalt) = _passwordHasher.GeneratePassword(plainTextPassword);
        (string hashedPasswordTwo, string differentSalt) = _passwordHasher.GeneratePassword(plainTextPassword); // create a new password to get a valid base 64 string to use for the different salt
        bool plainTextPasswordIsValid = _passwordHasher.ValidatePlainTextPassword(
            plainTextPassword,
            hashedPasswordOne,
            differentSalt
        );
        Assert.IsFalse(plainTextPasswordIsValid);
    }

    [TestMethod]
    public void ValidationShouldFailWhenADifferentPlainTextPasswordIsUsed()
    {
        string plainTextPassword = "abc";
        (string hashedPassword, string salt) = _passwordHasher.GeneratePassword("123");
        bool plainTextPasswordIsValid = _passwordHasher.ValidatePlainTextPassword(
            plainTextPassword,
            hashedPassword,
            salt
        );
        Assert.IsFalse(plainTextPasswordIsValid);
    }
}
