using System.Text.Json;
using AutoMapper;
using CommandService.Data;
using CommandService.Models;
using CommandsService.Dtos;
namespace CommandsService.EventProcessing
{

    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }

        //у нас есть событие, опубликованное платформой, мы хотим добавить его в нашу базу данных, 
        //что в конечном итоге мы и хотим сделать
        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.PlatformPublished:
                    addPlatform(message);
                    break;
                default:
                    break;
            }
        }

        //определения для нашего event (в нашем случае  platformPublishedDto.Event = "Platform_Published";)
        // для проекта PlatformService контролерра Platform, метода CreatePlatform 
        private EventType DetermineEvent(string notifcationMessage)
        {
            Console.WriteLine("--> Determining Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notifcationMessage);

            switch (eventType.Event)
            {
                case "Platform_Published":
                    System.Console.WriteLine("Platform_Published Event detected");
                    return EventType.PlatformPublished;
                default:
                    System.Console.WriteLine("--> Could not determone the event type");
                    return EventType.Undetermined;
            }
        }
        private void addPlatform(string platformPublishedMessage)
        {

            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();
                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if (!repo.ExtermalPlatformExist(plat.ExternalId))
                    {
                        repo.CreatePlatform(plat);
                        repo.SaveChanges();
                        System.Console.WriteLine("--> Platform added!...");

                    }
                    else
                    {
                        System.Console.WriteLine("--> Platform already existis...");
                    }
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"--> Could not be add Platform to DB {ex.Message}");
                }
            }
        }

    }

    enum EventType
    {
        PlatformPublished,
        Undetermined
    }
}