using MyDailyRoutine.Application;
using MyDailyRoutine.Application.Repositories;
using MyDailyRoutine.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IGoalService, GoalService>();
builder.Services.AddTransient<IHabitRepository>(implementation =>
{
    var param = "C:\\Temp\\MyFakeDatabase";
    return new HabitRepositoryJson(param);
});
builder.Services.AddTransient<IGoalRepository>(implementation =>
{
    var param = "C:\\Temp\\MyFakeDatabase";
    return new GoalRepositoryJson(param);
});


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

app.Run();
