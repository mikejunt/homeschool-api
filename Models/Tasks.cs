using System;
using System.Collections.Generic;

namespace homeschool_api.Models
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public int AssigneeId { get; set; }
        public int FamilyId { get; set; }
        public string Description { get; set; }
        public bool Completed { get; set; }
        public DateTime? Completetime { get; set; }
        public DateTime? Duetime { get; set; }
        public string Photo { get; set; }

        public virtual Users Assignee { get; set; }
        public virtual Users Author { get; set; }
        public virtual Family Family { get; set; }
    }
}
