using Hrm.Framework.Models;
using System.Collections.Generic;

namespace Hrm.Framework.ViewModels
{
    public class ChecklistViewModel
    {
        public ChecklistViewModel()
        {
            Pipelines = new PipelineGridModel();
        }

        public List<ChecklistDetailModel> ChecklistDetail { get; set; }
        public List<ChecklistModel> Checklist{ get; set; }
        public long StaffId { get; set; }
        public bool IsSave { get; set; } = false;
        public bool ControlAction { get; set; } = true;
        public PipelineGridModel Pipelines { get; set; }
    }
}
