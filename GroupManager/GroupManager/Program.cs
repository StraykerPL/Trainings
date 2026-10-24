using GroupManager.Data.Services;
using GroupManager.Data.Services.Interfaces;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();

builder.Services.AddScoped<IDataProvider, DefaultDataProvider>();
builder.Services.AddScoped<ILoggingManager, DefaultLoggingManager>();

var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();