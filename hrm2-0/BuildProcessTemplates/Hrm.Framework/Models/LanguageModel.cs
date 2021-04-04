using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hrm.Framework.Models
{
    public class LanguageModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string ImageName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }
    }
}