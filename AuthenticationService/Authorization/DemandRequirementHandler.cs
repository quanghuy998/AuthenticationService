using Microsoft.AspNetCore.Authorization;

namespace AuthenticationService.Authorization
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

            var matching = user.Claims.FirstOrDefault(_ => _.Type == "claims" && IsEqual(requirement.Demand, _.Value));

            if (matching == null)
                return Task.CompletedTask;

            context.Succeed(requirement);

            return Task.CompletedTask;
        }

        private bool IsEqual(string requiredDemand, string actualDemand)
        {
            var requiredDemandParts = requiredDemand.Split("_");
            var actualDemandParts = actualDemand.Split("_");
            if (requiredDemandParts.Length != 3 || actualDemandParts.Length != 3)
                return false;

            return requiredDemandParts[0] == actualDemandParts[0] &&
                   ((requiredDemandParts[1] == "*" || requiredDemandParts[1] == actualDemandParts[1]) &&
                    (requiredDemandParts[2] == "*" || requiredDemandParts[2] == actualDemandParts[2]));
        }
    }
}
