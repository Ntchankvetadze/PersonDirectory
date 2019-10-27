using System;
using System.Dynamic;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PersonsDirectoryApp.Repos;
using PersonsDirectoryApp.Repos.Infrastructure;
using PersonsDirectoryApp.Web.Models;
using PersonsDirectoryApp.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using PersonsDirectoryApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using PersonsDirectoryApp.Web.ViewModels;
using PersonsDirectoryApp.Web.Helpers;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc.Rendering;
using PersonsDirectoryApp.Web.Common;

namespace PersonsDirectoryApp.Web.Controllers
{
    public class PersonController : Controller
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IHostingEnvironment _hostingEnvironment;
        private UnitOfWork _uow;

        public PersonController(ILogger<PersonController> logger
                                , IUnitOfWork uow
                                ,IHostingEnvironment hostingEnvironment)
        {
            this._logger = logger;
            this._hostingEnvironment = hostingEnvironment;
            this._uow = uow as UnitOfWork;
        }

        public IActionResult Index(
            string currentFilter,
            string searchString,
            int? pageNumber)
        {

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var persons = new List<Person>();
            if (!String.IsNullOrEmpty(searchString))
            {
                persons = _uow.Persons.FindPersons(searchString).ToList();
            }
            else
            {
                persons = _uow.Persons.GetAll().ToList();
            }

            var personViewModels = new List<PersonViewModel>();

            foreach (var person in persons.OrderByDescending(p => p.Id))
            {
                personViewModels.Add(PersonHelper.ConvertToViewModel(person));
            }

            int pageSize = 10;

            return View(PaginatedList<PersonViewModel>.GetPage(personViewModels, pageNumber ?? 1, pageSize));
        }

        public IActionResult PersonFilter(PersonsSearchModel sModel)
        {
            ViewData["Cities"] = new SelectList(_uow.Cities.GetAll(), "Id", "Name");
            var persons = _uow.Persons.GetAll().ToList();
            sModel.GetPage(PersonHelper.FilterPersons(persons, sModel));

            return View(sModel);
        }

        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return NotFound();
            }

            var person = _uow.Persons.GetPersonWithDetales(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            var vModel = PersonHelper.ConvertToViewModel(person);
            vModel.PersonRelationMaps = _uow.PersonRelationMaps.GetPersonRelationMapByPerson(id.Value).ToList();

            return View(vModel);
        }

        public IActionResult Create()
        {
            var cities = _uow.Cities.GetAll();
            ViewData["Cities"] = new SelectList(cities, "Id", "Name");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModel]
        public IActionResult Create([Bind("Id,FirstName,LastName,Gender,PersonalNo,BirthDate,CityId,Photo")] PersonViewModel vModel)
        {
            try
            {
                var cities = _uow.Cities.GetAll();
                ViewData["Cities"] = new SelectList(cities, "Id", "Name");

                if (ModelState.IsValid)
                {
                    if (_uow.Persons.CheckPersonalNo(vModel.PersonalNo, 0))
                    {
                        ModelState.AddModelError("PersonalNo", $"Personal No:{vModel.PersonalNo} already exists");
                        return View(vModel);
                    }

                    if (vModel.Photo != null)
                    {
                        string photoDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                        vModel.ImageUrl = PersonHelper.UploadPhoto(vModel.Photo, photoDirectory);
                    }

                    var person = PersonHelper.ConvertToDbModel(vModel);
                    _uow.Persons.Add(person);
                    _uow.Complete();

                    return RedirectToAction(nameof(Index));
                }
                return View(vModel);
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = _uow.Persons.GetPersonWithDetales(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            var vModel = PersonHelper.ConvertToViewModel(person);
            
            ViewData["Cities"] = new SelectList(_uow.Cities.GetAll(), "Id", "Name");
            ViewData["TelephoneNumbers"] = _uow.TelephoneNumbers.GetTelephoneNumbersByPerson(id.Value).ToList();
            ViewData["PersonRelationMaps"] = _uow.PersonRelationMaps.GetPersonRelationMapByPerson(id.Value).ToList();

            return View(vModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateModel]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName,Gender,PersonalNo,BirthDate,CityId,ImageUrl,Photo")] PersonViewModel vModel)
        {
            try
            {
                ViewData["Cities"] = new SelectList(_uow.Cities.GetAll(), "Id", "Name");
                ViewData["TelephoneNumbers"] = _uow.TelephoneNumbers.GetTelephoneNumbersByPerson(id).ToList();
                ViewData["PersonRelationMaps"] = _uow.PersonRelationMaps.GetPersonRelationMapByPerson(id).ToList();

                if (id != vModel.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        var person = _uow.Persons.GetPersonByPersonalNo(vModel.PersonalNo);
                        if (person.Id != id)
                        {
                            ModelState.AddModelError("PersonalNo", $"Personal No:{vModel.PersonalNo} already exists");
                            return View(vModel);
                        }

                        if (vModel.Photo != null)
                        {
                            string photoDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                            vModel.ImageUrl = PersonHelper.UploadPhoto(vModel.Photo, photoDirectory);
                        }

                        PersonHelper.MapToDbModel(person, vModel);

                        _uow.Complete();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PersonExists(vModel.Id))
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
                return View(vModel);
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: Directory/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var person = _uow.Persons.Get(id.Value);
            if (person == null)
            {
                return NotFound();
            }

            var vModel = PersonHelper.ConvertToViewModel(person);

            return View(vModel);
        }

        // POST: Directory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var person = _uow.Persons.GetPersonWithDetales(id);
            var relations = _uow.PersonRelationMaps.GetPersonRelationMapByPerson(id);
            var telNumbers = _uow.TelephoneNumbers.GetTelephoneNumbersByPerson(id);

            _uow.PersonRelationMaps.RemoveRange(relations);
            _uow.TelephoneNumbers.RemoveRange(telNumbers);
            _uow.Persons.Remove(person);
            _uow.Complete();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult AddRelationMap([Bind("RelationType,PersonOneId,NewRelativePersonalNo")] PersonRelationMapViewModel vModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var relatedPerson = _uow.Persons.GetPersonByPersonalNo(vModel.NewRelativePersonalNo);
                    if (relatedPerson == null)
                    {
                        return Json(new { success = false, message = "Personal ID was not found" });
                    }
                    else if (relatedPerson.Id.Equals(vModel.PersonOneId))
                    {
                        return Json(new { success = false, message = "Related person must be different" });
                    }

                    vModel.PersonTwoId = relatedPerson.Id;
                    var model = PersonHelper.ConvertToDbModel(vModel);
                    _uow.PersonRelationMaps.Add(model);
                    _uow.Complete();

                    return Json(new { success = true, newId = model.Id });
                }
                else
                {
                    return Json(new { success = false, message = ModelState.Values.First().Errors.First().ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred while processing your request" });
            }
        }

        [HttpPost]
        public IActionResult DeleteRelationMap(int id)
        {
            try
            {
                var relation = _uow.PersonRelationMaps.Get(id);
                if (relation == null)
                {
                    return Json(new { success = false, message = "Relation Map was not found" });
                }

                _uow.PersonRelationMaps.Remove(relation);
                _uow.Complete();

                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error occurred while processing your request" });
            }
        }

        [HttpPost]
        public ActionResult AddTelephoneNumber([Bind("PersonId,TelephoneType,Number")] TelephoneNumberViewModel vModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var model = PersonHelper.ConvertToDbModel(vModel);
                    _uow.TelephoneNumbers.Add(model);
                    _uow.Complete();
                    
                    return Json(new { success = true, newId = model.Id });
                }
                else
                {
                    return Json(new { success = false, message = ModelState.Values.First().Errors.First().ErrorMessage });
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error occurred while processing your request" });
            }
        }

        [HttpPost]
        public ActionResult DeleteTelephoneNumber(int id)
        {
            try
            {
                var telephone = _uow.TelephoneNumbers.Get(id);
                if (telephone == null)
                {
                    return Json(new { success = false });
                }

                _uow.TelephoneNumbers.Remove(telephone);
                _uow.Complete();

                return Json(new { success = true });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "Error occurred while processing your request" });
            }
        }

        public IActionResult Report()
        {
            var relations = _uow.PersonRelationMaps.GetAll();
            var persons = _uow.Persons.GetAll();
            var vModel = new List<ReportViewModel>();

            foreach (var person in persons)
            {
                if (relations.Any(r => r.PersonOneId.Equals(person.Id) || r.PersonTwoId.Equals(person.Id)))
                {
                    var reporRow = new ReportViewModel()
                    {
                        PersonId = person.Id,
                        FirstName = person.FirstName,
                        LastName = person.LastName,
                        PersonalNo = person.PersonalNo,
                        RelativeCount = relations.Count(r => (r.PersonOneId.Equals(person.Id) || r.PersonTwoId.Equals(person.Id)) && r.RelationType == Enums.RelationType.Relative),
                        AcquaintanceCount = relations.Count(r => (r.PersonOneId.Equals(person.Id) || r.PersonTwoId.Equals(person.Id)) && r.RelationType == Enums.RelationType.Acquaintance),
                        ColleagueCount = relations.Count(r => (r.PersonOneId.Equals(person.Id) || r.PersonTwoId.Equals(person.Id)) && r.RelationType == Enums.RelationType.Colleague),
                        OtherCount = relations.Count(r => (r.PersonOneId.Equals(person.Id) || r.PersonTwoId.Equals(person.Id)) && r.RelationType == Enums.RelationType.Other)
                    };

                    vModel.Add(reporRow);
                }
            }

            return View(vModel);
        }

        private bool PersonExists(int id)
        {
            return _uow.Persons.Get(id) != null;
        }

    }
}