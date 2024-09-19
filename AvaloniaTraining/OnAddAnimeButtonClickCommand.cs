using AvaloniaTraining.ViewModels;
using AvaloniaTraining.Views;
using CommunityToolkit.Mvvm.Input;
using System;

namespace AvaloniaTraining
{
    public sealed class OnAddAnimeButtonClickCommand : IRelayCommand
    {
        private readonly MainWindowViewModel _mainWindowViewModel;

        public event EventHandler? CanExecuteChanged;

        public OnAddAnimeButtonClickCommand(MainWindowViewModel vm)
        {
            _mainWindowViewModel = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            var msgBox = new MessageBox("Training", "Adding new anime");
            msgBox.Show();
        }

        public void NotifyCanExecuteChanged()
        {
            throw new NotImplementedException();
        }
    }
}