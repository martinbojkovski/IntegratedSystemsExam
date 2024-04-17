using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository.Interface;
using IntegratedSystems.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegratedSystems.Service.Implementation
{
    public class VacinationCenterService : IVactinationCenterService
    {
        private readonly IRepository<VaccinationCenter> _centerRepository;
        private readonly IRepository<Vaccine> _vacineRepository;
        private readonly IRepository<Patient> _patientRepository;

        public VacinationCenterService(IRepository<VaccinationCenter> centerRepository, IRepository<Vaccine> vacineRepository, IRepository<Patient> patientRepository)
        {
            _centerRepository = centerRepository;
            _vacineRepository = vacineRepository;
            _patientRepository = patientRepository;
        }

        public bool AddVacinatedPatient(VacinatePatientDTO model)
        {
            Vaccine vaccine = new Vaccine();
            vaccine.Manufacturer = model.manufacturer;
            vaccine.PatientId = (Guid)model.patientId;
            vaccine.PatientFor = _patientRepository.Get(model.patientId);
            vaccine.Certificate = Guid.NewGuid();
            vaccine.VaccinationCenter = (Guid)model.VacinationCenterId;
            vaccine.DateTaken = (DateTime)model.TimeTaken;
            vaccine.Center = _centerRepository.Get(model.VacinationCenterId);
            if (_centerRepository.Get(model.VacinationCenterId).MaxCapacity >= 1)
            {
                _vacineRepository.Insert(vaccine);
                var center = _centerRepository.Get(model.VacinationCenterId);
                center.MaxCapacity -= 1;
                _centerRepository.Update(center);
                return true;
            }
            return false;
        }

        public VaccinationCenter CreateNewCenter(VaccinationCenter center)
        {
            return this._centerRepository.Insert(center);
        }

        public VaccinationCenter DeleteCenter(Guid id)
        {
            var center = _centerRepository.Get(id);
            return this._centerRepository.Delete(center);
        }

        public VaccinationCenter GetCenterById(Guid? id)
        {
            return _centerRepository.Get(id);   
        }

        public List<VaccinationCenter> GetCenters()
        {
            return _centerRepository.GetAll().ToList();
        }

        public VaccinationCenter UpdateCenter(VaccinationCenter center)
        {
            return _centerRepository.Update(center);
        }
    }
}
