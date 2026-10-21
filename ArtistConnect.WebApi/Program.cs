using ArtistConnect.Service;
using ArtistConnect.Service.Interfaces;
using ArtistConnect.WebApi.Endpoints;
using ArtistConnect.WebApi.Infrastructure;
using ArtistConnect.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ApiExceptionHandler>();
builder.Services.AddArtistConnectDatabase(builder.Configuration);
builder.Services.AddArtistConnectServices();
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Jwt:Authority"];
    });
builder.Services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
    .Configure<ITokenValidationService>((options, tokenValidationService) =>
        options.TokenValidationParameters = tokenValidationService.CreateValidationParameters());
builder.Services.AddAuthorization();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapArtistaEndpoints();
app.MapMusicaEndpoints();
app.MapAuthEndpoints(app.Environment.IsDevelopment());

app.Run();

public partial class Program;
