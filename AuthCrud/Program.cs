using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

var connectionString = Environment.GetEnvironmentVariable("DBConnectionStrings");
if (string.IsNullOrEmpty(connectionString))
    throw new Exception("Connection string not found. Ensure the .env file is correctly configured and placed in the root directory.");
var privateKey = Environment.GetEnvironmentVariable("PrivateKey");
if (string.IsNullOrEmpty(privateKey))
    throw new Exception("PrivateKey not found. Ensure the .env file is correctly configured and placed in the root directory.");

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
