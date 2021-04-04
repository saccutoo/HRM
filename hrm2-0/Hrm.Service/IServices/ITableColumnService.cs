namespace Hrm.Service
{
    public partial interface ITableColumnService : IBaseService
    {
        string GetTableColumn(string tableName, bool isUpdatingField = false, long columnId = 0, int groupId = 0);
        string ExcuteSqlString(string sqlData);
    }
}
