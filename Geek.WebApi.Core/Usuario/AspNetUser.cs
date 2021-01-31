using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Geek.WebApi.Core.Usuario
{
    public class AspNetUser : IAspNetUser
    {
        private readonly IHttpContextAccessor _accessor;
        public AspNetUser(IHttpContextAccessor acessor)
        {
            _accessor = acessor;
        }

        public string Name => _accessor.HttpContext.User.Identity.Name;
        public Guid ObterUserId()
        {
            var guid = EstaAutenticado() ? Guid.Parse(_accessor.HttpContext.User.GetUserId()) : Guid.Empty;
            return guid;
        }

        public string ObterUserEmail()
        {
            var email = EstaAutenticado() ? _accessor.HttpContext.User.GetUserEmail() : "";
            return email;
        }

        public string ObterUserToken()
        {
            var token = EstaAutenticado() ? _accessor.HttpContext.User.GetUserToken() : "";
            return token;
        }

        public bool EstaAutenticado()
        {
            return _accessor.HttpContext.User.Identity.IsAuthenticated;
        }

        public bool PossuiRole(string role)
        {
            return _accessor.HttpContext.User.IsInRole(role);
        }

        public IEnumerable<Claim> ObterClaims()
        {
            return _accessor.HttpContext.User.Claims;
        }

        public HttpContext ObterHttpContext()
        {
            return _accessor.HttpContext;
        }

        public string ObterUserRefreshToken()
        {
            return EstaAutenticado() ? _accessor.HttpContext.User.GetUserRefreshToken() : "";
        }
    }

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
            return claim?.Value;
        }

        public static string GetUserEmail(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst(ClaimTypes.Email);
            return claim?.Value;
        }
        public static string GetUserToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }
            var claim = principal.FindFirst("JWT");
            return claim?.Value;
        }
        public static string GetUserRefreshToken(this ClaimsPrincipal principal)
        {
            if (principal == null)
            {
                throw new ArgumentException(nameof(principal));
            }

            var claim = principal.FindFirst("RefreshToken");
            return claim?.Value;
        }
    }
}

