using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.IHelper
{
    public interface IEmailConfiguration
    {
         string SmtpServer { get; set; }
         int SmtpPort { get; set; }
        string SmtpUsername { get; set; }
        string SmtpPassword { get; set; }
    }
}
