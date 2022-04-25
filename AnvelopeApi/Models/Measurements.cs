using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Models
{
    public class Measurements
    {
        [Required]
        public string diametru { get; set; }
        [Required]
        public int inaltime { get; set; }
        [Required]
        public int latime { get; set; }
        [Required]
        public string sezon { get; set; }
        [Required]
        public string categorie { get; set; }
    }
}
