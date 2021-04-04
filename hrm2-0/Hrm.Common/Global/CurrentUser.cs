namespace Hrm.Common
{
    public static class CurrentUser
    {
        public static long UserId { get; set; }
        public static string UserName { get; set; }
        public static string DbName { get; set; }
        public static long LanguageId { get; set; } = 0;
        public static long RoleId { get; set; }
        public static long CurrencyId { get; set; }
    }
}
