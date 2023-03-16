using FluentValidation;
using GymAndYou___MinimalAPI___Project;
using GymAndYou___MinimalAPI___Project.Gyms;
using GymAndYou___MinimalAPI___Project.Gyms.Validator;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var authentication = new AuthenticationDetails();
builder.Configuration.GetSection("Authentication").Bind(authentication);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register services
builder.Services.AddSingleton<GymService>();
builder.Services.AddScoped<IValidator<Gym>,GymValidator>();
builder.Services.AddSingleton(authentication);

//Authentication

    builder.Services.AddAuthentication(option => 
    {
        option.DefaultAuthenticateScheme = "Bearer";
        option.DefaultScheme = "Bearer";
        option.DefaultChallengeScheme = "Bearer";
    }).AddJwtBearer(config => {
        config.RequireHttpsMetadata = false;
        config.SaveToken = true;
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = authentication.JwtIssuer,
            ValidAudience = authentication.JwtIssuer,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authentication.JwtKey))
        };
    });
//Authorization
    builder.Services.AddAuthorization();
    

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.RegisterEndpoints();
app.RegisterAccountEndpoints();

app.Run();

