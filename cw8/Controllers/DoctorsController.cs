using cw8_Code_First.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using cw8_Code_First.Models.DTO;
using cw8_Code_First.Exceptions;
using cw8.Models;

namespace cw8_Code_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public DoctorsController(IDbService dbService)
        {
            _dbService = dbService;
        }
        [HttpGet]
        public async Task<IActionResult> GetDoctorsAsync()
        {
            return await _dbService.GetDoctorsAsync();
            
        }
        [HttpPost("AddDoctor")]
        public async Task<IActionResult> AddDoctorAsync(DoctorDetails newDoctor)
        {
            return await _dbService.AddDoctorAsync(newDoctor);
        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDoctor(int id)
        {
            return await _dbService.DeleteDoctorAsync(id);
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> ModifyDoctor(DoctorRequest request)
        {
            return await _dbService.ModifyDoctorAsync(request);
        }
    }
}
