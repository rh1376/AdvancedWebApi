using CleanArchitecture.Domain.Common;
using Microsoft.AspNetCore.Identity;

namespace CleanArchitecture.Infrastructure.Identity;

public class ApplicationUser : IdentityUser<int>, IEntity<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
