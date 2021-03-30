﻿using Autofac;
using MathematicalExpressionsCalculator.Library;
using MathematicalExpressionsCalculator.Library.Repositories;
using MathematicalExpressionsCalculator.Library.Utilities;
using MathematicalExpressionsCalculator.Library.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.UI
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterType<ConsoleMessenger>().As<IConsoleMessenger>();

            builder.RegisterType<FileValidator>().As<IFileValidator>();
            builder.RegisterType<FileRepository>().As<IFileRepository>();
            builder.RegisterType<ExpressionValidator>().As<IExpressionValidator>();
            builder.RegisterType<ConsoleRepository>().As<IConsoleRepository>();
            builder.RegisterType<ConsoleInputCatcher>().As<IInputCatcher>();

            return builder.Build();
        }
    }
}
