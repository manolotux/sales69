using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using Sales.WEB.Helpers;

namespace Sales.WEB.Auth
{
	public class AuthProvider(IJSRuntime _js, HttpClient _http) : AuthenticationStateProvider, ILoginService
    {
        private readonly AuthenticationState _anonimous = new(new ClaimsPrincipal(new ClaimsIdentity()));

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _js.GetLocalStorageItem();

            if (token == null)
                return _anonimous;
            
            return BuildAuthenticationState(token);
        }

        private AuthenticationState BuildAuthenticationState(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            var claims = ParseClaimsFromJwt(token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt")));
        }

        private IEnumerable<Claim>? ParseClaimsFromJwt(string token)
        {
            var jwtSecureToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
            return jwtSecureToken.Claims;
        }

        public async Task LoginAsync(string token)
        {
            await _js.SetLocalStorageItem(token);
            var authState = BuildAuthenticationState(token);
            NotifyAuthenticationStateChanged(Task.FromResult(authState));
        }

        public async Task LogoutAsync()
        {
            await _js.RemoveLocalStorageItem();
            _http.DefaultRequestHeaders.Authorization = null;
            NotifyAuthenticationStateChanged(Task.FromResult(_anonimous));
        }
    }
}
