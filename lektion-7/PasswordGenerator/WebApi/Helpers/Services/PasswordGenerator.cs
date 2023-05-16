using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace WebApi.Helpers.Services;

public class PasswordGenerator
{
    private IConfiguration _configuration;

    public PasswordGenerator()
    {
        var config = new ConfigurationBuilder().AddJsonFile("appsettings.json");
        _configuration = config.Build();
    }

    public string HashPassword(string password)
    {
        var prefix = _configuration["SecretKey"]!;
        var salt = new byte[256 / 8];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        string hash = Hasher(password, salt);

        return $"{prefix}{Convert.ToBase64String(salt)}.{hash}";
    }

    public bool ValidatePassword(string password, string hashedPassword) 
    {
        var parts = hashedPassword.Split(".");
        var prefix = parts[0];
        var salt = Convert.FromBase64String(parts[1]);
        var hashed = parts[2];

        return hashed == Hasher(password, salt);
    }

    private string Hasher(string password, byte[] salt)
    {
        string hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            salt: salt,
            password: password,
            prf: KeyDerivationPrf.HMACSHA512,
            iterationCount: 10000,
            numBytesRequested: 512 / 8
        ));

        return hash;
    }
}
