using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL
{
    public class ReportParameters
    {
        public string Username { get; set; }
        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Enter start date.")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DisplayName("Finish Date")]
        [Required(ErrorMessage = "Enter finish date.")]
        [DataType(DataType.Date)]
        public DateTime FinishDate { get; set; }
    }
}
