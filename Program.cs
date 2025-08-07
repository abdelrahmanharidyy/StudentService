using StudentDAL;
using StudentDAL.Interfaces;
using AspNetCoreRateLimit;
using Microsoft.Extensions.DependencyInjection;
var builder = WebApplication.CreateBuilder(args);

// Read connection string
string connStr = builder.Configuration.GetConnectionString("MyConnection");

// Register repositories with interfaces (better for abstraction and testing)
builder.Services.AddScoped<ICollegeRepository>(provider =>
    new CollegeRepository(connStr));

builder.Services.AddScoped<IStudentRepository>(provider =>
    new StudentRepository(connStr));
//Rate limit prevent "DDos Attack":

builder.Services.AddMemoryCache();

builder.Services.Configure<IpRateLimitOptions>(
    builder.Configuration.GetSection("IpRateLimiting"));



builder.Services.AddMemoryCache();
builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();

// Add controllers and Swagger
builder.Services.AddControllers().AddXmlSerializerFormatters();// Return xml

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
app.UseIpRateLimiting();
