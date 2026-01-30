

using Microsoft.AspNetCore.Identity;

namespace Tamplate.Domain.Models
{
    public class User:IdentityUser
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
