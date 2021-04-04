using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class LocalizationEntity
    {
        public long Id { get; set; }
        public long LanguageId { get; set; }
        public string ResourceName { get; set; }
        public string ResourceValue { get; set; }
    }
}
