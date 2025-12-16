using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Security.Claims;

namespace FriendOfAwardBewertung
{

    public class MyCustomAuthStateProvider : AuthenticationStateProvider
    {
        private ClaimsPrincipal _anonymous = new(new ClaimsIdentity());
        // _anonymous - falls _currentUser null ist
        private ClaimsPrincipal? _currentUser = null;


        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser ?? _anonymous));
        }
        //public void LoginAdmin(string email)
        //{
        //    Console.WriteLine("############### LOGIN ADMIN ###############");
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, email),
        //        new Claim(ClaimTypes.Role, "Admin")
        //    };

        //    ClaimsIdentity identity = new ClaimsIdentity(claims, "AdminAuth");
        //    _currentUser = new ClaimsPrincipal(identity);


        //    Console.WriteLine("LoginUser: Claims:");
        //    foreach (Claim? c in _currentUser.Claims)
        //    {
        //        Console.WriteLine($"{c.Type} = {c.Value}");
        //    }
        //    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        //}


        //public void LoginUser(string token)
        //{
        //    Console.WriteLine("############### LOGIN USER ###############");
        //    List<Claim> claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, token),
        //        new Claim(ClaimTypes.Role, "User")
        //    };

        //    ClaimsIdentity identity = new ClaimsIdentity(claims, "UserAuth");
        //    _currentUser = new ClaimsPrincipal(identity);

        //    Console.WriteLine("LoginUser: Claims:");
        //    foreach (Claim? c in _currentUser.Claims)
        //    {
        //        Console.WriteLine($"{c.Type} = {c.Value}");
        //    }

        //    NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        //}



        public void Login(string username)
        {
            ClaimsIdentity identity = new([new Claim(ClaimTypes.Name, username)],
                    "MyCustomAuthType");  // von SPAA erfunden, keiner der Standardtypen

            _currentUser = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }


        public void Logout()
        {
            _currentUser = null;
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }
}
