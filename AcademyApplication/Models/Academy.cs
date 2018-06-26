using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AcademyApplication.Entity;

namespace AcademyApplication.Models
{
    public class Academy
    {
        public string AddContactUsValues(ContactFormValues contactForm)
        {
            string isContactAdded = string.Empty;
            try
            {
                using (var academyEntity = new ATCACADEMYEntities())
                {
                    Contactu contactus = new Contactu();
                    contactus.ContactName = contactForm.Name;
                    contactus.ContactEmail = contactForm.Email;
                    contactus.ContactPhone = contactForm.Phone;
                    contactus.ContactInformation = contactForm.Info;
                    academyEntity.Contactus.Add(contactus);
                    academyEntity.SaveChanges();
                    isContactAdded = "saved";
                }
            }
            catch (Exception ex)
            {
                isContactAdded = null;
            }
            return isContactAdded;
        }
    }
    public class ContactFormValues
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Info { get; set; }
    }
}