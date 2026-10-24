using MudBlazor;

namespace GroupManager.Constants
{
    public sealed class GroupsIcons
    {
        public readonly IReadOnlyDictionary<string, string> GroupsIconsList = new Dictionary<string, string>()
        {
            { "Grupa 1", Icons.Material.Filled.Star },
            { "Grupa 2", Icons.Material.Filled.Category },
            { "Grupa 3", Icons.Material.Filled.TagFaces },
            { "Grupa 4", Icons.Material.Filled.Cloud },
            { "Grupa 5", Icons.Material.Filled.Rectangle },
        };
    }
}