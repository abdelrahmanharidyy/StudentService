using StudentDAL;
using StudentDAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Read connection string
string connStr = builder.Configuration.GetConnectionString("MyConnection");

// Register repositories with interfaces (better for abstraction and testing)
builder.Services.AddScoped<ICollegeRepository>(provider =>
    new CollegeRepository(connStr));

builder.Services.AddScoped<IStudentRepository>(provider =>
    new StudentRepository(connStr));


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
