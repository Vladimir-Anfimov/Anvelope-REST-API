using AnvelopeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Data
{
    public interface ISecurityRepository
    {
        UserDb LoginAdmin(LoginUser dateLogin);
    }
}
