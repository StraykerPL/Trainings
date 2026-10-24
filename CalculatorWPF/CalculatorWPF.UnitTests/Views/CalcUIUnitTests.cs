using System;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using CalculatorWPF.Views;
using CalculatorWPF.ViewModels;

// For copy-paste:
// Prepare:

// Execute:

// Verify:


namespace CalculatorWPF.UnitTests.Views
{
    [TestClass]
    public class CalcUIUnitTests
    {
        // Memo: Defult constructor for CalcUI class defines static object,
        // so if app compiles successfully, there's no reason to write tests for it.

        [TestMethod]
        public void Constructor_CreateViewModel_CorrectViewModelSet()
        {
            // Prepare:
            var mock = new Mock<IViewModel>();
            // Execute:
            var ui = new CalcUI(mock.Object);
            // Verify:
            Assert.AreEqual(mock.Object, ui);
        }

        [TestMethod]
        public void Handlers_HandleButtonClick_ViewModelReceivedData()
        {
            // Prepare:
            string fakeData = "OK";
            var mockButton = new Mock<Button>(); // Not so properly, but will do the trick.
            var mockViewModel = new Mock<IViewModel>();
            mockButton.Setup(x => x.Content).Returns(fakeData);
            mockViewModel.Setup(x => x.DataHandler(fakeData)).Verifiable();
            var ui = new CalcUI(mockViewModel.Object);
            // Execute:
            ui.ButtonClicked(mockButton, new Mock<EventArgs>().Object);
            // Verify:
            Mock.Verify(mockViewModel);
        }

        [TestMethod]
        public void Handlers_HandleDataBoxClick_ViewModelReceivedData()
        {
            // Prepare:
            string fakeData = "OK";
            var mockTextBox = new Mock<TextBox>(); // Not so properly, but will do the trick.
            var mockViewModel = new Mock<IViewModel>();
            mockTextBox.Setup(x => x.Text).Returns(fakeData);
            mockViewModel.Setup(x => x.DataBoxHandler(fakeData)).Verifiable(); // Bool value same as in CalcUI class,
            var ui = new CalcUI(mockViewModel.Object);
            // Execute:
            ui.DataBoxClicked(mockTextBox, new Mock<KeyboardFocusChangedEventArgs>().Object);
            // Verify:
            Mock.Verify(mockViewModel);
        }
    }
}
