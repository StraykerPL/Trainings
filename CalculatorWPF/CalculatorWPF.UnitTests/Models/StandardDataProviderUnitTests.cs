using System;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorWPF.Models;

// For copy-paste:
// Prepare:

// Execute:

// Verify:


namespace CalculatorWPF.UnitTests.Models
{
    [TestClass]
    public class StandardDataProviderUnitTests
    {
        [TestMethod]
        public void Constructor_CreatingObject_ObjectCreatedWithSetProperty()
        {
            // Prepare:

            // Execute:
            var obj = new StandardDataProvider();
            // Verify:
            Assert.IsNotNull(obj.Values);
        }
    }
}
