using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigital.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("ContactEmails")]
    public class ContactEmail
    {
        [Key]
        public int ID { get; set; }

        public int ContactID { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Email Required")]
        public string EmailAddress { get; set; }

    }
}
