namespace Searchify.Domain.Model;

public record AuthResponse(string AccessToken, DateTime ExpiresAtUtc, string TokenType = "Bearer");
