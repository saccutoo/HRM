using System.Web.Mvc;
using Hrm.Framework.Models;
using System;
using Hrm.Service;
using Hrm.Common;
using Newtonsoft.Json;
using System.Linq;
using Hrm.Framework.Controllers;

namespace Hrm.Web.Controllers
{
    public class AttachmentController : BaseController
    {
        private IAttachmentService _attachmentService;
        public AttachmentController(IAttachmentService attachmentService)
        {
            this._attachmentService = attachmentService;
        }

        public ActionResult DownloadFileById(int id)
        {
            var responseDcument = _attachmentService.GetAttackmentById(id, DataType.Document);
            if (responseDcument != null)
            {
                var resultDocument = JsonConvert.DeserializeObject<HrmResultModel<AttachmentModel>>(responseDcument);
                if (!CheckPermission(resultDocument))
                {
                    //return to Access Denied
                }
                else
                {
                    if (resultDocument.Results.Count > 0)
                    {
                        var name = resultDocument.Results.FirstOrDefault().FileName;
                        var path = System.Configuration.ConfigurationManager.AppSettings["UploadFolder"] + "\\" + name;
                        if (!string.IsNullOrEmpty(name))
                        {
                            var extension = name.Substring(name.LastIndexOf('.'), name.Count() - name.LastIndexOf('.'));
                            byte[] fileBytes;
                            var fileName = resultDocument.Results.FirstOrDefault().DisplayFileName;
                            try
                            {
                                fileBytes = System.IO.File.ReadAllBytes(path);
                            }
                            catch
                            {
                                fileBytes = new byte[0];
                            }
                            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName + extension);
                        }
                    }

                }
            }
            return File(new byte[0], "dimage/png", "ResultFile.jpeg");
        }
    }
}