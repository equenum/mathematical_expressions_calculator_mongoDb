﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.Library.Observers
{
    /// <summary>
    /// Represents an subtraction observer.
    /// </summary>
    public class SubtractionObserver : IObserver
    {
        public void Update(IExpressionSubject subject)
        {
            if ((subject as ExpressionSubject).State == ExpressionSubjectState.Subtraction)
            {
                subject.Stack.Push((subject.A - subject.B).ToString());
            }
        }
    }
}
