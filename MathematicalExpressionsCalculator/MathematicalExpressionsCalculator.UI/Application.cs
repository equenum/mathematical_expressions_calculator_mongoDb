using MathematicalExpressionsCalculator.Library;
using MathematicalExpressionsCalculator.Library.Observers;
using MathematicalExpressionsCalculator.Library.Repositories;
using MathematicalExpressionsCalculator.Library.Utilities;
using MathematicalExpressionsCalculator.Library.Validation;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.UI
{
    public class Application : IApplication
    {
        private readonly IFileValidator _fileValidator;
        private readonly IExpressionValidator _expressionValidator;
        private readonly IConsoleMessenger _consoleMessenger;
        private readonly IInputCatcher _userInputCatcher;
        private readonly IUnitOfWork _unitOfWork;

        public Application(IFileValidator fileValidator, IExpressionValidator expressionValidator,
                           IConsoleMessenger consoleMessenger, IInputCatcher userInputCatcher, 
                           IUnitOfWork unitOfWork)
        {
            _fileValidator = fileValidator;
            _expressionValidator = expressionValidator;
            _consoleMessenger = consoleMessenger;
            _userInputCatcher = userInputCatcher;
            _unitOfWork = unitOfWork;
        }

        public void Run()
        {
            Log.Information("Application was started.");
            _consoleMessenger.WelcomeMessage();

            string userInput = _userInputCatcher.Capture();

            if (string.IsNullOrEmpty(userInput) == false)
            {
                _fileValidator.FilePath = userInput;

                if (_fileValidator.Validate())
                {
                    Log.Information("User input is determined as a file path.");
                    _unitOfWork.FileRepository.SetInputFilePath(userInput);

                    List<IExpressionSubject> fileExpressions = _unitOfWork.FileRepository.Get();
                    Log.Information("Input file was loaded.");

                    foreach (IExpressionSubject expression in fileExpressions)
                    {
                        _expressionValidator.Expression = string.Join("", expression.InfixNotationValue);

                        if (_expressionValidator.Validate())
                        {
                            expression.Calculate();
                            _unitOfWork.DatabaseRepository.AddExpressionToStore(expression);
                        }
                    }

                    _unitOfWork.FileRepository.Add();
                    _unitOfWork.DatabaseRepository.Add();
                    Log.Information("The result was written to output the file and database.");
                }
                else
                {
                    Log.Information("User input was determined as a mathematical expression.");
                    _unitOfWork.ConsoleRepository.AddExpressionToStore(userInput);

                    List<IExpressionSubject> consoleExpressions = _unitOfWork.ConsoleRepository.Get();
                    Log.Information("Input expression was loaded.");

                    foreach (IExpressionSubject expression in consoleExpressions)
                    {
                        expression.Calculate();
                        _unitOfWork.DatabaseRepository.AddExpressionToStore(expression);
                    }

                    _unitOfWork.ConsoleRepository.Add();
                    _unitOfWork.DatabaseRepository.Add();
                    Log.Information("Result was written to console.");
                }
            }
            else
            {
                _consoleMessenger.EmptyInputMessage();
                Log.Error("Empty input.");
            }

            Log.Information("Application was ended.");
        }
    }
}
