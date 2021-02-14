using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBDExam.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APBDExam.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClinicController : ControllerBase
    {

        private readonly IDbService _dbService;

        public ClinicController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [Route("api/medicaments")]
        [HttpGet("{IdMedicament}")]
        IActionResult getMedicamentData([FromRoute]int IdMedicament)
        {
            try
            {
                var res = _dbService.getMedicamentData(IdMedicament);
                if (res != null) return Ok(res);
            }
            catch(ClinicException e)
            {
                return BadRequest(e.Message);
            }
            return BadRequest();
        }

        [Route("api/patients")]
        IActionResult   deletePatientFromDb([FromRoute] int IdPatient)
        {
            var rowsAffected = _dbService.deletePatientFromDb(IdPatient);
            if (rowsAffected == 0) return NotFound($"Pacjenta o podanym id: {IdPatient} nie ma w bazie");

            return Ok("Usuwanie zakonczone");
        }





    }
}
