using CommandService.Models;
namespace CommandService.Data
{
    public interface ICommandRepo
    {

        //Platforms
        bool SaveChanges();
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform plat);
        bool PlatformExist(int PlatformId);
        bool ExtermalPlatformExist(int externalPlartofrmId);


        //Commands

        IEnumerable<Command> GetCommandsForPlatform(int PlatformId);
        Command GetCommand(int PlatformId, int commandId);
        void CreateCommand(int platform, Command command);
    }
}
