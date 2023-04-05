using Microsoft.EntityFrameworkCore;
using UploadExcelFileTask.Data;
using UploadExcelFileTask.Data.Repo;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Connect to database
builder.Services.AddDbContext<AAISContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
//Allow Cross-Origin Resource Sharing
builder.Services.AddCors();
builder.Services.AddTransient<IUploadExcelFile, UploadExcelFile>();
builder.Services.AddHttpContextAccessor();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(
       options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
   );

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
