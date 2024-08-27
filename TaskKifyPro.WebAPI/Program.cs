using Microsoft.EntityFrameworkCore;
using System.Globalization;
using TaskKifyPro.Business.Abstract;
using TaskKifyPro.Business.Services;
using TaskKifyPro.DataAccess.Abstract;
using TaskKifyPro.DataAccess.Context;
using TaskKifyPro.DataAccess.EntityFramework;



var builder = WebApplication.CreateBuilder(args);





builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TaskifyProDbContext>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserDutyService, UserDutyService>();
builder.Services.AddScoped<IDutyService, DutyService>();
builder.Services.AddScoped<IPerformanceService, PerformanceService>();
builder.Services.AddScoped<INotificationService, NotificationService>();


builder.Services.AddScoped<IUserDal, UserDal>();
builder.Services.AddScoped<ITeamDal, TeamDal>();
builder.Services.AddScoped<IUserDutyDal, UserDutyDal>();
builder.Services.AddScoped<IDutyDal, DutyDal>();
builder.Services.AddScoped<IPerformanceDal, PerformanceDal>();
builder.Services.AddScoped<INotificationDal, NotificationDal>();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(builder => builder
    .WithOrigins("http://localhost:3000")
    .WithHeaders("Authorization")
    .AllowAnyHeader()
    .AllowAnyMethod()
);
app.UseAuthorization();

app.MapControllers();

app.Run();