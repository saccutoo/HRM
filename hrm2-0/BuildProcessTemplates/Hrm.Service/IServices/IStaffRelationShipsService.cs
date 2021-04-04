using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Service
{
    public partial interface IStaffRelationShipsService : IBaseService
    {
        string GetRelationShipsByStaff(BasicParamType param, long staffId, out int totalRecord);
    }
}
