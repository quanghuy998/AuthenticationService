using Microsoft.AspNetCore.Authorization;

namespace IdentityService.Authorization
{
    public class DemandRequirement : IAuthorizationRequirement
    {
        public string Demand { get; }

        public DemandRequirement(string demand)
        {
            this.Demand = demand;
        }
    }

    public class DemandRequirementHandler : AuthorizationHandler<DemandRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DemandRequirement requirement)
        {
            var user = context.User;

            var matching = user.Claims.First(_ => _.Type == "claims" && _.Value == requirement.Demand);

            if (matching == null)
                return Task.CompletedTask;

            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
