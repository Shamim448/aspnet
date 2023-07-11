
using System.Security.Claims;

namespace Crud.Infrastructure.Securities
{
    public interface ITokenService
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}