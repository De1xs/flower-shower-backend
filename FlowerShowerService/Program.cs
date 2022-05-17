using FlowerShowerService.Data;
using FlowerShowerService.Infrastructure;
using FlowerShowerService.Security;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.WithOrigins("http://localhost:8080").AllowAnyMethod().AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DatabaseConnectionString")));

// Register helpers
if (builder.Configuration["PasswordVerification"] is "Strict")
{
    builder.Services.AddScoped<IPasswordHelper, StrictPasswordHelper>();
}
else if (builder.Configuration["PasswordVerification"] is "Relaxed")
{
    builder.Services.AddScoped<IPasswordHelper, RelaxedPasswordHelper>();
}


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

if (builder.Configuration["LoggingUserEndpoint"] is "True")
{
    app.UseOurCustomLogger();
}

app.MapControllers();

app.Run();
