namespace Hrm.Service
{
    public partial interface ITableService : IBaseService
    {
        string GetTable(string tableName);
    }
}
