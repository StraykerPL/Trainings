using Avalonia.Controls;
using AvaloniaTraining.Models;

namespace AvaloniaTraining.Views
{
    public partial class MessageBox : Window
    {
        private Anime? _createdAnime;

        public readonly string Message;

        public Anime? CreatedAnime
        {
            get
            {
                var tmp = _createdAnime;
                _createdAnime = null;
                return tmp;
            }
            set
            {
                _createdAnime = value;
            }
        }

        public MessageBox()
        {
            InitializeComponent();
            Message = string.Empty;
            Width = 200;
            Height = 150;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        public MessageBox(string title, string message)
            : this()
        {
            Title = title;
            Message = message;
        }

        private void AddAnimeButtonClick()
        {
            CreatedAnime = new Anime("Aldnoah.Zero", 12);
        }
    }
}