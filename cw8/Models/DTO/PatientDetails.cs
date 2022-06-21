using System;
using System.ComponentModel.DataAnnotations;

namespace cw8_Code_First.Models.DTO
{
    public class PatientDetails
    {
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Birthdate is required.")]
        public DateTime Birthdate  { get; set; }
    }
}
