using Avalonia.Controls;
using AvaloniaTraining.ViewModels;

namespace AvaloniaTraining.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowState = WindowState.Maximized;
        }

        public MainWindow(MainWindowViewModel vm)
            : this()
        {
            DataContext = vm;
        }
    }
}