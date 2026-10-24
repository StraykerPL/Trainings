using GroupManager.Data.Models;

namespace GroupManager.Data.Services.Interfaces
{
    public interface IDataProvider
    {
        ICollection<User> GetUsers();

        ICollection<string> GetGroups();
    }
}