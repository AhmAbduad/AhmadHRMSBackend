using AhmadHRMSBackend.Data;
using AhmadHRMSBackend.DataAccessLayer.Attendance;
using AhmadHRMSBackend.DataAccessLayer.EmployeeList;
using AhmadHRMSBackend.DataAccessLayer.Leave;
using AhmadHRMSBackend.DataAccessLayer.MarkAttendance;
using AhmadHRMSBackend.DataAccessLayer.TimeSheet;
using AhmadHRMSBackend.Interfaces;
using AhmadHRMSBackend.Services.Attendance;
using AhmadHRMSBackend.Services.EmployeeList;
using AhmadHRMSBackend.Services.Leave;
using AhmadHRMSBackend.Services.MarkAttendance;
using AhmadHRMSBackend.Services.TimeSheet;
using AhmadHRMSBackend.UnitofWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IEmployeeList, EmployeeListRepository>();
builder.Services.AddScoped<EmployeeListService>();
builder.Services.AddScoped<IAttendance, AttendanceRepository>();
builder.Services.AddScoped<AttendanceService>();
builder.Services.AddScoped<IMarkAttendance, MarkAttendanceRepository>();
builder.Services.AddScoped<MarkAttendanceService>();
builder.Services.AddScoped<ILeave, LeaveRepository>();
builder.Services.AddScoped<LeaveService>();
builder.Services.AddScoped<ITimeSheet,TimeSheetRepository>();
builder.Services.AddScoped<TimeSheetService>();


builder.Services.AddScoped<IUnitofWork, UnitofWork>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDev",
        policy => policy
            .WithOrigins(
                "http://localhost:4200"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            );
});


var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
