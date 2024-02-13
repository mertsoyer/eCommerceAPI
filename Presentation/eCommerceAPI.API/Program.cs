using eCommerceAPI.Application;
using eCommerceAPI.Application.Validators.Product;
using eCommerceAPI.Infrastructure.Filters;
using eCommerceAPI.Infrastructure.Services;
using eCommerceAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
//MEDIATR CONFIGURATION
builder.Services.AddApplicationServices();
//CORS CONFIGURATION
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://")));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ProductCreateValidator>()).ConfigureApiBehaviorOptions(option => option.SuppressModelStateInvalidFilter = true);
// Otomatik olarak di�er Validationlar i�in de ge�erli olacakt�r. Ayn� zamanda ConfigureApiBehaviorOptions metodu ile de validasyon durumunu manuel y�netme kabiliyeti ekledik.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
