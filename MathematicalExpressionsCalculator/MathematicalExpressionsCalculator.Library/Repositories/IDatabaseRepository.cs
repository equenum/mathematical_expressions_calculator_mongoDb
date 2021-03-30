using MathematicalExpressionsCalculator.Library.Observers;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.Library.Repositories
{
    public interface IDatabaseRepository : IRepository<IExpressionSubject>
    {
        IExpressionSubject GetById(Guid id);
        void Upsert(Guid id, IExpressionSubject expression);
        void Remove(Guid id);

        void AddExpressionToStore(IExpressionSubject expression);
        void SetTable(string tableName);
    }
}
