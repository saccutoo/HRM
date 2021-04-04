using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Core
{
    class HrmException : Exception
    {
        public HrmException()
        {
        }

        public HrmException(string message)
            : base(message)
        {
        }

       
        public HrmException(string messageFormat, params object[] args)
			: base(string.Format(messageFormat, args))
		{
        }

        protected HrmException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        public HrmException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
