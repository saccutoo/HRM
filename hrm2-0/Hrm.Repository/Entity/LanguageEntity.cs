using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class LanguageEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string LanguageCulture { get; set; }
        public string ImageName { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsDeleted { get; set; }

    }
}
