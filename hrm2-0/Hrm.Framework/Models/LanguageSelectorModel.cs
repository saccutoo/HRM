using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Framework.Models
{
    public class LanguageSelectorModel : BaseModel
    {
        public List<dynamic> Languages { get; set; }
        public long CurrentLanguageId { get; set; }
        public bool UseImages { get; set; }
    }
}
