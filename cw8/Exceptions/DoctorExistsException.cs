using System;

namespace cw8_Code_First.Exceptions
{
    public class DoctorExistsException : Exception
    {
        public DoctorExistsException() : base("Doctor already exists.") { }
    }
}
