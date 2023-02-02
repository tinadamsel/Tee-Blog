using Core.Models;
using Core.ViewModels;
using Logic.IHelper;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Helper
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IEmailService emailService;
        private readonly IEmailConfiguration _emailConfiguration;

        public EmailHelper(IEmailService emailService, IEmailConfiguration emailConfiguration)
        {
            //_emailService = emailService;
            this.emailService = emailService;
           _emailConfiguration = emailConfiguration;
        }

        public bool SendVerificationEmail(ApplicationUser user, string linkToClick)
        {
            if (user != null && linkToClick != null)
            {
                string toEmail = user.Email;
                string subject = "Tee Blog"; 

                var message = "Dear " + user.FirstName + "," + "</br> " +
                 "This is to notify you, that you have been registered successfully with Tee Blog." + "<br/>" +
                 "Kindly click on the button to verify your email <br/> <br/>" +
                 "<button> <a href='" + linkToClick + "'> Verify </a>" + "</button> <br/>" +
                 "or copy the link " + linkToClick + "to a browser to continue. <br/> <br/>" +
                  "Kind regards,<br/>" +
                  "Tee Group.";

                var isSent = emailService.SendEmail(toEmail, subject, message);
                if (isSent)
                {
                    return true;
                }
            }
            return false;
        }

        public bool SendAdminEmail(SupportViewModel customer)
        {
            if (customer != null)
            {
                string toEmail = _emailConfiguration.AdminEmail;
                string subject = "Message From Tee Blog Contact.";

                var message = "Hello Admin" + "," + " Message from a user:" +
                    "</br> <br/> " + "Name: " + customer.Name + "," + "</br> " +
                    "</br> " + "Subject: " + customer.Subject + "," + "</br> " +
                    "</br> " + "Email Address: " + customer.Email + "," + "</br> " +
                    "</br> " + "Message: " + customer.Message + "</br> " +
                    "</br> " +

                  "Tee Group.";

                var isSent = emailService.SendEmail(toEmail, subject, message);
                if (isSent)
                {
                    return true;
                }
            }
            return false;
        }

    }
}


