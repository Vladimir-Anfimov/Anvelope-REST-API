using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Models
{
    public class AnvelopaForCreateWithoutId
    {
        [Required]
        public int? inaltime { get; set; }
        [Required]
        public int? latime { get; set; }
        [Required]
        public string? diametru { get; set; }
        [Required]
        public string? sezon { get; set; }
        [Required]
        public float? pret { get; set; }
        [Required]
        public string? descriere { get; set; }
        [Required]
        public string? indice_viteza { get; set; }
        [Required]
        public int? dot { get; set; }
        [Required]
        public int? stoc { get; set; }
        [Required]
        public string? categorie { get; set; }
        [Required]
        public string? model { get; set; }
        [Required]
        public IFormFile? imagine { get; set; }
        [Required]
        public string? marca { get; set; }
        [Required]
        public string? indice_greutate { get; set; }
    }

    public class AnvelopaAllData
    {
            [Required]
            public int? id_anvelopa { get; set; }
            [Required]
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
            public string? descriere { get; set; }
            [Required]
            public string? indice_viteza { get; set; }
            [Required]
            public int? dot { get; set; }
            [Required]
            public int? stoc { get; set; }
            
            public IFormFile imagine { get; set; }
            [Required]
            public string? categorie { get; set; }
            [Required]
            public string? marca { get; set; }
            [Required]
            public string? indice_greutate { get; set; }
            [Required]
            public int? recomandat { get; set; }
    }

    public class AnvelopaSingleRequest
    {
        public int? inaltime { get; set; }      
        public int? latime { get; set; }      
        public string? diametru { get; set; }
        public string? sezon { get; set; }
        public float? pret { get; set; }
        public string? descriere { get; set; }
        public string? indice_viteza { get; set; }
        public int? dot { get; set; }
        public int? stoc { get; set; }
        public string? imagine { get; set; }
        public string? categorie { get; set; }
        public string? model { get; set; }
        public string? marca { get; set; }
        public string? indice_greutate { get; set; }
        //doar pt get
        public int recomandat { get; set; }
    }

    public class AnvelopaAllDb
    {
        [Required]
        public int? id_anvelopa { get; set; }
        [Required]
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
        public int? stoc { get; set; }
        [Required]
        public string? imagine { get; set; }
        [Required]
        public string? categorie { get; set; }
    }

    public class AnvelopeFullDataWithNrAnvelope
    {
        public IEnumerable<AnvelopaAllDb> anvelope { get; set; }
        public int nr_anvelope { get; set; }
    }

    public class Filtrare
    {
        public string categorie { get; set; }
        public int latime { get; set; }
        public int inaltime { get; set; }
        public string diametru { get; set; }
        public string sezon { get; set; }
        public int order { get; set; }
        public int pagina { get; set; }
        public int stoc { get; set; }
    }

    public class IdAnvelopa
    {
        [Required]
        public int? id { get; set; }
    }

    public class TestImg
    {
        public IFormFile? imagine { get; set; }
    }
}
