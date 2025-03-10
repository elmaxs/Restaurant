using Microsoft.AspNetCore.Identity;

namespace Restaurant.Data.IdentityUpdate
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole() { }
        public ApplicationRole(string roleName) : base() { Name = roleName; }
    }
}
