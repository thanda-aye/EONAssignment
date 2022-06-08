using System;
using System.Collections.Generic;

#nullable disable

namespace EONAssignmentProj.Models
{
    public partial class UserTbl
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string DateReg { get; set; }
        public string SelectedDays { get; set; }
        public string AreaOfInterest { get; set; }
        public string AddRequest { get; set; }
    }
}
