using eCommerceAPI.API.Configurations.ColumnWriters;
using eCommerceAPI.API.Extensions;
using eCommerceAPI.Application;
using eCommerceAPI.Application.Validators.Product;
using eCommerceAPI.Infrastructure.Filters;
using eCommerceAPI.Infrastructure.Services;
using eCommerceAPI.Persistence;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using eCommerceAPI.SignalR;
using eCommerceAPI.SignalR.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddPersistenceServices();
builder.Services.AddInfrastructureServices();
//MEDIATR CONFIGURATION
builder.Services.AddApplicationServices();
//SIGNALR
builder.Services.AddSignalRServices();

//CORS CONFIGURATION
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://").AllowCredentials().AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
    .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<ProductCreateValidator>()).ConfigureApiBehaviorOptions(option => option.SuppressModelStateInvalidFilter = true);
// Otomatik olarak diðer Validationlar için de geçerli olacaktýr. Ayný zamanda ConfigureApiBehaviorOptions metodu ile de validasyon durumunu manuel yönetme kabiliyeti ekledik.

SqlColumn sqlColumn = new SqlColumn();
sqlColumn.ColumnName = "UserName";
sqlColumn.DataType = System.Data.SqlDbType.NVarChar;
sqlColumn.PropertyName = "UserName";
sqlColumn.DataLength = 50;
sqlColumn.AllowNull = true;
ColumnOptions columnOpt = new ColumnOptions();
columnOpt.Store.Remove(StandardColumn.Properties);
columnOpt.Store.Add(StandardColumn.LogEvent);
columnOpt.AdditionalColumns = new Collection<SqlColumn> { sqlColumn };

#region Serilog Ayarlarý
Logger log = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt")
            .WriteTo.MSSqlServer(
        connectionString: "server=MDSOYER\\SQLEXPRESS;initial catalog=eCommerceAPI;TrustServerCertificate=True;integrated security=true",
        sinkOptions: new MSSqlServerSinkOptions { TableName = "LogEvents", AutoCreateSqlTable = true }, appConfiguration: null,
     columnOptions: columnOpt)
            .Enrich.FromLogContext()
    .Enrich.With<UsernameColumnWriter>()
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog(log);
# endregion 

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//JWT CONFIGURATION
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Admin", options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.Name // JWT üzerinde name claimine karþýlýk gelen deðeri User.Identity.Name prop. elde edebiliyoruz.
    };

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());

app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    var userName = context?.User?.Identity?.IsAuthenticated != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("username", userName);
    await next();
}
);

app.MapControllers();
app.MapHubs(); // SignalR Hubs
app.Run();
