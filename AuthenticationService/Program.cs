using AuthenticationService;
using AuthenticationService.Application;
using AuthenticationService.Application.Extensions;
using AuthenticationService.Authorization;
using AuthenticationService.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddApplicationServiceRegistration();
builder.Services.AddInfrastructureServiceRegistration(builder.Configuration);

if (configuration.IsEnableFeatureFlag("DisableAuthenticationAtProduction"))
{
    builder.Services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();
};

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var a = configuration.GetAuthenticationConfig("");
        var audienceSecret = configuration.GetAuthenticationConfig("AdminAudienceSecret");
        var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceSecret));

        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = issuerSigningKey,

            ValidateAudience = true,
            ValidAudience = configuration.GetAuthenticationConfig("AdminAudienceId"),

            ValidateIssuer = true,
            ValidIssuer = configuration.GetAuthenticationConfig("Issuer")
        };
    });

builder.Services.AddAuthorization(options =>
{
    foreach(var policy in Policy.Policies)
    {
        options.AddPolicy(policy, new AuthorizationPolicy(
            new[] { new DemandRequirement(policy) },
            new[] { JwtBearerDefaults.AuthenticationScheme }));
    }
});

builder.Services.AddTransient<IAuthorizationHandler, DemandRequirementHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
