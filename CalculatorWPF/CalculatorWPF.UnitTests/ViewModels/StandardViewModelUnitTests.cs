using System;
using System.Collections.Generic;
using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CalculatorWPF.ViewModels;
using CalculatorWPF.Models;

// For copy-paste:
// Prepare:

// Execute:

// Verify:


namespace CalculatorWPF.UnitTests.ViewModels
{
    [TestClass]
    public class StandardViewModelUnitTests
    {
        [TestMethod]
        public void ClassDataHandle_HandleDigitSave_DigitSavedToMemory()
        {
            // Prepare:
            var mockUiUpdateMethod = new Mock<Action<string>>();
            var mockDataProvider = new Mock<IDataModel>();
            mockDataProvider.Setup(x => x.Values.Add("1")).Verifiable();

            var obj = new StandardViewModel(mockUiUpdateMethod.Object, mockDataProvider.Object);
            // Execute:
            obj.DataHandler("1");
            // Verify:
            Mock.Verify(mockDataProvider);
        }

        [TestMethod]
        public void ClassDataHandle_HandleSymbolSave_SymbolSavedToMemory()
        {
            // Prepare:
            var mockUiUpdateMethod = new Mock<Action<string>>();
            var mockDataProvider = new Mock<IDataModel>();
            mockDataProvider.Setup(x => x.Values.Add("+")).Verifiable();

            var obj = new StandardViewModel(mockUiUpdateMethod.Object, mockDataProvider.Object);
            // Execute:
            obj.DataHandler("+");
            // Verify:
            Mock.Verify(mockDataProvider);
        }

        [TestMethod]
        public void ClassDataProcessing_TwoOperandAddition_AdditionSuccessful()
        {
            // Prepare:
            var list = new List<string>
            {
                "2",
                "+",
                "2"
            };

            var mockUiUpdateMethod = new Mock<Action<string>>();
            var mockDataProvider = new Mock<IDataModel>();
            mockDataProvider.Setup(x => x.Values).Returns(list);
            mockUiUpdateMethod.Setup(x => x.Invoke("2+2=4")).Verifiable();

            var obj = new StandardViewModel(mockUiUpdateMethod.Object, mockDataProvider.Object);
            // Execute:
            obj.DataHandler("="); // We want to cast calculation,
            // Verify:
            Mock.Verify(mockUiUpdateMethod);
        }

        [TestMethod]
        public void ClassDataProcessing_ClearData_DataClearedFrom()
        {
            // Prepare:
            var mockUiUpdateMethod = new Mock<Action<string>>();
            var mockDataProvider = new Mock<IDataModel>();
            mockDataProvider.Setup(x => x.RecreateObjects()).Verifiable();

            var obj = new StandardViewModel(mockUiUpdateMethod.Object, mockDataProvider.Object);
            // Execute:
            obj.DataHandler("CE"); // We want to cast clear,
            // Verify:
            Mock.Verify(mockDataProvider);
        }
    }
}
