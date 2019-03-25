using RealmDigital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RealmDigital.Services
{
    public class ContactsBL
    {

        /// <summary>
        /// Get Contacts User wise
        /// </summary>
        /// <param name="userId">The userid.</param>        
        /// <returns>List of Contacts</returns>
        public List<Contact> GetContactsByUserId(int userId)
        {
            using (var service = new ServiceContext())
            {
                return service.Search<Contact>("[userId] = " + userId);
            }
        }

        /// <summary>
        /// Get Contact by Id
        /// </summary>
        /// <param name="id">id.</param>        
        /// <returns>Contact</returns>
        public Contact GetContactByID(int id)
        {
            
            using (var service = new ServiceContext())
            {
                return service.SelectObject<Contact>(id);
            }
        }

        /// <summary>
        /// Get Contact Mobiles by ContactId
        /// </summary>
        /// <param name="id">contact id.</param>        
        /// <returns>Contact Mobiles</returns>
        public List<ContactMobile> GetContactMobilesByContactID(int id)
        {
            using (var service = new ServiceContext())
            {
                return service.Search<ContactMobile>("[ContactID] = " + id);
            }
        }

        /// <summary>
        /// Get Contact Emails by ContactId
        /// </summary>
        /// <param name="id">contact id.</param>        
        /// <returns>Contact Emails</returns>
        public List<ContactEmail> GetContactEmailsByContactID(int id)
        {
            using (var service = new ServiceContext())
            {
                return service.Search<ContactEmail>("[ContactID] = " + id);
            }
        }

        /// <summary>
        /// Get Contact Mobile by Id
        /// </summary>
        /// <param name="id">id.</param>        
        /// <returns>Contact Mobile</returns>
        public ContactMobile GetContactMobileByID(int? id)
        {
            using (var service = new ServiceContext())
            {
                return service.SelectObject<ContactMobile>(id.Value);
            }
        }

        /// <summary>
        /// Delete Contact
        /// </summary>
        /// <param name="contactId">contactId.</param>        
        /// <returns></returns>
        public void DeleteContactByContactId(int contactId)
        {
            using (var service = new ServiceContext())
            {
                service.DeleteList<ContactMobile>("[ContactID] = " + contactId);
                service.DeleteList<ContactEmail>("[ContactID] = " + contactId);
                service.Delete<Contact>(contactId);

            }
        }

        /// <summary>
        /// Save Contact Mobile
        /// </summary>
        /// <param name="mobile">contact mobile.</param>        
        /// <returns></returns>
        public int SaveContactMobile(ContactMobile mobile)
        {
            using (var service = new ServiceContext())
            {
                return service.Save<ContactMobile>(mobile, true, false, new string[] { "MobileNo" });
            }
        }

        /// <summary>
        /// Get Contact Email by Id
        /// </summary>
        /// <param name="id">id.</param>        
        /// <returns>Contact Email</returns>
        public ContactEmail GetContactEmailByID(int? id)
        {
            using (var service = new ServiceContext())
            {
                return service.SelectObject<ContactEmail>(id.Value);
            }
        }

        /// <summary>
        /// Save Contact Email
        /// </summary>
        /// <param name="email">contact email.</param>        
        /// <returns></returns>
        public int SaveContactEmail(ContactEmail email)
        {
            using (var service = new ServiceContext())
            {
                return service.Save<ContactEmail>(email, true, false, new string[] { "EmailAddress" });
            }
        }

        /// <summary>
        /// Delete Contact Mobile by Id
        /// </summary>
        /// <param name="id">id.</param>        
        /// <returns></returns>
        public void DeleteContactMobile(int? id)
        {
            using (var service = new ServiceContext())
            {
                service.Delete<ContactMobile>(id.Value);
            }
        }

        /// <summary>
        /// Delete Contact Email by Id
        /// </summary>
        /// <param name="id">id.</param>        
        /// <returns></returns>
        public void DeleteContactEmail(int? id)
        {
            using (var service = new ServiceContext())
            {
                service.Delete<ContactEmail>(id.Value);
            }
        }

        /// <summary>
        /// Save Contact
        /// </summary>
        /// <param name="contact">id.</param>        
        /// <returns></returns>
        public int SaveContact(Contact contact)
        {
            using (var service = new ServiceContext())
            {
                return service.Save<Contact>(contact);
            }
        }
    }
}
