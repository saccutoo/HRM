using System;

namespace Hrm.Framework.Models
{
    public class StaffRelationShipsModel : BaseModel
    {
        public long StaffId { get; set; }
        public long RelationshipId { get; set; }
        public string Name { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        public long Status { get; set; }
        public bool IsEmergency { get; set; }
        public bool Deduction { get; set; }
        public string DeductionCode { get; set; }
        public DateTime? DeductionFrom { get; set; }
        public DateTime? DeductionTo { get; set; }
        public string Note { get; set; }
        public long OrderNo { get; set; }
    }
}
