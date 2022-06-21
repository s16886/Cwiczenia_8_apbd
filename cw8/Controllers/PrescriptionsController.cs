using cw8_Code_First.Models.DTO;
using cw8_Code_First.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace cw8_Code_First.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IDbService _dbService;
        public PrescriptionsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPrescription(int idPrescription)
        {
            return await _dbService.GetPrescriptionAsync(idPrescription);
        }
    }
}
