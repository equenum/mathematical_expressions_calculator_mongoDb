using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathematicalExpressionsCalculator.Library.Observers
{
    public class ExpressionDto
    {
        [BsonId]
        public Guid Id { get; set; }
        public string Infix { get; set; }
        public string Result { get; set; }
    }
}
