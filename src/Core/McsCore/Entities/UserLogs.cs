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
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string AppName { get; set; }
        public string Message { get; set; }
        public DateTime LogDate { get; set; }
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