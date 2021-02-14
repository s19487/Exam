using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APBDExam.DTOs.Responses
{
    public class MedicamentPrescriptionsResponse
    {
        public int IdMedicament { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public ICollection<int> Prescriptions { get; set; }
        public DateTime Date { get; set; }

    }
}
