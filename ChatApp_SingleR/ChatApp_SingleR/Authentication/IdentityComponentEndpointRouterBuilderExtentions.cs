

using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace ChatApp_SingleR.Authentication
{
    public static class IdentityComponentEndpointRouterBuilderExtentions
    {
        public static IEndpointConventionBuilder MapAdditionalIdentityEndpoints(this IEndpointRouteBuilder endPoint)
        {
            var accountGroup = endPoint.MapGroup("/Account");
            accountGroup.MapPost("/Logout", async (ClaimsPrincipal user, SignInManager<AppUser> signInManager) =>
            {
                await signInManager.SignOutAsync();
                return TypedResults.LocalRedirect("/");
            });
            return accountGroup;
        }
        
    }
}
