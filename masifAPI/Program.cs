using Azure.Identity;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using masifAPI.Data;
using Microsoft.Extensions.FileProviders;
using masifAPI.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.FileProviders;



var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAntiforgery();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDirectoryBrowser();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
      policy =>
      {
          policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin();
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

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection));

  
  

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<ApplicationDbContext>();
var app = builder.Build();

// Configure the HTTP request pipeline.  
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapIdentityApi<IdentityUser>();





app.UseAntiforgery();


/* app.MapPost("/upload", async (IFormFile file) =>

{
    var fileName = Path.GetFileName(file.FileName);
    
    
    
    app.Logger.LogInformation(fileName);
    using var stream = File.OpenWrite(fileName);
    
    await file.CopyToAsync(stream);
    


    
}).DisableAntiforgery(); */
// Create a BlobServiceClient that will authenticate through Active Directory
Uri accountUri = new Uri("https://MYSTORAGEACCOUNT.blob.core.windows.net/");
BlobServiceClient client = new BlobServiceClient(accountUri, new DefaultAzureCredential());

app.MapControllers();
app.UseRouting(); 

app.UseCors(builder => builder
       .AllowAnyHeader()
       .AllowAnyMethod()
       .AllowAnyOrigin()
    );
app.UseRouting(); 
  
app.UseStaticFiles(); 

app.UseAuthorization();

app.UseHttpsRedirection();



app.Run();


public class LogActionFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var request = context.HttpContext.Request;
        Console.WriteLine($"Request method: {request.Method}");
        base.OnActionExecuting(context);
    }
};