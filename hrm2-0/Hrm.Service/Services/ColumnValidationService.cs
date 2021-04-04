using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;

namespace Hrm.Service
{
    public partial class ColumnValidationService : IColumnValidationService
    {
        IColumnValidationRepository _columnValidationRepository;
        private string _dbName;
        public ColumnValidationService(IColumnValidationRepository columnValidationRepository)
        {
            _columnValidationRepository = columnValidationRepository;
            _dbName = CurrentUser.DbName;
        }
        public string GetColumnValidationByTable(string tableName, string dbName)
        {
            if(dbName == "")
            {
                var response = _columnValidationRepository.GetColumnValidationByTable(tableName, _dbName);
                return JsonConvert.SerializeObject(response);
            }
            else
            {
                var response = _columnValidationRepository.GetColumnValidationByTable(tableName, dbName);
                return JsonConvert.SerializeObject(response);
            }
           
        }
    }
}
