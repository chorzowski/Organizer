using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjektTest2.Models
{
    public class ApplicationUsers : IdentityUser
    {
        public Task Task { get; set; }
    }
    public class Task 
    {
        public int TaskId { get; set; }

        [DataType(DataType.Date)]
        public DateTime AddTime { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FinishDay { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh-mm}}", ApplyFormatInEditMode = true)]
        public DateTime FinishTime { get; set; }

        public string TaskContent { get; set; }
        public int DaysToFinishTask => (FinishDay - DateTime.Now).Days + 1;
        public string ApplicationUsers_ID { get; set; }
        public string ApplicationUsers_Email { get; set; }
        public ApplicationUsers ApplicationUsers { get; set; }
    }
}
