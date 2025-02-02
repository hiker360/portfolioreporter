using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

// Load configuration
var configuration = builder.Configuration;

// Get the Excel file path from appsettings.json
string excelFilePath = configuration["ExcelFilePath"];

// Read Excel data at startup
var excelReaderService = new ExcelReaderService(excelFilePath);
var accounts = excelReaderService.ReadAccounts();
var balances = excelReaderService.ReadBalances();

// Register services
builder.Services.AddSingleton(excelReaderService);
builder.Services.AddSingleton(new PortfolioCalculationService(accounts, balances));
builder.Services.AddSingleton<ChartService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
