using Microsoft.AspNetCore.Identity;

namespace HaulageSystem.Application.Models.Identity;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool EmailConfirmed { get; set; }
    public string SecurityStamp { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTime? LockoutEndDateUtc { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
    public string NormalizedUserName { get; set; }
    public string NormalizedEmail { get; set; }
    public string ConcurrencyStamp { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
}