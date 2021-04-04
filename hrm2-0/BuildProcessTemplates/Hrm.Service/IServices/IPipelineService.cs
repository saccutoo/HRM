using System;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial interface IPipelineService : IBaseService
    {
        string GetPipelineStepByMenuName(string menuName, long staffId = 0);
        string GetPipeline();
        string GetPipelineByMenuName(string menuName);
        string SavePipeline(PipelineEntity pipeline, List<PipelineStepType> pipelineStep);
        string GetPipelineStepByPipelineId(long pipelineId);
        string GetPipelineById(long Id);
        string GetPipelineStepByStaffId(long pipelineId,long staffId);
    }
}
