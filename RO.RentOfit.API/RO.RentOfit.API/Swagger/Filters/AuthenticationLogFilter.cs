
namespace RO.RentOfit.API.Swagger.Filters
{
    class AuthenticationLogFilter :Attribute, IAuthorizationFilter
    {      
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var token = context.HttpContext.Request.Headers["AuthToken"];
            if (String.IsNullOrEmpty(token))
            {
                context.Result = new StatusCodeResult(StatusCodes.Status401Unauthorized);
            }
           
        }
    }
}
