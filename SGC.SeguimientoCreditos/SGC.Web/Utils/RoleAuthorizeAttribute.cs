using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SGC.DAL.Entidades;

namespace SGC.Web.Utils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true)]
    public class RoleAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly Rol[] _roles;
        public RoleAuthorizeAttribute(params Rol[] roles) { _roles = roles; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var userRol = context.HttpContext.Session.GetString("Rol");
            if (string.IsNullOrEmpty(userRol))
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }
            if (_roles.Any() && !_roles.Select(r => r.ToString()).Contains(userRol))
                context.Result = new ForbidResult();
        }
    }
}
