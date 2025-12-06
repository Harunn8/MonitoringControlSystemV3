using McsCore.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorApplication.Data.Interfaces
{
    public interface IParameterLogContext
    {
        IMongoCollection<ParameterLogs> ParameterLogs { get; }
    }
}
