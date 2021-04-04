using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Admin.ViewModels
{
    public class ChecklistDetailViewModel
    {
        public List<dynamic> ChecklistDetail { get; set; } = new List<dynamic>();
        public List<dynamic> MasterData { get; set; } = new List<dynamic>();
        public List<dynamic> MasterDataControlType { get; set; } = new List<dynamic>();
        public ChecklistModel Checklist { get; set; } = new ChecklistModel();
        public string ActionName { get; set; }

    }
}