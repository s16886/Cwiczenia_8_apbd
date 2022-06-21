using System.ComponentModel.DataAnnotations;

namespace cw8_Code_First.Models.DTO
{
    public class DoctorDetails
    {
        [Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }
    }
}
