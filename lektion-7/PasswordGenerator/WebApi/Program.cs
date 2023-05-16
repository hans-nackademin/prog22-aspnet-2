using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Helpers.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// contexts
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));

// authentication

// repositories

// services
builder.Services.AddTransient<PasswordGenerator>();
builder.Services.AddScoped<UserManager>();


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseAuthentication();    // vem är du? inloggning
app.UseAuthorization();     // vad får du göra? authorize/roller

app.MapControllers();
app.Run();
