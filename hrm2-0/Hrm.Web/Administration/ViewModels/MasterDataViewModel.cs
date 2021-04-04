using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;

namespace Hrm.Admin.ViewModels
{
    public class MasterDataViewModel
    {
        public TableViewModel Table { get; set; }
        public List<MasterDataModel> MasterDatas { get; set; } = new List<MasterDataModel>();
        public MasterDataModel MasterData { get; set; }

        public List<dynamic> MasterDataStatus { get; set; }
        public List<dynamic> ListMasterDataByLanguage { get; set;}
        public List<dynamic> Languages { get; set; }
        public long GroupId { get; set; }
    }
}