using Microsoft.EntityFrameworkCore;
using VineforceAPIDemoTest.Data;
using VineforceAPIDemoTest.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add DbContext with SQL Server

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();

// Add controllers
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

builder.Services.AddCors();
// Configure method in Program.cs
app.UseCors(builder =>
{
    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
