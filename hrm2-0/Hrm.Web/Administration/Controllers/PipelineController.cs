using Hrm.Admin.ViewModels;
using Hrm.Common;
using Hrm.Common.Helpers;
using Hrm.Framework.Controllers;
using Hrm.Framework.Helper;
using Hrm.Framework.Helpers;
using Hrm.Framework.Models;
using Hrm.Framework.ViewModels;
using Hrm.Repository.Entity;
using Hrm.Repository.Type;
using Hrm.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Hrm.Admin.Controllers
{
    public partial class PipelineController : BaseController
    {
        #region Fields
        private IPipelineService _pipelineService;
        private IStaffService _staffService;
        private IMasterDataService _masterDataService;
        private IMenuService _menuService;
        private ILocalizationService _localizationService;
        private long _languageId;
        #endregion Fields
        #region Constructors
        public PipelineController(IPipelineService pipelineService, IStaffService staffService, IMasterDataService masterDataService, IMenuService menuService, ILocalizationService localizationService)
        {
            _pipelineService = pipelineService;
            _staffService = staffService;
            _localizationService = localizationService;
            _masterDataService = masterDataService;
            _menuService = menuService;
            _languageId = CurrentUser.LanguageId;
        }
        #endregion
        public ActionResult List()
        {
            List<ListPipeline> listPipeline = new List<ListPipeline>();
            var pipelineRespone = _pipelineService.GetPipeline();
            List<PipelineModel> pipeline = new List<PipelineModel>();
            List<long> listId = new List<long>();
            if (!string.IsNullOrEmpty(pipelineRespone))
            {
                var result= JsonConvert.DeserializeObject<HrmResultModel<PipelineModel>>(pipelineRespone);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    pipeline = result.Results;
                    listId = pipeline.Select(x => x.Id).Distinct().ToList();
                }
                
            }
            if (listId.Count()>0)
            {
                listId.Sort();
                for (int i = 0; i < listId.Count(); i++)
                {
                    listPipeline.Add(new ListPipeline()
                    {
                        ListPipelineModel = pipeline.Where(x => x.Id == listId[i]).ToList()
                    });
                }
            }                      
            PipelineViewModel pipelineView = new PipelineViewModel();
            pipelineView.PipelineView = listPipeline;            
            return View(pipelineView);
        }
        public ActionResult ShowFormAddPipeline(long pipelineId)
        {
            AddPipelineViewModel addPipeline = new AddPipelineViewModel();
            addPipeline.Pipeline = new PipelineModel()
            {
            };
            var param = new BasicParamModel()
            {
                FilterField = string.Empty,
                PageNumber = 1,
                PageSize = int.MaxValue,
                LanguageId = CurrentUser.LanguageId,
                RoleId = 1,
                UserId = CurrentUser.UserId,
                DbName = CurrentUser.DbName
            };
            
            List<PipelineStepModel> pipelineSteps = new List<PipelineStepModel>();
            PipelineModel pipeline = new PipelineModel();
            if (pipelineId!=0)
            {
                var pipelineStepResponse = this._pipelineService.GetPipelineStepByPipelineId(pipelineId);
                if (!string.IsNullOrEmpty(pipelineStepResponse))
                {
                    var result= JsonConvert.DeserializeObject<HrmResultModel<PipelineStepModel>>(pipelineStepResponse);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        pipelineSteps = result.Results;
                    }
                    
                }
                var pipelineResponse = this._pipelineService.GetPipelineById(pipelineId);
                if (!string.IsNullOrEmpty(pipelineResponse))
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<PipelineModel>>(pipelineResponse);
                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        pipeline = result.Results.FirstOrDefault();

                    }
                }
                
            }

            if (pipelineSteps.Count()==0)
            {
                List<MasterDataModel> masterDatas = new List<MasterDataModel>();
                var responseMasterData = this._masterDataService.GetAllMasterDataByName(MasterGroup.PipelinePosition, _languageId);
                if (responseMasterData != null)
                {
                    var resultMasterData = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(responseMasterData);
                    if (!CheckPermission(resultMasterData))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        masterDatas = resultMasterData.Results;
                    }
                }
                foreach (var item in masterDatas)
                {
                    pipelineSteps.Add(new PipelineStepModel()
                    {
                        PositionId= item.Id,
                        PositionName= item.Name
                    });
                }
            }
            addPipeline.Pipeline = pipeline;
            addPipeline.PipelineStep = pipelineSteps;
            if (addPipeline.Menu == null)
            {
                var menu_vm = new Hrm.Framework.ViewModels.MenuViewModel();
                menu_vm.Menus = new List<MenuModel>();
                var response = _menuService.GetMenu();
                if (response != null)
                {
                    var result = JsonConvert.DeserializeObject<HrmResultModel<MenuModel>>(response);

                    if (!CheckPermission(result))
                    {
                        //return to Access Denied
                    }
                    else
                    {
                        menu_vm.Menus = result.Results;
                        addPipeline.Menu = JsonConvert.DeserializeObject<List<dynamic>>(JsonConvert.SerializeObject(menu_vm.Menus));
                    }
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Pipeline", "_AddPipeline.cshtml"), addPipeline);

        }
        public ActionResult AddStage(List<PipelineStepModel> PipelineStep)
        {
            PipelineStep =  PipelineStep.OrderBy(x=>x.OrderNo).ToList();
            List<MasterDataModel> masterData = new List<MasterDataModel>();
            var resultMasterData = this._masterDataService.GetAllMasterDataByName(MasterGroup.PipelinePosition, _languageId);
            if (!string.IsNullOrEmpty(resultMasterData))
            {
                var result = JsonConvert.DeserializeObject<HrmResultModel<MasterDataModel>>(resultMasterData);
                if (!CheckPermission(result))
                {
                    //return to Access Denied
                }
                else
                {
                    masterData = result.Results;
                }
            }
            var masterDataNomal = masterData.FirstOrDefault(s => s.Name == MasterGroup.PositionName);
            var listPipelineStep = PipelineStep.Where(s => s.PositionName == MasterGroup.PositionName).ToList();

            listPipelineStep.Add(new PipelineStepModel()
            {
                PositionId = masterDataNomal.Id,
                PositionName = masterDataNomal.Name
            });
            foreach (var item in PipelineStep)
            {
                if (item.PositionName!= MasterGroup.PositionName)
                {
                    listPipelineStep.Add(item);
                }
                if (item.ListStringPipelineRule != null && item.ListStringPipelineRule.Length>0)
                {
                    item.PipelineRule = String.Join(",", item.ListStringPipelineRule.ToArray());
                }
            }
            return PartialView(UrlHelpers.TemplateAdmin("Pipeline", "_AddPipelineContent.cshtml"), listPipelineStep);
        }
        public ActionResult removePipelineStep(int index, List<PipelineStepModel> PipelineStep)
        {
            PipelineStep.RemoveAt(index);
            return PartialView(UrlHelpers.TemplateAdmin("Pipeline", "_AddPipelineContent.cshtml"), PipelineStep);
        }
        public ActionResult SavePipeline(AddPipelineViewModel data)
        {
            if (data.Pipeline != null)
            {
                var validations = ValidationHelper.Validation(data.Pipeline, "Pipeline");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            data.Pipeline.CreatedBy = CurrentUser.UserId;
            data.Pipeline.UpdatedBy = CurrentUser.UserId;
            foreach (var item in data.PipelineStep)
            {
                if (item.ListStringPipelineRule != null)
                {
                    item.PipelineRule = string.Join(",", item.ListStringPipelineRule);
                }
            }
            if (data.Pipeline != null)
            {
                var validations = ValidationHelper.Validation(data, "data");
                if (validations.Count > 0)
                {
                    return Json(new { Result = validations, Invalid = true }, JsonRequestBehavior.AllowGet);
                }
            }
            bool isSuccess = false;
            var responeseResources = string.Empty;
            var pipelineEntity = MapperHelper.Map<PipelineModel, PipelineEntity>(data.Pipeline);
            var pipelineStepTity = MapperHelper.MapList<PipelineStepModel, PipelineStepType>(data.PipelineStep);
            var result = _pipelineService.SavePipeline(pipelineEntity, pipelineStepTity);
            if (result != null)
            {
                var response = JsonConvert.DeserializeObject<HrmResultModel<bool>>(result);
                if (!CheckPermission(response))
                {
                    //return to Access Denied
                }
                else
                {
                    if (response.Success == true)
                    {
                        List<ListPipeline> listPipeline = new List<ListPipeline>();
                        var pipelineRespone = _pipelineService.GetPipeline();
                        List<PipelineModel> pipeline = new List<PipelineModel>();
                        List<long> listId = new List<long>();

                        if (pipelineRespone != null)
                        {
                            var resultPipeline = JsonConvert.DeserializeObject<HrmResultModel<PipelineModel>>(pipelineRespone);
                            if (!CheckPermission(resultPipeline))
                            {
                                //return to Access Denied
                            }
                            else
                            {
                                pipeline = resultPipeline.Results;
                                listId = pipeline.Select(x => x.Id).Distinct().ToList();
                            }

                        }
                        if (listId.Count() > 0)
                        {
                            listId.Sort();
                            for (int i = 0; i < listId.Count(); i++)
                            {
                                listPipeline.Add(new ListPipeline()
                                {
                                    ListPipelineModel = pipeline.Where(x => x.Id == listId[i]).ToList()
                                });
                            }
                        }
                        PipelineViewModel pipelineView = new PipelineViewModel();
                        pipelineView.PipelineView = listPipeline;
                        return PartialView(UrlHelpers.TemplateAdmin("Pipeline", "_Pipeline.cshtml"), pipelineView);
                    }
                    else
                    {
                        if (data.Pipeline.Id != 0)
                        {
                            responeseResources = _localizationService.GetResources("Message.Update.UnSuccessful");
                        }
                        else
                        {
                            responeseResources = _localizationService.GetResources("Message.Create.UnSuccessfu");
                        }
                    }
                }
            }

            return Json(new { isSuccess, responeseResources }, JsonRequestBehavior.AllowGet);
        }      
    }
}
