using CommandService;
using CommandService.AsyncDataServices;
using CommandService.Data;
using CommandsService.EventProcessing;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Данный код на языке C# использует метод расширения AddHostedService для добавления размещенной службы с именем MessageBusSubscriber в контейнер службы.
//Размещенная служба в ASP.NET Core — это фоновая служба, которая асинхронно выполняет фоновую задачу при запуске приложения, 
//и служба останавливается при остановке приложения. В основном он используется для длительных задач, 
//таких как отправка электронных писем или обработка данных по расписанию.
//Здесь MessageBusSubscriber — это класс, реализующий интерфейс IHostedService. 
//Когда этот класс добавляется в качестве размещенной службы с помощью метода AddHostedService,
// он будет зарегистрирован в контейнерной системе внедрения зависимостей (DI) ASP.NET Core.
//Таким образом, всякий раз, когда приложение запускается, эта фоновая служба запускается и выполняет задачи,
//упомянутые в классе MessageBusSubscriber, которые могут быть чем угодно, от получения сообщений из очереди сообщений и их соответствующей обработки.
builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Db Inmemory
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<ICommandRepo, CommandRepo>();

builder.Services.AddControllers();
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
