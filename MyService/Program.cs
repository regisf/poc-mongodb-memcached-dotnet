using Enyim.Caching.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MyService.Models;
using MyService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()

    // Mem cached
    .AddEnyimMemcached(o => { o.Servers = new List<Server> {new() {Address = "cache", Port = 11211}}; })

    // Database
    .AddScoped<IHelloService, HelloService>()
    .AddScoped<IMongoDatabase>(sp =>
    {
        var dbSettings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
        return new MongoClient(dbSettings.ConnectionString).GetDatabase(dbSettings.DatabaseName);
    })
    .Configure<DatabaseSettings>(builder.Configuration.GetSection("HelloDatabase"))
    .AddSingleton<IDatabaseSettings>(sp =>
        sp.GetRequiredService<IOptions<DatabaseSettings>>().Value
    )
    .Configure<MemcachedSettings>(builder.Configuration.GetSection("Memcached"))
    .AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    builder.WebHost.UseUrls("http://+:5000");
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();