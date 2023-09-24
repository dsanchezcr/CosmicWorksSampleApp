using CosmicWorksSampleApp.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddAntiforgery();
builder.Services.Configure<RouteOptions>(o =>
{
    o.LowercaseUrls = true;
    o.LowercaseQueryStrings = true;
});
builder.Services.AddSingleton<ICosmosService, CosmosService>();
builder.Services.AddSingleton<ISearchService, SearchService>();
builder.Services.AddSingleton<IChatService, ChatService>();
builder.Services.AddApplicationInsightsTelemetry(builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]);
var app = builder.Build();
app.UseRouting();
app.MapRazorPages();
app.UseStaticFiles();
app.Run();