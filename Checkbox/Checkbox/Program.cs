using Checkbox.Client.Pages;
using Checkbox.Components;
using Microsoft.AspNetCore.ResponseCompression;
using Checkbox.Hubs;
using Checkbox;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddSignalR();
builder.Services.AddHttpClient();

builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        ["application/octet-stream"]);
});

builder.Services.Configure<CheckboxSizeOption>(
    builder.Configuration.GetSection(CheckboxSizeOption.CheckboxSize));

builder.Services.AddSingleton<CheckboxService>();

var app = builder.Build();

app.UseResponseCompression();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Checkbox.Client._Imports).Assembly);

app.MapHub<CheckboxHub>("/checkboxhub");

app.MapGet("/state", () => app.Services.GetRequiredService<CheckboxService>().checkboxes);

app.Run();