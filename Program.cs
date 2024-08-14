using System.Text;
using System.Text.Json.Serialization;
using Api.Services;
using Api.User;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions() {
    ApplicationName = typeof(Program).Assembly.FullName,
    EnvironmentName = "Development",
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = "wwwroot",
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    swaggerOptions => {
        swaggerOptions.SwaggerDoc("v1", new() { Title = "Api", Version = "v1" });
        swaggerOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {
            Description = "Enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "Jwt"

        }); 
        swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement {
            {
                new OpenApiSecurityScheme {
                    Reference = new OpenApiReference {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] { }
            }
        });
    });

builder.Services.AddControllers(options => {}).AddJsonOptions(jsonOptions => {
    jsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null;
    jsonOptions.JsonSerializerOptions.WriteIndented = true;
    jsonOptions.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddDbContextFactory<FitnessContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Fitness")),
    ServiceLifetime.Scoped);
builder.Services.AddDbContextFactory<UserContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Users")),
    ServiceLifetime.Scoped);


builder.Services.AddAuthorization();
builder.Services.AddProblemDetails();
// builder.Services.AddApiVersioning();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddIdentityApiEndpoints<ApplicationUser>(
        options => {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = true;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
        })
    .AddEntityFrameworkStores<UserContext>()
    .AddSignInManager();

var validIssuer = builder.Configuration.GetValue<string>("Jwt:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("Jwt:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("Jwt:SymmetricSecurityKey");

builder.Services.AddAuthentication(
    options => {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters() {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = validIssuer,
            ValidAudience = validAudience,
            IssuerSigningKey  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricSecurityKey))
        };
    }
);

var app = builder.Build();

app.Urls.Add(builder.Configuration.GetValue<string>("Urls:http"));
app.Urls.Add(builder.Configuration.GetValue<string>("Urls:https"));
//Ensure that the Migrations are applied
using (var scope = app.Services.CreateScope()) {
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<UserContext>();
    context.Database.Migrate();
    
    var fitnessContext = services.GetRequiredService<FitnessContext>();
    fitnessContext.Database.Migrate();
}

app.UseAuthentication();
app.UseAuthorization();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseCors(opt => {
    opt.AllowAnyOrigin();
    opt.AllowAnyMethod();
    opt.AllowAnyHeader();
});

app.UseHttpsRedirection();
app.MapIdentityApi<ApplicationUser>();
app.Run();


