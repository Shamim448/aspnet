using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace Crud.Infrastructure.Securities
{
    public class UserViewRequirementHandler :
          AuthorizationHandler<UserViewRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               UserViewRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "ViewUser" && x.Value == "true"))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
