using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameJiJia.Models
{
    public class comment
    {
        [BsonExtraElements]
        public BsonDocument CatchAll { get; set; }
    }
}
