using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Models
{
    public class MailData
    {
        public class Produs
        {
            [Required]
            public int? id_anvelopa { get; set; }
            public int? inaltime { get; set; }
            [Required]
            public int? latime { get; set; }
            [Required]
            public string? diametru { get; set; }
            [Required]
            public string? sezon { get; set; }
            [Required]
            public string? model { get; set; }
            [Required]
            public float? pret { get; set; }
            [Required]
            public int? cantitate { get; set; }
        }

        public class Comanda
        {
            [Required]
            public string? nume { get; set; }
            [Required]
            public string? prenume { get; set; }
            public string nume_companie { get; set; }
            [Required]
            public string? judet { get; set; }
            [Required]
            public string? localitate { get; set; }
            public string strada { get; set; }
            [Required]
            public int? numar { get; set; }
            public int cod_postal { get; set; }
            [Required]
            public string? telefon { get; set; }
            public string cui { get; set; }
            public string detalii { get; set; }
            [Required]
            public int? id_cart { get; set; }
        }
    }
}
