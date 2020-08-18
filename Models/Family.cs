using System;
using System.Collections.Generic;

namespace homeschool_api.Models
{
    public partial class Family
    {
        public Family()
        {
            Tasks = new HashSet<Tasks>();
            UserToFamily = new HashSet<UserToFamily>();
        }

        public int Id { get; set; }
        public int AdminId { get; set; }
        public string Name { get; set; }

        public virtual Users Admin { get; set; }
        public virtual ICollection<Tasks> Tasks { get; set; }
        public virtual ICollection<UserToFamily> UserToFamily { get; set; }
    }

    public partial class FamilyMembership
    {
        public int FamilyId { get; set; }
        public int AdminId { get; set; }
        public string Name { get; set; }
        public int RelationId { get; set; }
        public int UserId { get; set; }
        public int Role { get; set; }
        public bool Confirmed { get; set; }

    }

}
