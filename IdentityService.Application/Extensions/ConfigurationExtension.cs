using Microsoft.Extensions.Configuration;

namespace IdentityService.Application.Extensions
{
    public static class ConfigurationExtension
    {
        public static string GetAuthenticationConfig(this IConfiguration configuration, string key)
        {
            return configuration.GetSection("Authentication")[key] ?? null;
        }

        public static bool IsEnableFeatureFlag(this IConfiguration configuration, string featureflag) 
        {
            return bool.Parse(configuration.GetSection("FeatureFlags")[featureflag]);
        }
    }
}
