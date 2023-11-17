using Microsoft.EntityFrameworkCore;
using TaxCalculator.Entities.Context;
using TaxCalculator.Repository;
using TaxCalculator.Repository.IRepositories;
using TaxCalculator.Repository.Repositories;
using TaxCalculator.Service.BusinessContracts;
using TaxCalculator.Service.BusinessServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TaxCalculatorApiContext")));

builder.Services.AddScoped<DbContext, DataContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IDbInitializer, DbInitializer>();

builder.Services.AddTransient<IPostalCodeTaxTypeRepository, PostalCodeTaxTypeRepository>();
builder.Services.AddTransient<IProgressiveTaxBracketRepository, ProgressiveTaxBracketRepository>();
builder.Services.AddTransient<ITaxCalculationRecordRepository, TaxCalculationRecordRepository>();

builder.Services.AddTransient<IPostalCodeTaxTypeService, PostalCodeTaxTypeService>();
builder.Services.AddTransient<IProgressiveTaxBracketService, ProgressiveTaxBracketService>();
builder.Services.AddTransient<ITaxCalculationRecordService, TaxCalculationRecordService>();


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
