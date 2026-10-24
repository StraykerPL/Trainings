using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using CalculatorWPF.ViewModels;

namespace CalculatorWPF.Views
{
    public partial class CalcUI : Window, IUserInterface
    {
        public IViewModel ViewModel { get; }

        /// <summary>
        /// Basic UI constructor.
        /// </summary>
        public CalcUI()
        {
            InitializeComponent();
            ViewModel = new StandardViewModel(UpdateDataBox);
        }

        /// <summary>
        /// UI constructor for dynamic <see cref="IViewModel"/> object.
        /// </summary>
        /// <param name="viewModel"><see cref="IViewModel"/> handler for app's logic.</param>
        public CalcUI(IViewModel viewModel)
            : base()
        {
            ViewModel = viewModel;
        }

        /// <summary>
        /// Handler for buttons. Forwards execution to ViewModel.
        /// </summary>
        /// <param name="sender">Defines button, which has been pressed.</param>
        /// <param name="e">Standard arguments for event.</param>
        public void ButtonClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var buttonName = (string)button.Content;

            ViewModel.DataHandler(buttonName);
        }

        /// <summary>
        /// Handler for data box. Forwards execution to ViewModel.
        /// </summary>
        /// <param name="sender">Always must be <see cref="TextBox"/></param>
        /// <param name="e">Standard arguments for event.</param>
        public void DataBoxClicked(object sender, KeyboardFocusChangedEventArgs e)
        {
            var dataContainer = (TextBox)sender;
            var data = dataContainer.Text;

            ViewModel.DataBoxHandler(data);
        }

        public void UpdateDataBox(string data)
        {
            DataBox.Text = data;
        }
    }
}
