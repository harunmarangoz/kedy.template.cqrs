using Application.Common;

namespace Application.Services;

public interface IWorkContext
{
    string Id { get; }
    string FirstName { get; }
    string LastName { get; }
    string FullName { get; }
    string Email { get; }
}