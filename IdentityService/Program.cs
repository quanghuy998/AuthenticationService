using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using IdentityService.Application.Extensions;
using IdentityService.Infrastructure;
using IdentityService.Authorization;
using IdentityService.Application;
using IdentityService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

IConfiguration configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddApplicationServiceRegistration();
builder.Services.AddInfrastructureServiceRegistration(builder.Configuration);


if (configuration.IsEnableFeatureFlag("DisableAuthenticationInDevelopment"))
    builder.Services.AddSingleton<IPolicyEvaluator, DisableAuthenticationPolicyEvaluator>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
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
    })
    .AddCookie();


builder.Services.AddAuthorization((Action<AuthorizationOptions>)(options =>
{
    foreach(var policy in Policy.Policies)
    {
        options.AddPolicy(policy, new AuthorizationPolicy(
            new[] { new DemandRequirement(policy) },
            new[] { JwtBearerDefaults.AuthenticationScheme }));
    }
}));


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "CorsPolicy", policy =>
    {
        policy.WithOrigins(configuration.GetSection("AllowedOrigins").Get<string[]>())
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
        });
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
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
