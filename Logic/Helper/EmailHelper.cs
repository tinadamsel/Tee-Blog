using Core.Models;
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
        public EmailHelper(IEmailService emailService)
        {
            //_emailService = emailService;
            this.emailService = emailService;
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


    }
}


