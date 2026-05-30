using FleetEdge.Identity.Domain.Common;
using FleetEdge.Identity.Domain.Enums;

namespace FleetEdge.Identity.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<RefreshToken> RefreshTokens { get; set; }
        = new List<RefreshToken>();
}