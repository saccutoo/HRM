using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hrm.Framework.Models;
namespace Hrm.Admin.ViewModels
{
    public class PipelineViewModel
    {
        public List<ListPipeline> PipelineView { get; set; }
    }
    public class ListPipeline
    {
        public List<PipelineModel> ListPipelineModel { get; set; }
    }
    public class AddPipelineViewModel
    {
        public PipelineModel Pipeline { get; set; }
        public List<PipelineStepModel> PipelineStep { get; set; }
        public List<dynamic> Menu { get; set; }

    }
}