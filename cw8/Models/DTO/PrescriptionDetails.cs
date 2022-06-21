

using cw8.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cw8_Code_First.Models.DTO
{
    public class PrescriptionDetails
    {
        [Required]
        public PatientDetails Patient { get; set; }

        [Required]
        public DoctorDetails Doctor { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        [Required]
        public IEnumerable<MedicamentDetails> Medicaments { get; set; }
    }
}
