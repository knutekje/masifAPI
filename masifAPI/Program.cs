using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using masifAPI.Data;
using masifAPI.Model;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAntiforgery();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
      policy =>
      {
          policy
          .WithOrigins("http://localhost:3000", "https://localhost:3000")
          .AllowAnyHeader()
          .AllowAnyMethod();
      });
});




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

builder.Services.AddDbContext<PictureContext>(options =>
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
    var fileName = Path.GetFileName(file.FileName);
    
    
    
    app.Logger.LogInformation(fileName);
    using var stream = File.OpenWrite(fileName);
    
    await file.CopyToAsync(stream);
    


    
}).DisableAntiforgery();

app.MapControllers();


app.UseRouting(); // i am not sure where this needs to be, since you are using a JS client. it might have to go after Cors middleware. Please edit this if you find out how where this line needs to go. for systems without JS clients, it goes before Cors middleware.

app.UseCors(); // you dont have to specify a policy name since you configured a default policy.

app.UseStaticFiles(); // this needs to go after cors middleware since you are using a JS client. this is confirmed at microsoft docs.

app.UseAuthorization();

app.UseHttpsRedirection();



app.Run();

