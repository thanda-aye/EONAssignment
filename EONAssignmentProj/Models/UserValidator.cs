using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EONAssignmentProj.Models
{
    public class UserValidator
    {
        [ScaffoldColumn(false)]
        public int UserID { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Please enter Name"), MaxLength(30)]
        [Display(Name = "Name")]
        //[Required]
        //[MaxLength(30)]
        //[Display(Name = "Name")]
        public string Name { get; set; } = string.Empty;

        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Please enter Email Address"), MaxLength(50)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }
        public string Gender { get; set; }

        [Required(ErrorMessage = "Allowed date is between 1st jan 2023 to 30 june 2023")]
        [Display(Name = "Date Registered")]
        [Range(typeof(DateTime), "1st Jan 2023", "30 June 2023")]
        //[Required]
        //[Display(Name = "Registered Date")]
        public string RegDate { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "At minimum, 1 day is to be checked.")]
        //[Required]        
        [Display(Name = "Selected Days")]
        public string SelectedDays { get; set; }
        public string AreaOfInterest { get; set; }

        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Limit text to a maximum 100 characters"), MaxLength(100)]
        [Display(Name = "Additional Request")]
        //[Required]
        //[MaxLength(100)]
        //[Display(Name = "Selected Days")]
        public string AddRequest { get; set; } = string.Empty;
    }

    [ModelMetadataType(typeof(UserValidator))]
    public partial class UserTbl
    {
    }
}
