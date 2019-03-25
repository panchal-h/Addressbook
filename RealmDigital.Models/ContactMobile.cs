using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigital.Models
{
    [System.ComponentModel.DataAnnotations.Schema.Table("ContactMobiles")]
    public class ContactMobile
    {
        [Key]
        public int ID { get; set; }

        public int ContactID { get; set; }

        [Display(Name = "Mobile No")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        [Required]
        public string MobileNo { get; set; }

    }
}
