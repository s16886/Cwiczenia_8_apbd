using cw8.Models;
using cw8_Code_First.Models.DTO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace cw8_Code_First.Services
{
    public class DbService : IDbService
    {
        private readonly CodeFirstContext _dbContext;

        public DbService(CodeFirstContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> GetDoctorsAsync()
        {
            return new OkObjectResult(await _dbContext.Doctors
                .Select(e => new DoctorDetails
                {
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email
                }).ToListAsync());
        }
        public async Task<IActionResult> AddDoctorAsync(DoctorDetails doctor)
        {
            await _dbContext.Doctors.AddAsync(new Doctor()
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Email = doctor.Email
            });
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult($"Doctor {doctor.FirstName} {doctor.LastName} added.");
        }
        public async Task<IActionResult> ModifyDoctorAsync(DoctorRequest request)
        {
            var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == request.IdDoctor);
            if(doctor == null) return new BadRequestObjectResult($"Doctor does not exist");
            doctor.FirstName = request.FirstName;
            doctor.LastName = request.LastName;
            doctor.Email = request.Email;
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult($"Doctor modified.");
        }
        public async Task<IActionResult> DeleteDoctorAsync(int idDoctor)
        {
            var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == idDoctor);
            if (doctor == null) return new BadRequestObjectResult($"Doctor does not exist");
            _dbContext.Doctors.Remove(doctor);
            await _dbContext.SaveChangesAsync();
            return new OkObjectResult($"Doctor {idDoctor} removed.");
        }

        public async Task<IActionResult> GetPrescriptionAsync(int idPrescription)
        {
            var prescription = await _dbContext.Prescriptions
                .Where(p => p.IdPrescription == idPrescription)
                .SingleOrDefaultAsync();

            var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == prescription.IdDoctor);
            var patient = await _dbContext.Patients.SingleOrDefaultAsync(d => d.IdPatient == prescription.IdPatient);
            /*var doctor = await _dbContext.Doctors.SingleOrDefaultAsync(d => d.IdDoctor == request.IdDoctor);
            if(doctor == null) return new BadRequestObjectResult($"Doctor  ID:{request.IdDoctor} not exists.");

            var patient = await _dbContext.Patients.SingleOrDefaultAsync(d => d.IdPatient == request.IdPatient);
            if (patient == null) return new BadRequestObjectResult($"Patient ID:{request.IdPatient} not exists.");

            var prescription = await _dbContext.Prescriptions.SingleOrDefaultAsync(d => d.IdPrescription == request.IdPrescription);
            if (prescription == null) return new BadRequestObjectResult($"Prescription ID:{request.IdPrescription} not exists.");*/



            var PatientMedicaments = await _dbContext.Prescription_Medicaments
                .Where(m => m.IdPrescription == prescription.IdPrescription)
                .ToListAsync();
            List<MedicamentDetails> medList = new List<MedicamentDetails>();
            foreach(Prescription_Medicament pm in PatientMedicaments)
            {
                var med = await _dbContext.Medicaments
                    .Where(m => m.IdMedicament == pm.IdMedicament)
                    .SingleOrDefaultAsync();
                medList.Add(new MedicamentDetails
                {
                    Name = med.Name
                });
            }


            var medicaments = await _dbContext.Prescriptions
                .Where(p => p.IdPrescription == idPrescription)
                .Select(p => new PrescriptionDetails
                {
                   Patient = new PatientDetails
                   {
                       FirstName = patient.FirstName,
                       LastName = patient.LastName,
                       Birthdate =patient.Birthdate
                   },
                   Doctor = new DoctorDetails
                   {
                       FirstName = doctor.FirstName,
                       LastName= doctor.LastName,
                       Email = doctor.Email
                   },
                   Date = prescription.Date,
                   DueDate = prescription.DueDate, 
                   Medicaments = medList
                }).ToListAsync();


            return new OkObjectResult(medicaments);
        }
    }
}
