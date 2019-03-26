using RealmDigital.Infrastructure;
using RealmDigital.Models;
using RealmDigital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace RealmDigital.AddressBook.Controllers
{
    public class ContactController : Controller
    {
        ContactsBL _contactBL;

        public ContactController() => _contactBL = new ContactsBL();

        /// <summary>
        /// List of all Contacts User wise
        /// </summary>
        /// <param name="search">The search parameter.</param>
        /// <returns>action result</returns>
        public ActionResult Index(string search)
        {
            if (ProjectSession.UserID == 0)
            {
                return RedirectToAction("index", "home");
            }
            var contacts = _contactBL.GetContactsByUserId(ProjectSession.UserID);
            if (contacts != null)
            {
                contacts = contacts.OrderBy(s => s.FirstName).ToList();
            }
            if (!String.IsNullOrEmpty(search))
            {
                contacts = contacts.Where(s => s.FirstName.ToLower().Contains(search.ToLower()) || s.LastName.ToLower().Contains(search.ToLower())).ToList();
            }
            return View(contacts);
        }

        /// <summary>
        /// Contact Details
        /// </summary>
        /// <param name="id">contact id.</param>
        /// <returns>action result</returns>
        [ActionName("manage-contact")]
        public ActionResult ManageContact(int id = 0)
        {
            ViewBag.ContactId = id;
            Contact model = null;
            if (id > 0)
            {
                model = _contactBL.GetContactByID(id);
            }

            if (model == null)
            {
                model = new Contact();
            }

            return View("ManageContact", model);
        }

        /// <summary>
        /// To Save Contact Details
        /// </summary>
        /// <param name="contact">contact</param>
        /// <returns>action result</returns>
        [HttpPost]
        [ActionName("manage-contact")]
        public ActionResult ManageContact(Contact contact)
        {
            contact.UserID = ProjectSession.UserID;
            var contactID = _contactBL.SaveContact(contact);
            if (contactID > 0)
            {
                if (!string.IsNullOrEmpty(contact.MobileNo))
                {
                    var contactMobile = new ContactMobile();
                    contactMobile.ContactID = contactID;
                    contactMobile.MobileNo = contact.MobileNo;
                    _contactBL.SaveContactMobile(contactMobile);
                }

                if (!string.IsNullOrEmpty(contact.EmailAddress))
                {
                    var contactEmail = new ContactEmail();
                    contactEmail.ContactID = contactID;
                    contactEmail.EmailAddress = contact.EmailAddress;
                    _contactBL.SaveContactEmail(contactEmail);
                }
                if (contact.ID == 0)
                {
                    return RedirectToAction("manage-contact", new { id = contactID });
                }
                else
                {
                    return RedirectToAction("index");
                }
            }
            return View("ManageContact", contact);
        }

        /// <summary>
        /// To Confirm Details while delete
        /// </summary>
        /// <param name="contactId">contactId</param>
        /// <returns>action result</returns>
        public ActionResult DeleteContact(int contactId)
        {


            Contact contact = _contactBL.GetContactByID(contactId);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteContact", contact);
        }

        /// <summary>
        /// Deleted contact
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>action result</returns>
        [HttpPost]
        public ActionResult DeleteContact([Bind(Include = "ID")]Contact model)
        {
            _contactBL.DeleteContactByContactId(model.ID);
            return Json(new { success = true });
        }


        #region Contact Mobile

        /// <summary>
        /// List of Contact Mobile
        /// </summary>
        /// <param name="contactID">contactID</param>
        /// <returns>action result</returns>
        public ActionResult ContactMobiles(int contactID)
        {
            var contactMobiles = _contactBL.GetContactMobilesByContactID(contactID);
            return PartialView("_ContactMobiles", contactMobiles);
        }

        /// <summary>
        /// Add Contact Mobile
        /// </summary>
        /// <param name="contactID">contactID</param>
        /// <returns>action result</returns>
        public ActionResult AddContactMobile(int contactID)
        {
            var contactMobile = new ContactMobile();
            contactMobile.ContactID = contactID;
            return PartialView("_AddContactMobile", contactMobile);
        }

        /// <summary>
        /// Save Contact Mobile
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>action result</returns>
        [HttpPost]
        public ActionResult AddContactMobile([Bind(Include = "ID,ContactID,MobileNo")]ContactMobile model)
        {
            if (ModelState.IsValid)
            {
                var returnValue = _contactBL.SaveContactMobile(model);
                if (returnValue == -1)
                {
                    ModelState.AddModelError("Duplicate", "Duplicate Mobile No");
                    return PartialView("_AddContactMobile", model);
                }
                else
                {
                    return Json(new { success = true });
                }

            }

            return PartialView("_AddContactMobile", model);
        }

        /// <summary>
        /// Edit Contact Mobile
        /// </summary>
        /// <param name="id">contactID</param>
        /// <returns>action result</returns>
        public ActionResult EditContactMobile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContactMobile mobile = _contactBL.GetContactMobileByID(id);
            if (mobile == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddContactMobile", mobile);
        }

        /// <summary>
        /// Update Contact Mobile
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>action result</returns>
        [HttpPost]
        public ActionResult EditContactMobile([Bind(Include = "ID,ContactID,MobileNo")]ContactMobile model)
        {
            if (ModelState.IsValid)
            {
                var returnValue = _contactBL.SaveContactMobile(model);
                if (returnValue == -1)
                {
                    ModelState.AddModelError("Duplicate", "Duplicate Mobile No");
                    return PartialView("_AddContactMobile", model);
                }
                else
                {
                    return Json(new { success = true });
                }
            }

            return PartialView("_AddContactMobile", model);
        }

        /// <summary>
        /// To Confirm contact mobile while delete
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        public ActionResult DeleteContactMobile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContactMobile mobile = _contactBL.GetContactMobileByID(id);
            if (mobile == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteContactMobile", mobile);
        }

        /// <summary>
        /// Delete Contact Mobile
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpPost, ActionName("DeleteContactMobile")]
        public ActionResult DeleteConfirmContactMobile(int? id)
        {
            _contactBL.DeleteContactMobile(id);
            return Json(new { success = true });
        }


        #endregion

        #region Contact Email

        /// <summary>
        /// List of Contact Email
        /// </summary>
        /// <param name="contactID">contactID</param>
        /// <returns>action result</returns>
        public ActionResult ContactEmails(int contactID)
        {
            var contactEmails = _contactBL.GetContactEmailsByContactID(contactID);
            if (contactEmails != null)
            {
                contactEmails = contactEmails.OrderBy(m => m.EmailAddress).ToList();
            }
            return PartialView("_ContactEmails", contactEmails);
        }

        /// <summary>
        /// Add Contact Email
        /// </summary>
        /// <param name="contactID">contactID</param>
        /// <returns>action result</returns>
        public ActionResult AddContactEmail(int contactID)
        {
            var contactEmail = new ContactEmail();
            contactEmail.ContactID = contactID;
            return PartialView("_AddContactEmail", contactEmail);
        }

        /// <summary>
        /// Save Contact Email
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>action result</returns>
        [HttpPost]
        public ActionResult AddContactEmail([Bind(Include = "ID,ContactID,EmailAddress")]ContactEmail model)
        {
            if (ModelState.IsValid)
            {
                var returnValue = _contactBL.SaveContactEmail(model);
                if (returnValue == -1)
                {
                    ModelState.AddModelError("Duplicate", "Duplicate Email");
                    return PartialView("_AddContactEmail", model);
                }
                else
                {
                    return Json(new { success = true });
                }
            }
            return PartialView("_AddContactEmail", model);
        }

        /// <summary>
        /// Edit Contact Email
        /// </summary>
        /// <param name="id">contactID</param>
        /// <returns>action result</returns>
        public ActionResult EditContactEmail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContactEmail mobile = _contactBL.GetContactEmailByID(id);
            if (mobile == null)
            {
                return HttpNotFound();
            }
            return PartialView("_AddContactEmail", mobile);
        }

        /// <summary>
        /// Update Contact Email
        /// </summary>
        /// <param name="model">model</param>
        /// <returns>action result</returns>
        [HttpPost]
        public ActionResult EditContactEmail([Bind(Include = "ID,ContactID,EmailAddress")]ContactEmail model)
        {
            if (ModelState.IsValid)
            {
                var returnValue = _contactBL.SaveContactEmail(model);
                if (returnValue == -1)
                {
                    ModelState.AddModelError("Duplicate", "Duplicate Email");
                    return PartialView("_AddContactEmail", model);
                }
                else
                {
                    return Json(new { success = true });
                }
            }
            return PartialView("_AddContactEmail", model);
        }

        /// <summary>
        /// To Confirm contact Email while delete
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        public ActionResult DeleteContactEmail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ContactEmail mobile = _contactBL.GetContactEmailByID(id);
            if (mobile == null)
            {
                return HttpNotFound();
            }
            return PartialView("_DeleteContactEmail", mobile);
        }

        /// <summary>
        /// Delete Contact Email
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>action result</returns>
        [HttpPost, ActionName("DeleteContactEmail")]
        public ActionResult DeleteConfirmContactEmail(int? id)
        {
            _contactBL.DeleteContactEmail(id);
            return Json(new { success = true });
        }
        #endregion

    }
}
