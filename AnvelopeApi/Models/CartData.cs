using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Models
{
    public class CartData
    {
        public class CartStorageAdd
        {
            [Required]
            public int? id_anvelopa { get; set; }
            [Required]
            public int? cantitate { get; set; }
            [Required]
            public int? id_cart { get; set; }
        }

        public class CartStorageUpdate
        {
            [Required]
            public int? id_storage { get; set; }
            [Required]
            public int? cantitate { get; set; }
            [Required]
            public int? id_cart { get; set; }
        }

        public class IdCart
        {
            [Required]
            public int? id_cart { get; set; }
        }

        public class DataCartForDelete
        {
            [Required]
            public int? id_storage { get; set; }
            [Required]
            public int? id_cart { get; set; }
        }

        public class CartItemDb
        {
            public int id_storage { get; set; }
            public int cantitate { get; set; }
            public int inaltime { get; set; }
            public int latime { get; set; }
            public string diametru { get; set; }
            public string sezon { get; set; }
            public string model { get; set; }
            public float pret { get; set; }
            public string imagine { get; set; }
            public int id_anvelopa { get; set; }
        }
    }
}
