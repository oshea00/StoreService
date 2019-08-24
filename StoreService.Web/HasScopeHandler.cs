using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StoreService.Web
{
    public class HasScopeHandler : AuthorizationHandler<HasScopeRequirement>
    {
        private ILogger<HasScopeHandler> logger;

        public HasScopeHandler(ILogger<HasScopeHandler> logger)
        {
            this.logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasScopeRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            if (context.User.HasClaim(c => c.Type == "permissions" && c.Value == requirement.Scope))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
