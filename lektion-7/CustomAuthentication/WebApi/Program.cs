using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WebApi.Contexts;
using WebApi.Helpers.Repositories;
using WebApi.Helpers.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// contexts
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Sql")));

// repositories
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<UserHashRepository>();

// services 
builder.Services.AddTransient<PasswordGenerator>();
builder.Services.AddTransient<TokenGenerator>();
builder.Services.AddScoped<UserManager>();

// authentication
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddCookie()
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = true;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Token:Issuer"]!,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Token:Audience"]!,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Secret"]!))
    };
    x.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            if (string.IsNullOrEmpty(context.Principal?.Identity?.Name))
                context.Fail("Unauthorized");

            return Task.CompletedTask;
        }
    };
});



var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
