using Microsoft.EntityFrameworkCore;
using databasepmapilearn6.models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using databasepmapilearn6.Configurations;

var builder = WebApplication.CreateBuilder(args);

// add jwt env
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtAudiance = builder.Configuration.GetSection("Jwt:Audience").Get<string>();

// Add services to the container.
builder.Services.Configure<ConfJwt>(builder.Configuration.GetSection("Jwt")); // configure ConfJwt to the main program (valuenya belum disimpan di class constan)
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DatabasePmContext>(options =>
{
    string connectionString = builder.Configuration.GetConnectionString("Databaseapilearn6");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

// add JWT service
// ntar cari tau cara bacanya
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer( // default
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudiance,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }
);
// end add JWT service

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder
        .AllowAnyOrigin()
        .WithOrigins(
            "http://172.168.18.8",
            "http://localhost:3000")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); // add this for using JWT
app.UseAuthorization();
app.UseCors("CorsPolicy"); // add CORS

app.MapControllers();

app.Run();
