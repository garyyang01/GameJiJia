using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GameJiJia.Models
{
    public class UserInfo
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("AccountId")]
        public string AccountId { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("EmailAddress")] public string EmailAddress { get; set; } = null;

    }
}
