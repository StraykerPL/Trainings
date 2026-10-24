using System.Collections.Generic;

namespace CalculatorWPF.Models
{
    public interface IDataModel
    {
        List<string> Values { get; }

        void RecreateObjects();
    }
}
