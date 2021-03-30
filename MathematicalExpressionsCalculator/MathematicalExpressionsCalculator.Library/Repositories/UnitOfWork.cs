using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.Library.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IConsoleRepository ConsoleRepository { get; }
        public IFileRepository FileRepository { get; }
        public IDatabaseRepository DatabaseRepository { get; }

        public UnitOfWork(IConsoleRepository consoleRepository, 
                          IFileRepository fileRepository, 
                          IDatabaseRepository databaseRepository)
        {
            ConsoleRepository = consoleRepository;
            FileRepository = fileRepository;
            DatabaseRepository = databaseRepository;
        }
    }
}
