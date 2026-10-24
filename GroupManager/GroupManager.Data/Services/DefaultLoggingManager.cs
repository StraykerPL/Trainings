using GroupManager.Data.Models;
using GroupManager.Data.Services.Interfaces;

namespace GroupManager.Data.Services
{
    public sealed class DefaultLoggingManager : ILoggingManager
    {
        public string LogAction(LogActionType type, User user, string groupName)
        {
            switch (type)
            {
                case LogActionType.Add:
                    return string.Format("Użytkownik {0} {1} został przeniesiony do Grupy {2}", user.Name, user.Surename, groupName[groupName.Length - 1]);

                case LogActionType.Remove:
                    return string.Format("Użytkownik {0} {1} został usunięty z Grupy {2}", user.Name, user.Surename, groupName[groupName.Length - 1]);

                default: throw new Exception();
            }
        }
    }
}