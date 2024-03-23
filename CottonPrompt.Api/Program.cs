using Azure.Identity;
using Azure.Storage.Blobs;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Services.Artists;
using CottonPrompt.Infrastructure.Services.DesignBrackets;
using CottonPrompt.Infrastructure.Services.Designs;
using CottonPrompt.Infrastructure.Services.Invoices;
using CottonPrompt.Infrastructure.Services.Orders;
using CottonPrompt.Infrastructure.Services.OutputSizes;
using CottonPrompt.Infrastructure.Services.PrintColors;
using CottonPrompt.Infrastructure.Services.UserGroups;
using CottonPrompt.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph;
using Microsoft.Identity.Web;

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
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

// Inject Blob Service Client
builder.Services.AddScoped(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetSection("Storage")["ConnectionString"];
    var client = new BlobServiceClient(connectionString);
    return client;
});

// Inject MS Graph Client
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped(sp =>
{
    var httpContextAccessor = sp.GetService<IHttpContextAccessor>() ?? throw new Exception("Error injecting GraphServiceClient: httpContextAccessor is null");
    var httpContext = httpContextAccessor.HttpContext ?? throw new Exception("Error injecting GraphServiceClient: httpContext is null");
    var token = httpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
    var config = sp.GetRequiredService<IConfiguration>();
    var azureAdSection = config.GetSection("AzureAd");
    var scopes = new[] { "https://graph.microsoft.com/.default" };
    var tenantId = azureAdSection["TenantId"];
    var clientId = azureAdSection["ClientId"];
    var clientSecret = azureAdSection["ClientSecret"];
    var options = new OnBehalfOfCredentialOptions
    {
        AuthorityHost = AzureAuthorityHosts.AzurePublicCloud,
    };
    var onBehalfOfCredential = new OnBehalfOfCredential(
        tenantId, clientId, clientSecret, token, options);
    var graphClient = new GraphServiceClient(onBehalfOfCredential, scopes);
    return graphClient;
});

builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IDesignBracketService, DesignBracketService>();
builder.Services.AddScoped<IDesignService, DesignService>();
builder.Services.AddScoped<IPrintColorService, PrintColorService>();
builder.Services.AddScoped<IOutputSizeService, OutputSizeService>();
builder.Services.AddScoped<IArtistService, ArtistService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserGroupService, UserGroupService>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
