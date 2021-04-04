using Hrm.Framework.Models;
using System.Web;
using Hrm.Repository;
using Hrm.Common;
using Hrm.Repository.Entity;
using Hrm.Common.Helpers;

namespace Hrm.Framework.Context
{
    public static class CurrentContext
    {
        public static UserModel User
        {
            get; set;
        }
        public static string DbName { get; set; }
        public static string Theme
        {
            get; set;
        }
        public static string WebBaseUrl { get; set; }
    }
}
