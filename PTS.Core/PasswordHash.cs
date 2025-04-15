namespace PTS.Core;

using BCrypt.Net;


public static class PasswordHash
{

    public static string Encrypt(string plaintext, int version) {
        return BCrypt.HashPassword(plaintext);
    }

    public static bool Verify(string plaintext, string hash, int version) {
        return BCrypt.Verify(plaintext, hash);
    }
}
