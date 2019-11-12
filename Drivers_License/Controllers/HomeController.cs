using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Drivers_License.Domain;
using Drivers_License.Models;

namespace Drivers_License.Controllers
{
    public class HomeController : Controller
    {
        PersonLicenseModel personLicenseModel = new PersonLicenseModel();
        ViewPersonLicenseModel viewPersonLicenseModel = new ViewPersonLicenseModel();
        public ActionResult Index()
        {
            GetlicenseTypes();
            return View();
        }

        public ActionResult GetlicenseTypes()
        {
            dbDriversLicenseEntities entities = new dbDriversLicenseEntities();
            personLicenseModel.Drp_LicenceTypes = (from p in entities.tblLicenseTypes.AsEnumerable()
                                                   select new SelectListItem
                                                   {
                                                       Text = p.LicenseTypeName,
                                                       Value = p.Id.ToString()
                                                   }).ToList<SelectListItem>();
            return View("Index", personLicenseModel);
        }

        #region Person
     /// <summary>
     /// This method is used for add person license detail
     /// </summary>
     /// <param name="model"></param>
     /// <param name="form"></param>
     /// <returns></returns>
        [HttpPost]
        public ActionResult AddPersonLicenseDetail(PersonLicenseModel model, FormCollection form)
        {
            int id = 0;
            if (ModelState.IsValid)
            {
                dbDriversLicenseEntities entities = new dbDriversLicenseEntities();
                tblPersonDetail tblPersonDetail = new tblPersonDetail();
                if (model != null)
                {
                    int count = entities.tblPersonDetails.Where(a => a.PhoneNumber == model.PhoneNumber).Count();
                    if(count >0)
                    {
                        TempData["Error"] = "Phone Number Already Exist, Try Another!";
                        return RedirectToAction("Index", "Home");
                    }
                    int countLic = entities.tblDriversLicenseDetails.Where(a => a.License_number == model.LicenseNumber).Count();
                    if (countLic > 0)
                    {
                        TempData["Error"] = "License Number Already Exist, Try Another!";
                        return RedirectToAction("Index", "Home");
                    }

                    tblPersonDetail.Address = model.Address.Trim();
                    tblPersonDetail.CreatedDate = DateTime.UtcNow;
                    tblPersonDetail.FirstName = model.FirstName.Trim();
                    tblPersonDetail.LastName = model.LastName.Trim();
                    tblPersonDetail.LastUpdatedDate = DateTime.UtcNow;
                    tblPersonDetail.PhoneNumber = model.PhoneNumber.Trim();
                    entities.tblPersonDetails.Add(tblPersonDetail);
                    entities.SaveChanges();
                    id = tblPersonDetail.Id;
                    if (id > 0)
                    {
                        int Lid = 0;
                        tblDriversLicenseDetail tblDriversLicenseDetail = new tblDriversLicenseDetail();
                        tblDriversLicenseDetail.Issue_date = Convert.ToDateTime(model.IssueDate);
                        tblDriversLicenseDetail.Expiry_date = Convert.ToDateTime(model.ExpiryDate);
                        tblDriversLicenseDetail.License_number = model.LicenseNumber.Trim();
                        tblDriversLicenseDetail.PersonId = id;
                        tblDriversLicenseDetail.LicenseTypeId = Convert.ToInt32(form["LicenseTypeName"]);
                        entities.tblDriversLicenseDetails.Add(tblDriversLicenseDetail);
                        entities.SaveChanges();
                        Lid = tblDriversLicenseDetail.Id;
                        if (Lid > 0)
                        {
                            return RedirectToAction("ViewLicenseDetails", "Home");
                        }
                    }
                }



            }
            else
            {
                GetlicenseTypes();
            }
            return View("Index", personLicenseModel);
        }


        public ActionResult ViewLicenseDetails()
        {
            GetPersonsDetail();
            return View();
        }
        /// <summary>
        /// This method is used when we display all records of persons
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPersonsDetail()
        {
            dbDriversLicenseEntities entities = new dbDriversLicenseEntities();
            List<PersonLicenseModel> licenses = new List<PersonLicenseModel>();
            var query = from p in entities.tblPersonDetails
                        join d in entities.tblDriversLicenseDetails
                        on p.Id equals d.PersonId
                        join l in entities.tblLicenseTypes
                        on d.LicenseTypeId equals l.Id
                        select new PersonLicenseModel
                        {
                            FirstName = p.FirstName,
                            LastName = p.LastName,
                            Address = p.Address,
                            PhoneNumber = p.PhoneNumber,
                            LicenseNumber = d.License_number,
                            LicenseType = l.LicenseTypeName,
                            PersonId = p.Id,

                        };

            licenses = query.OrderByDescending(a=>a.PersonId).ToList();
            if (licenses != null && licenses.Count > 0)
            {
                viewPersonLicenseModel.lst = licenses;
            }
            return View("ViewLicenseDetails", viewPersonLicenseModel);
        }

        #endregion

        #region Edit
        /// <summary>
        /// This method is used to update person license detail
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="model"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(int ID, PersonLicenseModel model, FormCollection form)
        {
            using (dbDriversLicenseEntities entities = new dbDriversLicenseEntities())
            {

                int count = entities.tblPersonDetails.Where(a => a.PhoneNumber == model.PhoneNumber && a.Id != model.PersonId).Count();
                if (count > 0)
                {
                    TempData["Error"] = "Phone Number Already Exist, Try Another!";
                    return RedirectToAction("Index", "Home");
                }
                int countLic = entities.tblDriversLicenseDetails.Where(a => a.License_number == model.LicenseNumber && a.PersonId != model.PersonId).Count();
                if (countLic > 0)
                {
                    TempData["Error"] = "License Number Already Exist, Try Another!";
                    return RedirectToAction("Index", "Home");
                }


                tblPersonDetail updatedPerson = (from c in entities.tblPersonDetails
                                                 where c.Id == ID
                                                 select c).FirstOrDefault();
                if (updatedPerson != null)
                {
                    updatedPerson.FirstName = model.FirstName;
                    updatedPerson.LastName = model.LastName;
                    updatedPerson.Address = model.Address;
                    updatedPerson.PhoneNumber = model.PhoneNumber;
                    updatedPerson.LastUpdatedDate = DateTime.UtcNow;
                    entities.SaveChanges();
                    tblDriversLicenseDetail data = (from c in entities.tblDriversLicenseDetails
                                                    where c.PersonId == ID
                                                    select c).FirstOrDefault();

                    if (data != null)
                    {
                        data.License_number = model.LicenseNumber;
                        data.Issue_date = Convert.ToDateTime(model.IssueDate);
                        data.Expiry_date = Convert.ToDateTime(model.ExpiryDate);
                        data.LicenseTypeId = Convert.ToInt32(form["LicenseTypeName"]);
                    }
                    entities.SaveChanges();
                }

            }

            return RedirectToAction("ViewLicenseDetails");

        }
        /// <summary>
        /// This Method is used for get person license detail by person Id
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int ID)
        {
            PersonLicenseModel pModel = new PersonLicenseModel();
            using (dbDriversLicenseEntities entities = new dbDriversLicenseEntities())
            {

                var query = (from p in entities.tblPersonDetails
                             join d in entities.tblDriversLicenseDetails
                             on p.Id equals d.PersonId
                             join l in entities.tblLicenseTypes
                             on d.LicenseTypeId equals l.Id
                             where p.Id == ID
                             select new PersonLicenseModel
                             {
                                 FirstName = p.FirstName,
                                 LastName = p.LastName,
                                 Address = p.Address,
                                 PhoneNumber = p.PhoneNumber,
                                 LicenseNumber = d.License_number,
                                 LicenseTypeId = d.LicenseTypeId,
                                 IssueDate = d.Issue_date.ToString(),
                                 ExpiryDate = d.Expiry_date.ToString()

                             }).FirstOrDefault();

                if (query != null)
                {

                    var licenseTypes = (from p in entities.tblLicenseTypes.AsEnumerable()
                                        select new SelectListItem
                                        {
                                            Text = p.LicenseTypeName,
                                            Value = p.Id.ToString()

                                        }).ToList<SelectListItem>();
                    string licenseTypeId = Convert.ToString(query.LicenseTypeId);



                    var selectedlicenseType = licenseTypes.FirstOrDefault(p => p.Value == licenseTypeId);
                    selectedlicenseType.Selected = true;

                    pModel.FirstName = query.FirstName;
                    pModel.LastName = query.LastName;
                    pModel.Address = query.Address;
                    if(!string.IsNullOrEmpty(query.IssueDate))
                    {
                        DateTime issuedate = Convert.ToDateTime(query.IssueDate);
                        pModel.IssueDate = issuedate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    }
                    if(!string.IsNullOrEmpty(query.ExpiryDate))
                    {
                        DateTime expirydate = Convert.ToDateTime(query.ExpiryDate);
                        pModel.ExpiryDate = expirydate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);
                    }
                    pModel.PhoneNumber = query.PhoneNumber;
                    pModel.LicenseTypeId = query.LicenseTypeId;
                    pModel.Drp_LicenceTypes = licenseTypes;
                    pModel.LicenseNumber = query.LicenseNumber;
                    pModel.PersonId = ID;
                }
                else
                {
                    return HttpNotFound();

                }
            }
            return View(pModel);
        }
        #endregion

        #region Delete
        /// <summary>
        /// This method is used for delete person record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Delete(int? id)
        {

            using (dbDriversLicenseEntities entities = new dbDriversLicenseEntities())
            {

                var query = (from p in entities.tblPersonDetails
                             join d in entities.tblDriversLicenseDetails
                             on p.Id equals d.PersonId
                             join l in entities.tblLicenseTypes
                             on d.LicenseTypeId equals l.Id
                             where p.Id == id
                             select new PersonLicenseModel
                             {
                                 FirstName = p.FirstName,
                                 LastName = p.LastName,
                                 Address = p.Address,
                                 PhoneNumber = p.PhoneNumber,
                                 LicenseNumber = d.License_number,
                                 LicenseType = l.LicenseTypeName,
                                 PersonId = p.Id
                             }).FirstOrDefault();
                if (query == null)
                {
                    return HttpNotFound();
                }
                return View(query);
            }


        }
        /// <summary>
        /// This method is used when we display the data before delete record
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            using (dbDriversLicenseEntities entities = new dbDriversLicenseEntities())
            {

                tblDriversLicenseDetail data = entities.tblDriversLicenseDetails.Where(a => a.PersonId == id).FirstOrDefault();
                if(data != null)
                {
                    entities.tblDriversLicenseDetails.Remove(data);
                    entities.SaveChanges();
                }

                tblPersonDetail tblPersonDetail = entities.tblPersonDetails.Where(a => a.Id == id).FirstOrDefault();
                if (tblPersonDetail != null)
                {
                    entities.tblPersonDetails.Remove(tblPersonDetail);
                    entities.SaveChanges();

                    return RedirectToAction("ViewLicenseDetails");
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }


        #endregion
    }
}