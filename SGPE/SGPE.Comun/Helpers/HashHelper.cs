﻿using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using System.Text;

namespace SGPE.Comun.Helpers;

public class HashHelper
{
    public static HashedPassword Hash(string password)
    {
        byte[] salt = new byte[128 / 8];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // derive a 256-bit subkey (use HMACSHA1 with 10,000 iterations)
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 10000,
            numBytesRequested: 256 / 8));
        return new HashedPassword() { Password = hashed, Salt = Convert.ToBase64String(salt) };
    }

    public static bool CheckHash(string attemptedPassword, string hash, string salt)
    {
        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
             password: attemptedPassword,
             salt: Convert.FromBase64String(salt),
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 10000,
             numBytesRequested: 256 / 8));
        return hash == hashed;
    }

    public static byte[] GetHash(string password, string salt)
    {
        byte[] unhashedBytes = Encoding.Unicode.GetBytes(string.Concat(salt, password));
        using (SHA256 mySHA256 = SHA256.Create())
        {
            byte[] hashedBytes = mySHA256.ComputeHash(unhashedBytes);
            return hashedBytes;
        }
    }
}

public class HashedPassword
{
    public string Password { get; set; } = string.Empty;
    public string Salt { get; set; } = string.Empty;
}

