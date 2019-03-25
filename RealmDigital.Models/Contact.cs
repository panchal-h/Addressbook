using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigital.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("Contacts")]
    public class Contact
    {
        [Key]
        public int ID { get; set; }

        public int UserID { get; set; }

        [Display(Name = "First Name")]
        [Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        public string LastName { get; set; }

        [Display(Name = "Mobile No")]
        [NotMapped]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string MobileNo { get; set; }

        [NotMapped]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        public List<ContactMobile> ContactMobiles { get; set; }

        public List<ContactEmail> ContactEmails { get; set; }

    }
}
