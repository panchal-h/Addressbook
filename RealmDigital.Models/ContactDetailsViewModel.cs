using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealmDigital.Models
{
    public class ContactDetailsViewModel
    {
        public ContactDetailsViewModel()
        {
            Contact = new Contact();
            ContactMobiles = new List<ContactMobile>();
            ContactEmails = new List<ContactEmail>();
        }
        public Contact Contact { get; set; }

        public List<ContactMobile> ContactMobiles { get; set; }

        public List<ContactEmail> ContactEmails { get; set; }
    }
}
