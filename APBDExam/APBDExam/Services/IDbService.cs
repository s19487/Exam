using APBDExam.DTOs.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDExam.Services
{
    public interface IDbService
    {
        public MedicamentPrescriptionsResponse getMedicamentData(int IdMedicament);
        public int deletePatientFromDb(int IdPatient);


    }
}
