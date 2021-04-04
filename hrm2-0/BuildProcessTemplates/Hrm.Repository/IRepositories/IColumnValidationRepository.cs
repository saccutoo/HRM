using Hrm.Repository.Entity;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IColumnValidationRepository
    {
        HrmResultEntity<ColumnValidationEntity> GetColumnValidationByTable(string tableName, string dbName);
    }
}
