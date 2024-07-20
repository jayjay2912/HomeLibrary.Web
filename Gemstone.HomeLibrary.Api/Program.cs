using System.Text.Json.Serialization;
using Gemstone.HomeLibrary.Api.DbContext;
using Gemstone.HomeLibrary.Api.Services.HomeLibraryService;
using Gemstone.HomeLibrary.Api.Services.OpenLibraryService;
using Microsoft.EntityFrameworkCore;

#region Builder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

builder.Services.AddHttpClient();

// wide open CORS policy
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
{
    policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
}));

builder.Services.AddScoped<IHomeLibraryService, HomeLibraryService>();
builder.Services.AddScoped<IOpenLibraryService, OpenLibraryService>();

builder.Services.AddDbContext<LibraryDbContext>();

#endregion

#region App

var app = builder.Build();

app.UseCors();

app.MapHealthChecks("/_health");

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<LibraryDbContext>();
    context.Database.Migrate();
}

app.Run();

#endregion
