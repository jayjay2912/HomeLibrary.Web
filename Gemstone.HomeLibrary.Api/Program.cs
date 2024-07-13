using System.Text.Json.Serialization;
using Gemstone.HomeLibrary.Api.DbContext;
using Gemstone.HomeLibrary.Api.Services.HomeLibraryService;
using Gemstone.HomeLibrary.Api.Services.OpenLibraryService;

#region Builder

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
;

builder.Services.AddHttpClient();

builder.Services.AddScoped<IHomeLibraryService, HomeLibraryService>();
builder.Services.AddScoped<IOpenLibraryService, OpenLibraryService>();

builder.Services.AddDbContext<LibraryDbContext>();

#endregion

#region App

var app = builder.Build();

app.MapHealthChecks("/_health");

app.MapControllers();

app.Run();

#endregion
