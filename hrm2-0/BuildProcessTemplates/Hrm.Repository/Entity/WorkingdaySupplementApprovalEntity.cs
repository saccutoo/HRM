﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hrm.Repository.Entity
{
    public class WorkingdaySupplementApprovalEntity : BaseEntity
    {
        public long RequestId { get; set; }
        public long RequestStatusId { get; set; }
        public long RequestActionId { get; set; }
        public string Note { get; set; }
        public string ApprovalByName { get; set; }
        public DateTime ApprovedDate { get; set; }
    }
}