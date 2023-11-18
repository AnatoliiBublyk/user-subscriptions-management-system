using System;
using System.Security.Cryptography;
using System.Text;
using UserSubscriptionManagement.Infrastructure.Services.Interfaces;

namespace UserSubscriptionManagement.Infrastructure.Services;

/// <summary>
///     This services generates hashes for password and match hashes against open passwords
/// </summary>
public class Sha256Service : IHashService
{
    public bool VerifyHash(string input, string hash)
    {
        string inputHash = GetHash(input);
        return StringComparer.OrdinalIgnoreCase.Compare(inputHash, hash) == 0;
    }

    public string GetHash(string input)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        byte[] hashBytes = sha256.ComputeHash(inputBytes);

        StringBuilder sb = new StringBuilder();
        foreach (byte b in hashBytes)
        {
            sb.Append(b.ToString("x2")); // Convert each byte to a hexadecimal string
        }

        return sb.ToString();
    }
}