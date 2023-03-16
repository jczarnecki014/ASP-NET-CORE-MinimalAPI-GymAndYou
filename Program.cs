using FluentValidation;
using GymAndYou___MinimalAPI___Project.Gyms;
using GymAndYou___MinimalAPI___Project.Gyms.Validator;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Register services
builder.Services.AddSingleton<GymService>();
builder.Services.AddScoped<IValidator<Gym>,GymValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.RegisterEndpoints();

app.Run();

