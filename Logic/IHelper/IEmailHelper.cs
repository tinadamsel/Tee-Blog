﻿using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelper
{
    public interface IEmailHelper
    {
        bool SendVerificationEmail(ApplicationUser user, string linkToClick);
        bool SendAdminEmail(SupportViewModel customer);
    }
}
