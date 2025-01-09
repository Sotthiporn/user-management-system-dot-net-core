using Microsoft.AspNetCore.Authorization;
using user_management_dot_net_core.Exceptions;

namespace user_management_dot_net_core.Middlewares
{
    public class PermissionMiddleware
    {
        private readonly RequestDelegate _next;

        public PermissionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var endpoint = context.GetEndpoint();

            var requiresAuthorization = endpoint?.Metadata
                .GetMetadata<AuthorizeAttribute>() != null;

            if (requiresAuthorization)
            {
                var isAuthenticated = context.User.Identity?.IsAuthenticated ?? false;

                if (!isAuthenticated)
                {
                    throw new ForbiddenException("You do not have permission to perform this action");
                }
            }

            await _next(context);
        }
    }
}
