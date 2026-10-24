using System;
using System.Windows.Input;
using CalculatorWPF.ViewModels;

namespace CalculatorWPF.Views
{
    public interface IUserInterface
    {
        IViewModel ViewModel { get; }

        void ButtonClicked(object sender, EventArgs e);

        void DataBoxClicked(object sender, KeyboardFocusChangedEventArgs e);

        void UpdateDataBox(string data);
    }
}
