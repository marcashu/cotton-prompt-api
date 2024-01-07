using Azure.Storage.Blobs;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Services.DesignBrackets;
using CottonPrompt.Infrastructure.Services.Designs;
using CottonPrompt.Infrastructure.Services.Orders;
using CottonPrompt.Infrastructure.Services.OutputSizes;
using CottonPrompt.Infrastructure.Services.PrintColors;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt =>
{
    opt.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
builder.Services.AddDbContext<CottonPromptContext>(
        options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetSection("Storage")["ConnectionString"];
    var client = new BlobServiceClient(connectionString);
    return client;
});
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDesignBracketService, DesignBracketService>();
builder.Services.AddScoped<IDesignService, DesignService>();
builder.Services.AddScoped<IPrintColorService, PrintColorService>();
builder.Services.AddScoped<IOutputSizeService, OutputSizeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
