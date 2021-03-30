using MathematicalExpressionsCalculator.Library.Observers;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.Library.Repositories
{
    public class DatabaseRepository : IDatabaseRepository
    {
        private readonly IMongoDatabase db;
        private string _table = "Expressions";
        private List<IExpressionSubject> _store = new List<IExpressionSubject>();

        public DatabaseRepository()
        {
            var client = new MongoClient();
            db = client.GetDatabase("MathExpressions");
        }

        public void Add()
        {
            var collection = db.GetCollection<ExpressionDto>(_table);

            foreach (var expression in _store)
            {
                var expressionDto = new ExpressionDto() 
                {
                    Infix = string.Join("", expression.InfixNotationValue),
                    Result = expression.Result
                };

                collection.InsertOne(expressionDto);
            }
        }

        public List<IExpressionSubject> Get()
        {
            var collection = db.GetCollection<ExpressionDto>(_table);
            var mongoCollection = collection.Find(new BsonDocument()).ToList();

            var expressionSubjects = new List<IExpressionSubject>();

            foreach (var expressionDto in mongoCollection)
            {
                var expressionSubject = new ExpressionSubject(expressionDto.Infix)
                {
                    Result = expressionDto.Result
                };

                expressionSubjects.Add(expressionSubject);
            }

            return expressionSubjects;
        }

        public IExpressionSubject GetById(Guid id)
        {
            var collectioin = db.GetCollection<ExpressionDto>(_table);
            var filter = Builders<ExpressionDto>.Filter.Eq("Id", id);
            var expressionDto = collectioin.Find(filter).FirstOrDefault();

            var expressionSubject = new ExpressionSubject(expressionDto.Infix)
            {
                Result = expressionDto.Result
            };

            return expressionSubject;
        }

        public void Remove(Guid id)
        {
            var collectioin = db.GetCollection<ExpressionDto>(_table);
            var filter = Builders<ExpressionDto>.Filter.Eq("Id", id);
            collectioin.DeleteOne(filter);
        }

        public void Upsert(Guid id, IExpressionSubject expression)
        {
            var collectioin = db.GetCollection<ExpressionDto>(_table);
            var filter = Builders<ExpressionDto>.Filter.Eq("Id", id);

            var expressionDto = new ExpressionDto() 
            {
                Infix = string.Join("", expression.InfixNotationValue),
                Result = expression.Result
            };

            collectioin.ReplaceOne(
                filter,
                expressionDto,
                new ReplaceOptions() { IsUpsert = true });
        }

        public void SetTable(string tableName)
        {
            _table = tableName;
        }

        public void AddExpressionToStore(IExpressionSubject expression)
        {
            _store.Add(expression);
        }
    }
}
