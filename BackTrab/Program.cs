using Microsoft.EntityFrameworkCore;
using TrabalhoBackEnd.DAL;
using TrabalhoBackEnd.BLL;
using TrabalhoBackEnd.Security;
using TrabalhoBackEnd.Errors;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TrabalhoBackEnd.ISecurity;  // Certifique-se de que est� importando a interface correta
using TrabalhoBackEnd.IDAL;  // Certifique-se de que est� importando as interfaces corretas

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProjectDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("ConexaoBD"),
        new MySqlServerVersion(new Version(8, 0, 21))
    ));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();  
builder.Services.AddScoped<IDescriptionRepository, DescriptionRepository>();  
builder.Services.AddScoped<IJwtService, JwtService>(sp =>
{
    var configuration = sp.GetRequiredService<IConfiguration>();
    var secret = configuration["Jwt:Secret"];
    var issuer = configuration["Jwt:Issuer"];
    var audience = configuration["Jwt:Audience"];
    var expiration = int.Parse(configuration["Jwt:ExpirationMinutes"]);
    return new JwtService(secret, issuer, audience, expiration);
});

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<DescriptionService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
    };
});

builder.Services.AddAuthorization();
builder.Services.AddControllers();

var app = builder.Build();

app.Use(async (context, next) =>
{
    try
    {
        await next.Invoke();
    }
    catch (BadRequestException ex)
    {
        context.Response.StatusCode = StatusCodes.Status400BadRequest;
        await context.Response.WriteAsync(ex.Message);
    }
    catch (NotFoundException ex)
    {
        context.Response.StatusCode = StatusCodes.Status404NotFound;
        await context.Response.WriteAsync(ex.Message);
    }
    catch (Exception ex)
    {
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        await context.Response.WriteAsync("An unexpected error occurred.");
    }
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
    });
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
