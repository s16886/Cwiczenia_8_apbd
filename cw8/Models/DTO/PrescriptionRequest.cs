using cw8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw8_Code_First.Models.DTO
{
    public class PrescriptionRequest
    {
        /*[Required(ErrorMessage = "FirstName is required.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Date is required.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "DueDate is required.")]
        public DateTime DueDate { get; set; }
        [Required(ErrorMessage = "Doctor is required.")]
        public DoctorData Doctor { get; set; }

        public IEnumerable<Medicament>Medicaments { get; set; }*/

        [Required]
        public int IdPatient { get; set; }

        [Required]
        public int IdDoctor { get; set; }
        public int IdPrescription { get; set; }

    }
}
