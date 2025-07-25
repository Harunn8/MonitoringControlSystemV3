using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsCore.Entities
{
    public class UserLogs
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonElement("UserId")]
        public Guid UserId { get; set; }
        [BsonElement("AppName")]
        public string AppName { get; set; }
        [BsonElement("Message")]
        public string Message { get; set; }
        [BsonElement("LogDate")]
        public DateTime LogDate { get; set; }
        [BsonElement("LogType")]
        public UserLogType LogType { get; set; }
    }

    public enum UserLogType
    {
        Added = 0,
        Updated = 1,
        Deleted = 2,
        LoggedIn = 3,
        LoggedOut = 4,
        PasswordChanged = 5
    }
}
