using Hrm.Framework.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hrm.Framework.Helper
{
   public static class UploadFileHelper
    {
        public static List<AttachmentJs> GetFileUpload(IEnumerable<HttpPostedFileBase> attachment)
        {
            var result = new List<AttachmentJs>();
            // The Name of the Upload component is "files"
            if (attachment != null)
            {
                foreach (var file in attachment)
                {
                    //check if file exist and rename
                    var dir = System.IO.Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["UploadFolder"]);
                    string newFileName = file.FileName;
                    var path = Path.Combine(dir.FullName, newFileName);
                    var index = 0;
                    {
                        string fName = Path.GetFileNameWithoutExtension(file.FileName);
                        string fExt = Path.GetExtension(file.FileName);
                        newFileName = String.Concat(fName, string.Format("_{0}", index), fExt);
                        path = Path.Combine(dir.FullName, newFileName);
                        index++;
                    }
                    file.SaveAs(path);
                    result.Add(new AttachmentJs()
                    {
                        Id = 0,
                        Realname = newFileName,
                        Name = file.FileName,
                        Extension = Path.GetExtension(file.FileName),
                        Size = file.ContentLength,
                        CreatedDate = DateTime.Now.ToString("yyyy-MM-dd"),
                    });
                }
            }
            return result;
        }

        public static List<AttachmentJs> GetImageUpload(IEnumerable<HttpPostedFileBase> attachment)
        {
            var result = new List<AttachmentJs>();
            // The Name of the Upload component is "files"
            if (attachment != null)
            {
                foreach (var file in attachment)
                {
                    //check if file exist and rename
                    var dir = System.IO.Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["UploadFolder"]);
                    string fExt = Path.GetExtension(file.FileName);
                    string fileName =(Guid.NewGuid() + fExt).ToString();
                    var path = Path.Combine(dir.FullName, fileName);
                    file.SaveAs(path);
                    result.Add(new AttachmentJs()
                    {
                        Realname = fileName
                    });
                }
            }
            return result;
        }
        public static List<AttachmentJs> SaveFile(IEnumerable<HttpPostedFileBase> attachment)
        {
            var result = new List<AttachmentJs>();
            // The Name of the Upload component is "files"
            if (attachment != null)
            {
                foreach (var file in attachment)
                {
                    //check if file exist and rename
                    var dir = System.IO.Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings["UploadFolder"]);
                    string fExt = Path.GetExtension(file.FileName);
                    string fileName = (Guid.NewGuid() + fExt).ToString();
                    var path = Path.Combine(dir.FullName, fileName);
                    file.SaveAs(path);
                    result.Add(new AttachmentJs()
                    {
                        Realname = fileName,
                        Name = file.FileName,
                        Size = file.ContentLength,
                        Extension = fExt
                    });
                }
            }
            return result;
        }
        public static bool RemoveFileUpload(string realName)
        {
            bool isSuccess = false;
            if (!string.IsNullOrEmpty(realName))
            {
                var dir = System.Configuration.ConfigurationManager.AppSettings["UploadFolder"].Replace("\\", "/");
                var path = Path.Combine(dir, realName);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                    isSuccess = true;
                }
            }
            return isSuccess;
        }
    }
}
