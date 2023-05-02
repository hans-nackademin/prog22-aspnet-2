using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Helpers.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Azure Key Vault
builder.Configuration.AddAzureKeyVault(new Uri(builder.Configuration["VaultURI"]!.ToString()), new DefaultAzureCredential());

// Contexts
builder.Services.AddDbContext<DataContext>(x => x.UseCosmos(builder.Configuration["CosmosDB"]!, "PROG22"));

// Repositories
builder.Services.AddScoped<ProductRepository>();

var app = builder.Build();
app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
