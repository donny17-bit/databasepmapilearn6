using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace databasepmapilearn6.Utilities;

public static class UtlGenerator
{
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

    public static string Jwt(string confJwt, string claim, int expMinutes)
    {
        return "";
    }
}