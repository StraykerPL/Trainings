# CalculatorWPF

Additional info:
- Documentation can be generated from XML signatures in code,
- Every class, that is supposed to be in any of MVVM layers, must implement interface for this specific layer:
	- for View: IUserInterface,
	- for View Model: IViewModel,
	- for Model: IDataModel,
- StandardViewModel.DataHandler method can be enhanced by breaking into specialised classes/methods,
- projects' targeting build versions are set for .NET 6 on Windows 10 18362, minimal version set to Windows 7.0,

Issues during development:
- Not using DataBinding between View and ViewModel layers, direct execution forwarding in code-behind,
not bad in terms of coding but should be changed for easier future development,
- On my development environment occured issue with test detection, described under https://github.com/microsoft/testfx/issues/789,
work-arounds don't work, tests should work correctly on other environments,
- View is updated by ViewModel via method delegation, not notification, not fully MVVM-compatible code,
- StandardViewModel.ProcessCalculation requires full calculation in writting-correct order, eg. "√5=" not "5√", due to use of DataTable class for evaluation,
- There's a problem with percent calculation: StandardViewModel.ProcessCalculation misinterprets the signature of "%", marking it as Modulus,
- Too much code is in StandardViewModel class, which is not allowed by Single Responsibility rule from SOLID, private methods holds business logic, which should be in separated classes, this makes StandardViewModel class partly untestable,