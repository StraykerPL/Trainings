using System;
using System.Collections.Generic;

namespace CalculatorWPF.Models
{
    public class StandardDataProvider : IDataModel
    {
        public List<string> Values { get; private set; }

// This warning is here because of creating objects indirectly, nothing to worry here.
#pragma warning disable CS8618
        public StandardDataProvider()
#pragma warning restore CS8618
        {
            RecreateObjects();
        }

        public void RecreateObjects()
        {
            Values = new();
        }
    }
}
