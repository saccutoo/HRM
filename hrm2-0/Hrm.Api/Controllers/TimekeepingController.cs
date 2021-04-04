using Hrm.Api.Models;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Core.Infrastructure;
using Hrm.Framework.Models;
using Hrm.Repository.Entity;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Hrm.Api.Controllers
{
    public class TimekeepingController : CommonController
    {
        public TimekeepingController()
        {
            
        }

        [Route("api/timekeeping/Beet")]
        [HttpPost]
        public IHttpActionResult Beet(WorkingdayCheckInOutBeetModel model)
        {
            //var model = new WorkingdayCheckInOutBeetModel();
            var modelData = new WorkingdayCheckInOutModel()
            {
                SSN = model.person_id,
                WorkingDayMachineId = model.camera_id,
                Emotion = model.emotion,
                CheckTime = model.time,
                ImageUrl = model.image
            };
            if (model != null && DateTime.Compare(model.time ,DateTime.MinValue) !=0 && model.person_id != 0 && model.camera_id != 0)
            {
                var _workingDayService = EngineContext.Current.Resolve<IWorkingdayService>();
                var dataUser = _workingDayService.GetCustomerByTimeKeeper(Timekeeper.Beet, modelData.WorkingDayMachineId);
                var dataUserResult = JsonConvert.DeserializeObject<HrmResultModel<CustomerModel>>(dataUser);
                if (dataUserResult != null && dataUserResult.Results != null && dataUserResult.Results.Count > 0 && !String.IsNullOrEmpty(dataUserResult.Results.FirstOrDefault().DbName))
                {
                    var dataInOut = MapperHelper.Map<WorkingdayCheckInOutModel, WorkingdayCheckInOutEntity>(modelData);
                    dataInOut.DBName = dataUserResult.Results.FirstOrDefault().DbName;
                    var dataInOutResponse = _workingDayService.SaveCheckinCheckoutFromMachine(dataInOut);
                    var supplementNeedApprovalResult = JsonConvert.DeserializeObject<HrmResultModel<WorkingdayCheckInOutModel>>(dataInOutResponse);
                    if (supplementNeedApprovalResult != null && supplementNeedApprovalResult.Results != null && supplementNeedApprovalResult.Results.Count > 0)
                    {
                        return Json(new { Response = 200 });
                    }
                }
            }
            return BadRequest("Error");
        }
        public string Get1()
        {
            return "hihi";
        }
    }
}
