using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System.Collections.Generic;
namespace Hrm.Repository
{
    public partial interface IPipelineRepository
    {
        HrmResultEntity<PipelineStepEntity> GetPipelineStepByMenuName(string menuName, string dbName, long staffId = 0);
        HrmResultEntity<PipelineEntity> GetPipeline(string dbName);
        HrmResultEntity<PipelineEntity> GetPipelineByMenuName(string dbName, string menuName);
        HrmResultEntity<bool> SavePipeline(PipelineEntity pipeline, List<PipelineStepType> pipelineStep, string dbName);
        HrmResultEntity<PipelineStepEntity> GetPipelineStepByPipelineId(long pipelineId, string dbName);
        HrmResultEntity<PipelineEntity> GetPipelineById(long Id, string dbName);
        HrmResultEntity<PipelineStepEntity> GetPipelineStepByStaffId(long pipelineId,long staffId, string dbName);

    }
}
