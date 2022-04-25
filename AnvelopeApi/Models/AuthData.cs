using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Models
{
    public class LoginUser
    {
        [Required]
        public string? username { get; set; }
        [Required]
        public string? password { get; set; }
    }

    public class UserDb
    {
        public int id_user { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
