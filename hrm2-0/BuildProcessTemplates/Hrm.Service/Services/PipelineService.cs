using Hrm.Common;
using Hrm.Repository;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Hrm.Service
{
    public partial class PipelineService : IPipelineService
    {
        IPipelineRepository _pipelineRepository;
        private string _dbName;
        public PipelineService(IPipelineRepository pipelineRepository)
        {
            _pipelineRepository = pipelineRepository;
            if (!string.IsNullOrEmpty(CurrentUser.DbName))
            {
                this._dbName = CurrentUser.DbName;
            }
        }
        public string GetPipelineStepByMenuName(string menuName, long staffId = 0)
        {
            var response = this._pipelineRepository.GetPipelineStepByMenuName(menuName, _dbName, staffId);
            return JsonConvert.SerializeObject(response);
        }
        public string GetPipeline()
        {
            var response = this._pipelineRepository.GetPipeline( _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetPipelineByMenuName(string menuName)
        {
            var response = this._pipelineRepository.GetPipelineByMenuName(_dbName,menuName);
            return JsonConvert.SerializeObject(response);
        }
        public string SavePipeline(PipelineEntity pipeline, List<PipelineStepType> pipelineStep)
        {
            var response = this._pipelineRepository.SavePipeline(pipeline, pipelineStep, _dbName);
            return JsonConvert.SerializeObject(response);
        }

        public string GetPipelineStepByPipelineId(long pipelineId)
        {
            var response = this._pipelineRepository.GetPipelineStepByPipelineId(pipelineId, _dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetPipelineById(long Id)
        {
            var response = this._pipelineRepository.GetPipelineById(Id,_dbName);
            return JsonConvert.SerializeObject(response);
        }
        public string GetPipelineStepByStaffId(long pipelineId,long staffId )
        {
            var response = this._pipelineRepository.GetPipelineStepByStaffId(pipelineId,staffId, _dbName);
            return JsonConvert.SerializeObject(response);
        }

    }
}
