using CommandService.Models;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }


        public void CreateCommand(int platform, Command command)
        {
            if(command==null)
            {
                throw new ArgumentException(nameof(command));
            }
            command.PlatformId = platform;
            _context.Commands.Add(command);
            _context.SaveChanges();
        }

        public void CreatePlatform(Platform plat)
        {
            if(plat==null)
            {
                throw new ArgumentException(nameof(plat));
            }
            _context.Platforms.Add(plat);
             _context.SaveChanges();
        }

        public bool ExtermalPlatformExist(int externalPlartofrmId)
        {
              return _context.Platforms.Any(p => p.ExternalId == externalPlartofrmId);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands
            .Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands
            .Where(c => c.PlatformId == platformId)
            .OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExist(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}