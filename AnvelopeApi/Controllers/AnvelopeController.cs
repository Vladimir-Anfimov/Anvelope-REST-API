using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AnvelopeApi.Data;
using AnvelopeApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AnvelopeApi.Controllers
{
    [ApiController]
    [Route("/anvelope")]
    public class AnvelopeController : ControllerBase
    {
        private readonly IAnvelopeRepository _anvelopeRepository;
        private IWebHostEnvironment _hostingEnvironment;
        private string filePath;
        public AnvelopeController(IAnvelopeRepository anvelopeRepository, IWebHostEnvironment environment)
        {
            _anvelopeRepository = anvelopeRepository;
            _hostingEnvironment = environment;
            filePath = Path.Combine(environment.WebRootPath,"images");
        }
        [HttpGet("{id:int}")]
        public ActionResult<AnvelopaSingleRequest> getAnvelopaById(int id)
        {
            if (id == null) return BadRequest(new { mesaj = "Nu ati trimis un id" });
            var _anvelopa = _anvelopeRepository.getAnvelopaById(id);
            if(_anvelopa == null) return Ok(new { anvelopa = _anvelopa });
            return Ok(new { anvelopa = _anvelopa, anvelopeSimilare = _anvelopeRepository.getAnvelopeSimilare(_anvelopa.diametru, id) });
        }

        [HttpGet("recomandate")]
        public ActionResult<AnvelopaAllDb> getAnvelopeRecomandate()
        {
            return Ok(new { anvelope = _anvelopeRepository.getAnvelopeRecomandate() });
        }

      

        [HttpPost("filtrat")]
        public ActionResult<AnvelopaAllDb> getAnvelopeFiltrate(Filtrare filtru)
        {
            return Ok(_anvelopeRepository.getAnvelopeFiltrare(filtru));
        }

        [HttpPost("panel/all")]
        [Authorize]
        public ActionResult<AnvelopaAllDb> getAllAnvelopeInPanel()
        {
            return Ok(new { anvelope = _anvelopeRepository.getAllAnvelopePanel() });
        }

        [HttpPost("add")]
        [Authorize]
        public async Task<ActionResult<string>> addAnvelopa([FromForm]AnvelopaForCreateWithoutId anvelopa)
        {
            var fileName = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + Path.GetFileName(anvelopa.imagine.FileName);
            var _status = _anvelopeRepository.addAnvelopa(anvelopa, fileName);
            Console.WriteLine(_status);
            if (_status == 1)
            {
                if (anvelopa.imagine != null && anvelopa.imagine.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await anvelopa.imagine.CopyToAsync(fileStream);
                    }
                    return Ok(new { status = _status, mesaj = "Adaugata cu succes." });
                }
                return BadRequest(new { mesaj = "Eroare de upload." });
            }
            return Ok(new { status = _status, mesaj = "Exista un produs cu aceleasi date deja." });
        }

        [HttpPost("edit")]
        [Authorize]
        public async Task<ActionResult<string>> updateAnvelopa([FromForm]AnvelopaAllData anvelopa)
        {

            string fileName;
            if (anvelopa.imagine == null) fileName = "-1";
            else fileName = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond).ToString() + Path.GetFileName(anvelopa.imagine.FileName);

            var _status = _anvelopeRepository.updateAnvelopa(anvelopa, fileName);

            if (_status == 1 && anvelopa.imagine != null)
            {
                if (anvelopa.imagine != null && anvelopa.imagine.Length > 0)
                {
                    using (var fileStream = new FileStream(Path.Combine(filePath, fileName), FileMode.Create))
                    {
                        await anvelopa.imagine.CopyToAsync(fileStream);
                    }
                    return Ok(new { status = _status, mesaj = "Modificata cu succes (inclusiv poza).", path = fileName });
                }
                return BadRequest(new { mesaj = "Eroare de upload." });
            }
            return Ok(new { status = 2, mesaj = "Modificata cu succes (fara poza)." });
        }

        [HttpPost("delete")]
        [Authorize]
        public ActionResult<string> deleteAvelopa(IdAnvelopa anvelopa)
        {
            string fileName;
            try
            {
                fileName = _anvelopeRepository.deleteAnvelopaById(anvelopa);
            }
            catch
            {
                return BadRequest(new { mesaj = "A aparut o eroare la stergerea din baza de date, prin urmare produsul nu a fost eliminat."});
            }
            try
            {
                if ((System.IO.File.Exists(Path.Combine(filePath, fileName))))
                {
                    System.Diagnostics.Debug.WriteLine(Path.Combine(filePath, fileName));
                    System.IO.File.SetAttributes(filePath, FileAttributes.Normal);
                    System.IO.File.Delete(Path.Combine(filePath, fileName));
                    return Ok(new { mesaj = "Stergere cu succes." });
                }
                else return BadRequest(new { mesaj = "A aparut o eroare la stergerea pozei, insa produsul a fost eliminat. Reactualizati pagina." });
            }
            catch
            {
                return BadRequest(new { mesaj = "A aparut o eroare la stergerea pozei, insa produsul a fost eliminat." });
            }
        }

        [HttpPost("filtru")]
        public ActionResult<Measurements> getAllMeasurements(Measurements masuri)
        {
            return Ok(new { filtru = _anvelopeRepository.getAllMeasurements(masuri) });
        }

       

    }

}