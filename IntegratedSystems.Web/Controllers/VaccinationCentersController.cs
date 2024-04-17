using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IntegratedSystems.Domain.Domain_Models;
using IntegratedSystems.Repository;
using IntegratedSystems.Service.Implementation;
using IntegratedSystems.Service.Interface;

namespace IntegratedSystems.Web.Controllers
{
    public class VaccinationCentersController : Controller
    {
        private readonly IVactinationCenterService _vacinationCenterService;
        private readonly IPatientService _patientService;

        public VaccinationCentersController(IVactinationCenterService vacinationCenterService, IPatientService patientService)
        {
            _vacinationCenterService = vacinationCenterService;
            _patientService = patientService;
        }

        public IActionResult VacinatePatient(Guid id)
        {
            VacinatePatientDTO model = new VacinatePatientDTO
            {
                VacinationCenterId = id,
                patients = _patientService.GetPatients(),
                Manufacturers = new List<string>()
                {
                    "phizer", "astra zeneca", "sputnik"
                }
            };


            return View(model);
        }

        [HttpPost]
        public IActionResult VacinatePatient(VacinatePatientDTO model)
        {
            var result = _vacinationCenterService.AddVacinatedPatient(model);
            if (result)
            {
                return RedirectToAction("Index", "VaccinationCenters");
            }
            else {
                return RedirectToAction("ErrorMessage", "VaccinationCenters");
            }
            
        }

        public IActionResult ErrorMessage()
        {
            return View();
        }


        // GET: VaccinationCenters
        public IActionResult Index()
        {
            return View(_vacinationCenterService.GetCenters());
        }

        // GET: VaccinationCenters/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vacinationCenterService.GetCenterById(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VaccinationCenters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (ModelState.IsValid)
            {
                vaccinationCenter.Id = Guid.NewGuid();
                _vacinationCenterService.CreateNewCenter(vaccinationCenter);
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vacinationCenterService.GetCenterById(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }
            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Name,Address,MaxCapacity,Id")] VaccinationCenter vaccinationCenter)
        {
            if (id != vaccinationCenter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _vacinationCenterService.UpdateCenter(vaccinationCenter);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VaccinationCenterExists(vaccinationCenter.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vaccinationCenter);
        }

        // GET: VaccinationCenters/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vaccinationCenter = _vacinationCenterService.GetCenterById(id);
            if (vaccinationCenter == null)
            {
                return NotFound();
            }

            return View(vaccinationCenter);
        }

        // POST: VaccinationCenters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var vaccinationCenter = _vacinationCenterService.GetCenterById(id);
            if (vaccinationCenter != null)
            {
                _vacinationCenterService.DeleteCenter(id);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool VaccinationCenterExists(Guid id)
        {
            return _vacinationCenterService.GetCenterById(id) != null;
        }
    }
}
