using cw8.Models;
using cw8_Code_First.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw8_Code_First.Services
{
    public interface IDbService
    {
        public Task<IActionResult> GetDoctorsAsync();
        public Task<IActionResult> AddDoctorAsync(DoctorDetails doctor);
        public Task<IActionResult> ModifyDoctorAsync(DoctorRequest request);
        public Task<IActionResult> DeleteDoctorAsync(int idDoctor);
        public Task<IActionResult> GetPrescriptionAsync(int idPrescription);
    }
}
