using McsCore.Entities;
using McsUserLogs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McsUserLogs.Services.Base
{
    public interface IUserLogService
    {
        Task SetEventUserLog(UserLogs log);
        Task<List<McsUserLogResponse>> GetUserLogByUserId(Guid userId);
    }
}
