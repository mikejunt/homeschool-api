using System;
using System.Collections.Generic;

namespace homeschool_api.Models
{
    public partial class Users
    {
        public Users()
        {
            Family = new HashSet<Family>();
            TasksAssignee = new HashSet<Tasks>();
            TasksAuthor = new HashSet<Tasks>();
            UserToFamily = new HashSet<UserToFamily>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public bool Minor { get; set; }
        public string ParentEmail { get; set; }

        public virtual ICollection<Family> Family { get; set; }
        public virtual ICollection<Tasks> TasksAssignee { get; set; }
        public virtual ICollection<Tasks> TasksAuthor { get; set; }
        public virtual ICollection<UserToFamily> UserToFamily { get; set; }
    }

    public partial class FamilyUserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }
        public int FamilyId { get; set; }
        public int Role { get; set; }
        public bool Confirmed { get; set; }
    }
    
}

