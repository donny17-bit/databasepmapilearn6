using System.IdentityModel.Tokens.Jwt;
using System.Text;
using databasepmapilearn6.Configurations;
using databasepmapilearn6.Constans;
using databasepmapilearn6.InputModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Tokens;

namespace databasepmapilearn6.Utilities;

public static class UtlGenerator
{
    public static string LogId() => RandomString(CDefault.LogIdLength);

    public static string ErrorCode() => RandomString(CDefault.ErrorCodeLength, CDefault.ErrorCodeChar);

    public static string RandomString(int length, string allowedChars = CDefault.RandomCharRange)
    {
        Random random = new Random();

        return new string(Enumerable
            .Repeat(allowedChars, length)
            .Select(s => s[random.Next(s.Length)])
            .ToArray());
    }

    public static string GenerateRandom(int length, string chars)
    {
        Random random = new();
        // generate string
        string token = new(Enumerable.Repeat(chars, length) // this will make collection of repeated chars. for example chars: "ABC123" length: 5, this will make chars collection 5 times
            .Select(s => s[random.Next(s.Length)]) // this is used to select random char from chars string
            .ToArray()); // this is used to convert selected char to an array
                         // new method is used to join all the array to become an string
        return token;
    }

    public static string Jwt(ConfJwt confJwt, IMClaim claim, int expMinutes)
    {
        // ntar cari tau symmetric security key 
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(confJwt.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // buat claim
        var c = claim.ToClaim();

        // create secure token
        var secureToken = new JwtSecurityToken(
            issuer: confJwt.Issuer,
            audience: confJwt.Audience,
            claims: c,
            expires: DateTime.Now.AddMinutes(expMinutes),
            signingCredentials: credentials
        );

        // create token from secure token
        var token = new JwtSecurityTokenHandler().WriteToken(secureToken);

        return token;
    }
}