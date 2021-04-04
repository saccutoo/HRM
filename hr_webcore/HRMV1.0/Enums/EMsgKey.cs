using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Web.Enums
{
    public enum EMsgKey
    {
        Global_ObjectNotFound,
        Global_ActionFailed,

        #region QL label
        Label_Validate_NotEnterValue,
        Label_Validate_KeyAlreadyExits,
        #endregion

        #region Action
        Action_Validate_NotEnterActionName
        #endregion
    }
}
