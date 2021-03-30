using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.Library.Repositories
{
    public interface IUnitOfWork
    {
        IConsoleRepository ConsoleRepository { get; }
        IFileRepository FileRepository { get; }
        IDatabaseRepository DatabaseRepository { get; }
    }
}
