using System;
using System.Data;
using System.Text;
using System.Windows;
using CalculatorWPF.Models;

namespace CalculatorWPF.ViewModels
{
    public class StandardViewModel : IViewModel
    {
        private readonly IDataModel _model;
        private readonly Action<string> _updateDataBox;

        /// <summary>
        /// Constructor for <see cref="StandardViewModel"/>
        /// </summary>
        /// <param name="uiUpdateMethod">Method for updating UI</param>
        public StandardViewModel(Action<string> uiUpdateMethod)
        {
            _model = new StandardDataProvider();
            _updateDataBox = uiUpdateMethod;
        }

        /// <summary>
        /// Constructor for specific <see cref="IDataModel"/> object.
        /// </summary>
        /// <param name="uiUpdateMethod">Method for updating UI</param>
        /// <param name="dataProvider"><see cref="IDataModel"/> object to handle data storage.</param>
        public StandardViewModel(Action<string> uiUpdateMethod, IDataModel dataProvider)
        {
            _model = dataProvider;
            _updateDataBox = uiUpdateMethod;
        }

        /// <summary>
        /// Default data handle method for buttons, preparing data for calculation.
        /// </summary>
        /// <param name="data"><see cref="string"/> object containing value.</param>
        public void DataHandler(string data)
        {
            // Is result requested?
            if(data == "=" && _model.Values.Count != 0)
            {
                var result = ProcessCalculation();
                _model.Values.Add("=");
                _model.Values.Add(result.ToString());
                var output = PrepareOutput();
                SetDataBox(output);
                return;
            }
            else if (data == "%")
            {
                var result = ProcessCalculation();
                _model.Values.Add("%");
                _model.Values.Add("=");
                _model.Values.Add(result.ToString());
                var output = PrepareOutput();
                SetDataBox(output);
            }
            else if(data == "CE")
            { // Reset:
                _model.RecreateObjects();
                SetDataBox("0");
                return;
            }

            // No calculation requested, input handle:
            _model.Values.Add(data);
            var operand = PrepareOutput();
            SetDataBox(operand);
        }

        /// <summary>
        /// Data handle for DataBox, handles full preparation for calculation.
        /// </summary>
        /// <param name="data"><see cref="string"/> object containing value.</param>
        public void DataBoxHandler(string data)
        {
            // Not in use now.
        }

        /// <summary>
        /// Makes calculations, based on model's data.
        /// </summary>
        private double ProcessCalculation()
        {
            // Creates string without informing symbols, like "=".
            var builder = new StringBuilder();
            foreach(var item in _model.Values)
            {
                builder.Append(item);
            }
            var tab = new DataTable();

            object? tmp;
            try
            { // Try to compute the value:
                tmp = tab.Compute(builder.ToString(), "");
            }
            catch (Exception a)
            {
                MessageBox.Show("Can't process calculation: " + a.Message);
                return double.NaN;
            }

            return Convert.ToDouble(tmp); // tmp here is "int".
        }

        /// <summary>
        /// Sets given data to UI using delegation.
        /// </summary>
        /// <param name="data"><see cref="string"/> object with data.</param>
        private void SetDataBox(string data)
        {
            _updateDataBox.Invoke(data);
        }

        /// <summary>
        /// Creates string with model's data to set it in UI.
        /// </summary>
        /// <returns><see cref="string"/> object ordered by operation.</returns>
        private string PrepareOutput()
        {
            var builder = new StringBuilder();

            foreach(var item in _model.Values)
            {
                builder.Append(item);
            }

            return builder.ToString();
        }
    }
}
