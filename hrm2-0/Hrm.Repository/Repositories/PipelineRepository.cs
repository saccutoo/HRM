using Hrm.Common;
using Hrm.Common.Dapper;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Hrm.Common.Helpers;
namespace Hrm.Repository
{
    public partial class PipelineRepository : CommonRepository, IPipelineRepository
    {
        
        public HrmResultEntity<PipelineStepEntity> GetPipelineStepByMenuName(string menuName, string dbName, long staffId = 0)
        {
            var par = new DynamicParameters();
            par.Add("@MenuName", menuName);
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            return ListProcedure<PipelineStepEntity>("Pipeline_Get_GetPipelineStepByMenuName", par, dbName: dbName);
        }
        public HrmResultEntity<PipelineEntity> GetPipeline( string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            return ListProcedure<PipelineEntity>("Pipeline_Get_GetPipeline", par, dbName: dbName);
        }
        public HrmResultEntity<bool> SavePipeline(PipelineEntity pipeline,List<PipelineStepType> pipelineStep,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", pipeline.Id);
            par.Add("@Name", pipeline.Name);
            par.Add("@MenuId", pipeline.MenuId);
            par.Add("@Description", pipeline.Description);
            par.Add("@IsDefault", pipeline.IsDefault);
            par.Add("@CreatedBy", pipeline.CreatedBy);
            par.Add("@UpdatedBy", pipeline.UpdatedBy);
            par.Add("@DbName",dbName);
            par.Add("@PipelineStep", pipelineStep.ConvertToUserDefinedDataTable(), DbType.Object);
            return Procedure("Pipeline_Update_SavePipeline", par);
        }

        public HrmResultEntity<PipelineStepEntity> GetPipelineStepByPipelineId(long pipelineId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@PipelineId", pipelineId);
            par.Add("@DbName", dbName);
            return ListProcedure<PipelineStepEntity>("Pipeline_Get_GetPipelineStepByPipelineId", par, dbName: dbName);
        }
        public HrmResultEntity<PipelineEntity> GetPipelineById(long Id,string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@Id", Id);
            par.Add("@DbName", dbName);
           return ListProcedure<PipelineEntity>("Pipeline_Get_GetPipelineById", par, dbName: dbName);
        }
        public HrmResultEntity<PipelineEntity> GetPipelineByMenuName(string dbName, string menuName)
        {
            var par = new DynamicParameters();
            par.Add("@DbName", dbName);
            par.Add("@MenuName", menuName);
            return ListProcedure<PipelineEntity>("Pipeline_Get_GetPipelineByMenuName", par, dbName: dbName);
        }
        public HrmResultEntity<PipelineStepEntity> GetPipelineStepByStaffId(long pipelineId,long staffId, string dbName)
        {
            var par = new DynamicParameters();
            par.Add("@PipelineId", pipelineId);
            par.Add("@StaffId", staffId);
            par.Add("@DbName", dbName);
            return ListProcedure<PipelineStepEntity>("Pipeline_Get_GetPipelineStepByStaffId", par, dbName: dbName);
        }
    }

}
