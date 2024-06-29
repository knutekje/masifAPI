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

var connection = "Server=192.168.100.103;Database=masifAPI;Port=5432;User Id=knutekje;Password=hore23;Ssl Mode=Require";




/* 
builder.Services.AddDbContext<IncidentContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddDbContext<FoodItemContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddDbContext<ReportContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddDbContext<PictureContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connection)); */

builder.Services.AddDbContext<IncidentContext>(options =>
          options.UseNpgsql(connection));
          
builder.Services.AddDbContext<FoodItemContext>(options =>
          options.UseNpgsql(connection));

builder.Services.AddDbContext<ReportContext>(options =>
          options.UseNpgsql(connection));

builder.Services.AddDbContext<PictureContext>(options =>
          options.UseNpgsql(connection));

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