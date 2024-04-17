using IntegratedSystems.Domain.Domain_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Interface
{
    public interface IVactinationCenterService
    {
        public List<VaccinationCenter> GetCenters();
        public VaccinationCenter GetCenterById(Guid? id);
        public VaccinationCenter CreateNewCenter(VaccinationCenter center);
        public VaccinationCenter UpdateCenter(VaccinationCenter center);
        public VaccinationCenter DeleteCenter(Guid id);
        public Boolean AddVacinatedPatient(VacinatePatientDTO model);

    }
}
