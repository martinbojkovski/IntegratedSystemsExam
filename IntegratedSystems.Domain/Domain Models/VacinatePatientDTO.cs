using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Domain.Domain_Models
{
    public class VacinatePatientDTO
    {
        public Guid? VacinationCenterId { get; set; }
        public List<string>? Manufacturers { get; set; }
        public string? manufacturer { get; set; }
        public List<Patient>? patients { get; set; }
        public Guid? patientId { get; set; }
        public DateTime? TimeTaken { get; set; }
    }
}
