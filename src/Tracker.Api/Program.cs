using System.Text.Json.Serialization;
using Tracker.Api.IoC;
using Tracker.Api.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        _ = policy.AllowAnyOrigin();
    });
});

builder.Services
    .AddControllers(options =>
    {
        options.UseGeneralRoutePrefix("api");
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();
app.UseAuthorization();

app.MapControllers();

app.Run();
