using Microsoft.AspNetCore.Http;
using PersonsDirectoryApp.Data.Models;
using PersonsDirectoryApp.Web.Models;
using PersonsDirectoryApp.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PersonsDirectoryApp.Web.Helpers
{
    public static class PersonHelper
    {
        private static string _defaultImageName = "NoPhoto.jpg";
        public static Person ConvertToDbModel(PersonViewModel vModel)
        {
            return new Person()
            {
                Id = vModel.Id > 0 ? vModel.Id : 0,
                FirstName = vModel.FirstName,
                LastName = vModel.LastName,
                Gender = vModel.Gender,
                PersonalNo = vModel.PersonalNo,
                BirthDate = vModel.BirthDate,
                CityId = vModel.CityId,
                ImageUrl = String.IsNullOrEmpty(vModel.ImageUrl) ? _defaultImageName : vModel.ImageUrl
            };
        }

        public static void MapToDbModel(Person model, PersonViewModel vModel)
        {

            model.Id = vModel.Id > 0 ? vModel.Id : 0;
            model.FirstName = vModel.FirstName;
            model.LastName = vModel.LastName;
            model.Gender = vModel.Gender;
            model.PersonalNo = vModel.PersonalNo;
            model.BirthDate = vModel.BirthDate;
            model.CityId = vModel.CityId;
            model.ImageUrl = String.IsNullOrEmpty(vModel.ImageUrl) ? _defaultImageName : vModel.ImageUrl;
        }

        public static PersonViewModel ConvertToViewModel(Person model)
        {
            return new PersonViewModel()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                PersonalNo = model.PersonalNo,
                BirthDate = model.BirthDate,
                CityId = model.CityId,
                ImageUrl = model.ImageUrl,
                City = model.City,
                TelephoneNumbers = model.TelephoneNumbers
                //PersonRelationMaps = person.PersonOneRelationMaps.Union(person.PersonTwoRelationMaps).ToList()
            };
        }

        public static PersonRelationMap ConvertToDbModel(PersonRelationMapViewModel vModel)
        {
            return new PersonRelationMap()
            {
                Id = vModel.Id > 0 ? vModel.Id : 0,
                PersonOneId = vModel.PersonOneId,
                PersonTwoId = vModel.PersonTwoId,
                RelationType = vModel.RelationType
            };
        }

        public static PersonRelationMap ConvertToViewModel(PersonRelationMap model)
        {
            return new PersonRelationMap()
            {
                Id = model.Id,
                PersonOneId = model.PersonOneId,
                PersonTwoId = model.PersonTwoId,
                RelationType = model.RelationType
            };
        }

        public static TelephoneNumber ConvertToDbModel(TelephoneNumberViewModel vModel)
        {
            return new TelephoneNumber()
            {
                Id = vModel.Id > 0 ? vModel.Id : 0,
                PersonId = vModel.PersonId,
                TelephoneType = vModel.TelephoneType,
                Number = vModel.Number
            };
        }

        public static TelephoneNumber ConvertToViewModel(TelephoneNumber model)
        {
            return new TelephoneNumber()
            {
                Id = model.Id,
                PersonId = model.PersonId,
                TelephoneType = model.TelephoneType,
                Number = model.Number
            };
        }

        public static string UploadPhoto(IFormFile photo, string photoDirectory)
        {
            var uniqueFileName = $"{Guid.NewGuid().ToString()}_{photo.FileName}";
            string photoPath = Path.Combine(photoDirectory, uniqueFileName);
            photo.CopyTo(new FileStream(photoPath, FileMode.Create));

            return uniqueFileName;
        }

        public static List<PersonViewModel> FilterPersons(List<Person> persons, PersonsSearchModel sModel)
        {
            if (!string.IsNullOrEmpty(sModel.FirstName))
            {
                persons = persons.Where(p => p.FirstName.Equals(sModel.FirstName)).ToList();
            }
            if (!string.IsNullOrEmpty(sModel.LastName))
            {
                persons = persons.Where(p => p.LastName.Equals(sModel.LastName)).ToList();
            }
            if (!string.IsNullOrEmpty(sModel.PersonalNo))
            {
                persons = persons.Where(p => p.PersonalNo.Equals(sModel.PersonalNo)).ToList();
            }
            if (sModel.Gender.HasValue)
            {
                persons = persons.Where(p => p.Gender.Equals(sModel.Gender)).ToList();
            }
            if (sModel.BirthDate.HasValue)
            {
                persons = persons.Where(p => p.BirthDate.Equals(sModel.BirthDate)).ToList();
            }
            if (sModel.CityId.HasValue)
            {
                persons = persons.Where(p => p.CityId.Equals(sModel.CityId)).ToList();
            }
            if (sModel.HasPhoto.HasValue)
            {
                persons = persons.Where(p => p.ImageUrl.Equals(_defaultImageName)).ToList();
            }

            var personViewModels = new List<PersonViewModel>();
            foreach (var person in persons.OrderByDescending(p => p.Id))
            {
                personViewModels.Add(PersonHelper.ConvertToViewModel(person));
            }

            return personViewModels;
        }
    }
}
