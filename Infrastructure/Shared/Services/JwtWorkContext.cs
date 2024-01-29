using System.Security.Claims;
using System.Text.Json;
using Application.Common;
using Application.Services;
using Microsoft.AspNetCore.Http;

namespace Shared.Services;

public class JwtWorkContext(IHttpContextAccessor httpContextAccessor) : IWorkContext
{
    public string Id => httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "";
    public string FirstName => httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value ?? "";
    public string LastName => httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Surname)?.Value ?? "";
    public string FullName => $"{FirstName} {LastName}";
    public string Email => httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value ?? "";
}