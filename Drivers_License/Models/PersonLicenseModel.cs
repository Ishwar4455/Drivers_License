using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Drivers_License.Models
{
    public class PersonLicenseModel
    {
        [Required(ErrorMessage = "Enter First Name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Enter Last Name")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Enter Your Address")]
        [Display(Name = "Address")]
        [UIHint("MultilineText")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Enter Your Phone Number")]
        [Display(Name = "Phone Number")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Only Numbers allowed.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter Your License Number")]
        [Display(Name = "License Number")]
        public string LicenseNumber { get; set; }
        [Required(ErrorMessage = "Enter License Issue Date")]
        [Display(Name = "Issue Date")]
        public string IssueDate { get; set; }
        [Required(ErrorMessage = "Enter License Expiry Date")]
        [Display(Name = "Expiry Date")]
        public string ExpiryDate { get; set; }
        [Display(Name = "LicenseTypeId")]
        [Required(ErrorMessage = "{0} is required.")]
        public int LicenseTypeId { get; set; }
        [Key]
        public int? PersonId { get; set; }

        public string LicenseType { get; set; }

        public IList<SelectListItem> Drp_LicenceTypes { get; set; }
    }
    public class ViewPersonLicenseModel
    {
        public List<PersonLicenseModel> lst { get; set; }
    }
}