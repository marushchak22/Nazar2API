using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using shoesAPI.Clients;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();
builder.Services.AddHttpClient();
builder.Services.AddScoped<IDynamoDB_Client, DynamoDB_Client>();

builder.Services.AddSingleton<IAmazonDynamoDB>(sp =>
{
    var credentials = new BasicAWSCredentials("AKIAQOPR2Q55KZP6OHN7", "pxRs7x472OheN1BKX1nODSanD9QM/3NjJnLIpiIM");
    var config = new AmazonDynamoDBConfig()
    {
        RegionEndpoint = Amazon.RegionEndpoint.USEast1
    };
    return new AmazonDynamoDBClient(credentials, config);
});
builder.Services.AddSingleton<IDynamoDBContext, DynamoDBContext>();
builder.Services.AddSingleton<ShoesClient>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
