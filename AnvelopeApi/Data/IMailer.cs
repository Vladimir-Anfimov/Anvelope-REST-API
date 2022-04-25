﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Data
{
    public interface IMailer
    {
        Task SendEmailAsync(string email, string subject, string body);
    }
}
