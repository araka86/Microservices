using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;
namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly ICommandRepo _repository;
        private readonly IMapper _mapper;

        public PlatformsController(ICommandRepo repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        [HttpPost]
        public ActionResult TestInboundConnection()
        {
            System.Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test of from Platform Controllers");
        }
        [HttpPost]
        [Route("test")]
        public ActionResult TestInboundConnection2(object? inData = null)
        {
            System.Console.WriteLine("--> Inbound POST2 # Command Service");
            System.Console.WriteLine($"{inData}");
            return Ok("Inbound test2 of from Platform Controllers");
        }
        //Get all Platforms     GET     /api/c/platforms 
        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            Console.WriteLine("--> getting Platforms from CommandsService");
            var platformItems = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platformItems));
          
        }
    }
}