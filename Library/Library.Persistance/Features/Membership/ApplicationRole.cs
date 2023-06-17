using Microsoft.AspNetCore.Identity;
using System;

namespace Library.Persistance.Features.Membership
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        public ApplicationRole()
            : base()
        {
        }

        public ApplicationRole(string roleName)
            : base(roleName)
        {
        }
    }
}
