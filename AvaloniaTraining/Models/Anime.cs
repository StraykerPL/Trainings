namespace AvaloniaTraining.Models
{
    public class Anime(string name, int episodes)
    {
        public string Name { get; } = name;

        public string Description { get; set; } = string.Empty;

        public int Episodes { get; } = episodes;

        public int CurrentlyOnEpisode { get; set; }
    }
}