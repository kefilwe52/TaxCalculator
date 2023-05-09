using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Context;
using TaxCalculator.Repository;
using TaxCalculator.Repository.IRepositories;
using TaxCalculator.Repository.Repositories;
using TaxCalculator.Service.BusinessContracts;
using TaxCalculator.Service.BusinessServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaxCalculatorApiContext")));

builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddTransient<IPostalCodeInfoRepository, PostalCodeInfoRepository>();
builder.Services.AddTransient<IProgressiveTaxRateRepository, ProgressiveTaxRateRepository>();
builder.Services.AddTransient<ITaxCalculationRepository, TaxCalculationRepository>();

builder.Services.AddTransient<IPostalCodeInfoService, PostalCodeInfoService>();
builder.Services.AddTransient<IProgressiveTaxRateService, ProgressiveTaxRateService>();
builder.Services.AddTransient<ITaxCalculationService, TaxCalculationService>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    // Get the IDbInitializer service from the scope
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();

    // Initialize the database and seed data
    dbInitializer.Initialize();
    dbInitializer.SeedData();
}

app.Run();