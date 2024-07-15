namespace AdminAndUserRoleAuthentication.WebApi.DTOs;

public record UserSession(string? Id, string? FullName, string? Email, string? Role);
