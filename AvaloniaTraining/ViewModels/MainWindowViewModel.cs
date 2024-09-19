using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using AvaloniaTraining.Models;
using AvaloniaTraining.Views;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace AvaloniaTraining.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Anime> Animes { get; }

        public IRelayCommand OnAddAnimeButtonClick { get; }

        public MainWindowViewModel()
        {
            var animes = new List<Anime>
            {
                new("Attack on Titan", 24)
            };
            Animes = new ObservableCollection<Anime>(animes);
            OnAddAnimeButtonClick = new OnAddAnimeButtonClickCommand(this);
        }

        public void AddNewAnime(Anime anime)
        {
            Animes.Add(anime);
        }
    }
}