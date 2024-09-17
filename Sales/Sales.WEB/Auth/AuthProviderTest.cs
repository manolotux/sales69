using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace Sales.WEB.Auth
{
	public class AuthProviderTest : AuthenticationStateProvider
	{
		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var anonimous = new ClaimsIdentity();
			return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));
		}
	}
}
