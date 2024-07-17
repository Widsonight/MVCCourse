using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WebApp.Factory;
using WebApp.Factory.Interfaces;
using Auth__Classic;
using WebApi.Data;
using IdentityModel.Client;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

//builder.Services.AddAuthorization(options =>
//{
//    //options.AddPolicy("Inventory", p => p.RequireClaim("Position", "Inventory"));
//    //options.AddPolicy("Cashiers", p => p.RequireClaim("Position", "Cashier"));
//});


builder.Services.AddHttpClient <ICategoriesClient,CategoriesClient> (option =>
{
    option.BaseAddress = new Uri($"{builder.Configuration.GetSection("API").GetValue<string>("BaseAddress")}/Categories");
});

builder.Services.AddHttpClient<IProductClient, ProductClient>(option =>
{
    option.BaseAddress = new Uri($"{builder.Configuration.GetSection("API").GetValue<string>("BaseAddress")}/Products");
});

builder.Services.AddHttpClient<ISalesClient, SalesClient>(option =>
{
    option.BaseAddress = new Uri($"{builder.Configuration.GetSection("API").GetValue<string>("BaseAddress")}/Sales");
});

builder.Services.AddHttpClient<ITransactionsClient, TransactionsClient>(option =>
{
    option.BaseAddress = new Uri($"{builder.Configuration.GetSection("API").GetValue<string>("BaseAddress")}/Transactions");
});


builder.Services.AddHttpClient<ITokenClient, TokenFacClient>(option =>
{
    option.BaseAddress = new Uri($"https://localhost:5001");
});


var app = builder.Build();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


