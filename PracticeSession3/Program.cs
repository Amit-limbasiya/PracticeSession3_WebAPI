using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using PracticeSession3.Data;
using PracticeSession3.Services;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MyDBContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.RequestProperties| HttpLoggingFields.RequestBody| HttpLoggingFields.ResponsePropertiesAndHeaders| HttpLoggingFields.ResponseBody;
    logging.RequestHeaders.Add("My Request Header");
    logging.ResponseHeaders.Add("My Response Header");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpLogging();
app.Use(async (context,next) =>
{
    var sw = Stopwatch.StartNew();
    sw.Start();
    
    await next.Invoke();
    
    sw.Stop();
    Console.WriteLine($"Logging time for previous request : {sw.Elapsed}");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
