using Microsoft.OpenApi.Models;

// Initiate a web app builder
var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = Configuration.GetSection("AllowedOrigins").Value.Split(';');

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Todo API", Description = "All tasks", Version = "v1" });
    });
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.WithOrigins(allowedOrigins)
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Initialize the app
var app = builder.Build();

// Configure the HTTP request pipeline
app.UseSwagger();
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1");
    });
app.UseCors("CorsPolicy");

app.MapGet("/", () => Results.Json(new { Message = "Hello World!" }));

app.Run();