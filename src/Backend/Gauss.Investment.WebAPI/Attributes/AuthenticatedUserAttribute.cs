using Gauss.Investment.WebAPI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Gauss.Investment.WebAPI.Attributes
{
    public class AuthenticatedUserAttribute : TypeFilterAttribute
    {
        public AuthenticatedUserAttribute() : base(typeof(AuthenticatedUserFilter))
        {
        }
    }
}
