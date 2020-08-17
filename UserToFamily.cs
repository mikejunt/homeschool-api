using System;
using System.Collections.Generic;

namespace homeschool_api.Models
{
    public partial class UserToFamily
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FamilyId { get; set; }
        public int Role { get; set; }
        public bool Confirmed { get; set; }

        public virtual Family Family { get; set; }
        public virtual Users User { get; set; }
    }
}
