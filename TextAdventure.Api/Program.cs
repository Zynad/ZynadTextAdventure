using System.IO;
using System.Reflection;
using ApplicationServices.Configuration;
using Scalar.AspNetCore;
using Microsoft.OpenApi.Models;
using TextAdventure.Infrastructure.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "TextAdventure API",
        Version = "v1",
        Description = "Endpoints for the JSON-backed text adventure service"
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Send the session token as a Bearer token or rely on the HttpOnly authToken cookie"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        options.IncludeXmlComments(xmlPath);
    }
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5173", "http://localhost:3000")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
    });
});
builder.Services.AddTextAdventureGame(builder.Configuration);
builder.Services.AddTextAdventureInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("FrontendOrigins");

app.UseAuthorization();

var apiDocumentationSection = app.Configuration.GetSection("ApiDocumentation");
var apiDocumentationEnabled = apiDocumentationSection.GetValue<bool?>("Enabled")
    ?? app.Environment.IsDevelopment();
var apiDocumentationRequiresAuthorization = apiDocumentationSection.GetValue<bool?>("RequireAuthorization") ?? false;

if (apiDocumentationEnabled)
{
    var openApiEndpoint = app.MapOpenApi();
    var scalarEndpoint = app.MapScalarApiReference(options =>
    {
        options.Title = "TextAdventure API";
    });

    if (apiDocumentationRequiresAuthorization)
    {
        openApiEndpoint.RequireAuthorization();
        scalarEndpoint.RequireAuthorization();
    }
}

app.MapControllers();

app.Run();

/// <summary>
/// Entry point for the TextAdventure API application.
/// </summary>
public partial class Program;
