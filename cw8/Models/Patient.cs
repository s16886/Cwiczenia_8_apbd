﻿using System;
using System.Collections.Generic;

namespace cw8.Models
{
    public class Patient
    {
        public int IdPatient { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }

        public virtual ICollection<Prescription> Prescriptions { get; set; }
    }
}
