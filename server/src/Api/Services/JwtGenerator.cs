using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Api.Domain.Models;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services;

public interface IJwtGenerator
{
    string CreateToken(AppUser appUser);
}

public class JwtGenerator : IJwtGenerator
{
    readonly string _key;

    public JwtGenerator()
    {
        _key = DotNetEnv.Env.GetString("JWTSecret");
    }

    public string CreateToken(AppUser appUser)
    {
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, appUser.AppUserId.ToString()),
            new Claim(ClaimTypes.Gender, appUser.Gender.ToString())
        };

        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        SigningCredentials signingCredentials = new SigningCredentials(
            securityKey,
            SecurityAlgorithms.HmacSha512Signature
        );
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Expires = DateTime.UtcNow.AddDays(70),
            Subject = new ClaimsIdentity(claims),
            SigningCredentials = signingCredentials
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
