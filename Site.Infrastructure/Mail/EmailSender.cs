using Site.Application.Contracts.Infrastructure;
using Site.Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Site.Infrastructure.Mail
{
    public class EmailSender : IEmailSender
    {
        private EmailSetting _emailSettings;
        public Task<bool> SendEmail(Email email)
        {
            throw new NotImplementedException();
        }
    }
}
