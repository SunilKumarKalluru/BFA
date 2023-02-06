using BrownFieldAirLine.Services.CheckInMicroService.Context;
using Microsoft.EntityFrameworkCore;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.CheckInRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BaggageRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.BoardingRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.SeatingRepository;
using BrownFieldAirLine.Services.CheckInMicroService.Repository.LoyaltyRepository;
using Serilog;
using Serilog.Events;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((hostingContext, loggerConfig) =>
            loggerConfig.ReadFrom.Configuration(hostingContext.Configuration));
    ///Adding Swagger Service for Development environment
builder.Services.AddSwaggerGen( c => {
     c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "BrownFieldAirLine.Services.Check-InMicroservice",
            Version = "v1",
            Description = "An API to perform all web checkin operations of passengers",
            
        });
    // Set the comments path for the Swagger JSON and UI.
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
    }
);

    ///Adding Controllers Service
//builder.Services.AddControllers();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

    ///Adding automapper service 
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    ///Connection string configuration for Db context
builder.Services.AddDbContext<BrownFieldAirLineContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnectStr")));

    ///Repositories
builder.Services.AddScoped<ICheckInRepository,CheckInRepository>();
builder.Services.AddScoped<IBaggageRepository,BaggageRepository>();
builder.Services.AddScoped<IBoardingRepository,BoardingRepository>();
builder.Services.AddScoped<ISeatingRepository,SeatingRepository>();
builder.Services.AddScoped<ILoyaltyRepository,LoyaltyRepository>();

    ///CORS policy

builder.Services.AddCors(x => x.AddPolicy("AllowOrigin",options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

    ///Configuring Middleware

var app = builder.Build();
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
app.UseSwaggerUI(c => {
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Check-In Microservice V1");
    c.RoutePrefix = "";
  });

app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    ///Application Environment 
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();
app.UseHttpsRedirection();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
