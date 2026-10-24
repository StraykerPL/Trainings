using GroupManager.Data.Models;

namespace GroupManager.Data.Services.Interfaces
{
    public interface ILoggingManager
    {
        string LogAction(LogActionType type, User user, string groupName);
    }
}