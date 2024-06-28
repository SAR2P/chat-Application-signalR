using ChatModelsLibrary.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace ChatApp_SingleR.Authentication
{
    public class PersistinServerAuthenticationStateProvider :
        ServerAuthenticationStateProvider, IDisposable
    {

        private readonly PersistentComponentState _state;
        private readonly IdentityOptions _optiions;
        private readonly PersistingComponentStateSubscription _subscription;
        private Task<AuthenticationState>? _authenticationStateTask;



        public PersistinServerAuthenticationStateProvider(
            PersistentComponentState state, IOptions<IdentityOptions> options
            )
        {
            _state = state;
            _optiions = options.Value;
            AuthenticationStateChanged += OnAthenticationStateChanged;
            _subscription = state.RegisterOnPersisting(OnPersistingAsync, RenderMode.InteractiveWebAssembly);
        }

        private async Task OnPersistingAsync()
        {
            var authenticationState = await _authenticationStateTask;
            var principal = authenticationState.User;
            if (principal.Identity?.IsAuthenticated == true)
            {
                var userId = principal.FindFirst(_optiions.ClaimsIdentity.UserIdClaimType)?.Value;
                var email = principal.FindFirst(_optiions.ClaimsIdentity.EmailClaimType)?.Value;
                var fullname = principal.Claims.Where(f => f.Type == ClaimTypes.Name).Last().Value;
                if (userId != null && email != null && fullname != null)
                {
                    _state.PersistAsJson(nameof(UserInfo), new UserInfo
                    {
                        Id = userId,
                        FullName = fullname,
                        Email = email
                    });
                }
            }

        }

        private void OnAthenticationStateChanged(Task<AuthenticationState> task)
          => _authenticationStateTask = task;


        public void Dispose()
        {
            _subscription.Dispose();
            AuthenticationStateChanged -= OnAthenticationStateChanged;
        }
    }
}
