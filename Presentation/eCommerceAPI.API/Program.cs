using eCommerceAPI.Application.Validators.Product;
using eCommerceAPI.Persistence;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
//CORS CONFIGURATION
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://")));

builder.Services.AddControllers()
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ProductCreateValidator>()).ConfigureApiBehaviorOptions(option => option.SuppressModelStateInvalidFilter = true);
// Otomatik olarak diðer Validationlar için de geçerli olacaktýr. Ayný zamanda ConfigureApiBehaviorOptions metodu ile de validasyon durumunu manuel yönetme kabiliyeti ekledik.


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
app.UseCors();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
