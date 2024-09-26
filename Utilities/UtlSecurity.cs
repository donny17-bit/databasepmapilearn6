using System.Security.Cryptography;
using databasepmapilearn6.Constans;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace databasepmapilearn6.Utilities;

public class UtlSecurity 
{
    public static string HashedPassword(string password)
    {
        // encryption password
        var salt = Salt(16);
        var hash = Hash(password, salt);
        var stringSalt = Convert.ToBase64String(salt);
        var hashedPassword = $"{stringSalt}:{hash}";

        return hashedPassword;
    }

    public static (string rawPassword, string hashedPassword) GeneratePassword(int length)
    {
        // generate random password
        var rnd = new Random();
        var chars = new char[length];
        for (int i=0; i<length; i++) 
        {
            chars[i] = CDefault.RandomCharRange[rnd.Next(0, length)];
        }

        var randomPassword = new string(chars);

        // encrypt password
        string hashedPassword = HashedPassword(randomPassword);

        return (randomPassword, hashedPassword); 
    }

    public static bool ValidatePassword(string hashedPassword, string password)
    {
        string[] parts = hashedPassword.Split(":");
        byte[] salt = Convert.FromBase64String(parts[0]);
        string hash = Hash(password, salt);

        // int valid = parts[1].CompareTo(hash);
        return parts[1].Equals(hash);
    }


    public static byte[] Salt(int length)
    {
        var salt = RandomNumberGenerator.GetBytes(length);

        // yang ini blm tau untuk apa
        // using (var random = RandomNumberGenerator.Create())
        // {
        //     random.GetBytes(salt);
        // }

        return salt; 
    }

    public static string Hash(string password, byte[] salt)
    {
        // Hash reference : https://learn.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-8.0
        // hashing is one way encryption
        // to validate password use salt and inputed password and hash it to compare to hashed password
        byte[] hashcode = KeyDerivation.Pbkdf2(password, salt, KeyDerivationPrf.HMACSHA512, 100000, 16);
        string hashed = Convert.ToBase64String(hashcode);
        
        return hashed;
    }
}