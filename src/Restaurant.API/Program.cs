using DotNetEnv;
using Microsoft.OpenApi;
using Restaurant.Application;
using Restaurant.Infrastructure;
using Restaurant.Persistence;
using System.Text.Json.Serialization;

Env.Load();

var searchDir = Directory.GetCurrentDirectory();
while (searchDir is not null)
{
    var envPath = Path.Combine(searchDir, ".env");
    if (File.Exists(envPath)) { Env.Load(envPath); break; }
    searchDir = Directory.GetParent(searchDir)?.FullName;
}

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddHttpClient();
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.Converters.Add(
        new JsonStringEnumConverter());
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Restaurant API",
        Version = "v1"
    });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Nhập JWT token."
    });

    c.AddSecurityRequirement(document =>
    new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = []
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    await scope.ServiceProvider.InitialiseDatabaseAsync();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurant API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/api/routes", (IEnumerable<EndpointDataSource> endpointSources) =>
{
    var endpoints = endpointSources.SelectMany(es => es.Endpoints);
    return endpoints.OfType<RouteEndpoint>().Select(e => new
    {
        Method = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods.FirstOrDefault(),
        Route = e.RoutePattern.RawText
    });
});

app.MapControllers();

app.Run();
