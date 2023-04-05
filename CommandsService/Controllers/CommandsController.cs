using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{

    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {

        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public CommandsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CommandReadDto>> GetCommandsforPlatform(int platformId)
        {
            Console.WriteLine($" --> Hit GetCommandsforPlatformmm: {platformId}");
            if (!_repository.PlatformExist(platformId))
                return NotFound();

            var commands = _repository.GetCommandsForPlatform(platformId);
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));
        }

        //GET   api/c/platforms/{platformId}/commands/{commandId}
        [HttpGet("{commandId}", Name = "GetCommandforPlatform")]
        public ActionResult<CommandReadDto> GetCommandforPlatform(int platformId, int commandId)
        {
            Console.WriteLine($" --> Hit GetCommandforPlatform: {platformId} / {commandId}");
            if (!_repository.PlatformExist(platformId))
                return NotFound();

            var command = _repository.GetCommand(platformId, commandId);
            if (command == null)
                return NotFound();

            return Ok(_mapper.Map<CommandReadDto>(command));
        }
        //POST  api/c/platforms/{platformId}/commands/
        [HttpPost]
        public ActionResult<CommandReadDto> CreateCommandForPlatform(int platformId, CommandCreateDto commandDto)
        {

            Console.WriteLine($" --> Hit CreateCommandForPlatform: {platformId}");
            if (!_repository.PlatformExist(platformId))
                return NotFound();

            var command = _mapper.Map<Command>(commandDto); //result Command
            _repository.CreateCommand(platformId, command);
            _repository.SaveChanges();

            var commandReadDto = _mapper.Map<CommandReadDto>(command); //from Command to CommandCreateDto

            return CreatedAtAction(nameof(GetCommandforPlatform),
                new { platformId = platformId, commandId = commandReadDto.Id }, commandReadDto);


        }



    }

}