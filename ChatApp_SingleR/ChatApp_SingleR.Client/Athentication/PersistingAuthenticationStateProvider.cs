using ChatApp_SingleR.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace ChatApp_SingleR.Client.Athentication
{
    public class PersistingAuthenticationStateProvider : AuthenticationStateProvider
    {

        private static readonly Task<AuthenticationState> DefaulAuthenticatedTask =
            Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));

        private readonly Task<AuthenticationState> authenticationOnStateTask = DefaulAuthenticatedTask;

        public PersistingAuthenticationStateProvider(PersistentComponentState state)
        {
            if (!state.TryTakeFromJson<UserInfo>(nameof(UserInfo), out var userInfo) || userInfo is null)
                return;

            Claim[] claims = [
                    new Claim(ClaimTypes.NameIdentifier, userInfo.Id!),
                    new Claim(ClaimTypes.Email, userInfo.Email!),
                    new Claim(ClaimTypes.Name, userInfo.FullName!)
                ];

            authenticationOnStateTask = Task.FromResult(
                new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims,nameof(PersistingAuthenticationStateProvider)))))!;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
           => authenticationOnStateTask!;



    }
}
