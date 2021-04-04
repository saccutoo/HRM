using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hrm.Common.Helpers
{
    public static class Logger
    {
        private static readonly ILog LoggerMember = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void WriteLog(string message, string sp = null)
        {

        }
    }
}
