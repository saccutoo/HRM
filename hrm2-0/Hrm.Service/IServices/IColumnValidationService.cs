namespace Hrm.Service
{
    public partial interface IColumnValidationService : IBaseService
    {
        string GetColumnValidationByTable(string tableName, string dbName);
    }
}
