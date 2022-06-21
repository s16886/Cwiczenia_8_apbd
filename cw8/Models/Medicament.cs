using System.Collections.Generic;

namespace cw8.Models
{
    public class Medicament
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Prescription_Medicament> Prescriptions_Medicaments { get; set; }
    }
}
