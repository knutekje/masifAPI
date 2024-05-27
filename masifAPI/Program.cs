using Azure.Identity;
using masifAPI.Data;
using masifAPI.Model;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddAntiforgery();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var credential = new DefaultAzureCredential();
var connection = String.Empty;
if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddEnvironmentVariables().AddJsonFile("appsettings.Development.json");
    connection = builder.Configuration.GetConnectionString("AZURE_SQL_CONNECTIONSTRING");
}
else
{
    connection = Environment.GetEnvironmentVariable("AZURE_SQL_CONNECTIONSTRING");
}


builder.Services.AddDbContext<IncidentContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddDbContext<FoodItemContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddDbContext<ReportContext>(options =>
    options.UseSqlServer(connection));


var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAntiforgery();


app.MapPost("/upload", async (IFormFile file) =>
{
    var tempFile = Path.GetFileName(file.FileName);
     
    
    app.Logger.LogInformation(tempFile);
    using var stream = File.OpenWrite(tempFile);
    
    await file.CopyToAsync(stream);
    
}).DisableAntiforgery();

app.MapControllers();



app.UseHttpsRedirection();



app.Run();

