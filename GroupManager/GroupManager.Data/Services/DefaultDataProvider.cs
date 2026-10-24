using GroupManager.Data.Models;
using GroupManager.Data.Services.Interfaces;

namespace GroupManager.Data.Services
{
    public sealed class DefaultDataProvider : IDataProvider
    {
        private readonly List<string> _groups = new List<string>()
        {
            "Grupa 1", "Grupa 2", "Grupa 3", "Grupa 4", "Grupa 5"
        };

        private readonly List<User> _users = new List<User>()
        {
            new User() { Id = 1, Name = "Adam", Surename = "Kowalski", City = "Radom", GroupName = "UsersList" },
            new User() { Id = 2, Name = "Piotr", Surename = "Piotrowski", City = "Radom", GroupName = "UsersList" },
            new User() { Id = 3, Name = "Magda", Surename = "Nowakowska", City = "Warszawa", GroupName = "UsersList" },
            new User() { Id = 4, Name = "Jan", Surename = "Nowak", City = "Radom", GroupName = "UsersList" },
        };

        public ICollection<User> GetUsers()
        {
            return _users;
        }

        public ICollection<string> GetGroups()
        {
            return _groups;
        }
    }
}