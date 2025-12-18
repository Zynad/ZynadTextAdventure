namespace ApplicationServices.Admin.Models;

public record AdminUserDto(Guid Id, string Username, string? Password, IReadOnlyCollection<string> SessionTokens);
