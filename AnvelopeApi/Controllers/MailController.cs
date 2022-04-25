using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnvelopeApi.Data;
using AnvelopeApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace AnvelopeApi.Controllers
{
    [ApiController]
    [Route("/mail")]
    public class MailController : ControllerBase
    {
        private readonly IMailer _mailer;
        private readonly ICartRepository _cartRepository;
        private readonly IComenziRepository _comenziRepository;
        private string _email = "########";
        private string _mailTEST = "###########";
        public MailController(IMailer mailer, ICartRepository cartRepository, IComenziRepository comenziRepository)
        {
            _mailer = mailer;
            _cartRepository = cartRepository;
            _comenziRepository = comenziRepository;
        }

        [HttpPost("comanda")]
        public async Task<IActionResult> SendMailComanda(MailData.Comanda comanda)
        {
            string produsURL = "#########";
            string imaginiURL = "############";
            string listaDeProduse = "";
            List<CartData.CartItemDb> produse = new List<CartData.CartItemDb>();
            CartData.IdCart cart = new CartData.IdCart();
            cart.id_cart = comanda.id_cart;
            int idComandaDB = _comenziRepository.salveazaComanda(comanda);
            Console.WriteLine(idComandaDB);
            produse = _cartRepository.GetCartItems(cart).ToList();
            var pretTotalProduse = 0.0;

            foreach (var product in produse)
            {
                pretTotalProduse += product.cantitate * product.pret;
                _comenziRepository.introduItemComanda(idComandaDB, product.id_anvelopa, product.cantitate);
                listaDeProduse += 
                    $" <ul style='border-radius: 7px; list-style-type: none; background-color: whitesmoke; padding:15px'>" +
                    $" <li style='margin-top: 5px;'><img src='{imaginiURL}{product.imagine}' width='100px'/></li>" +
                    $" <li style='margin-top: 5px;'><b>Denumire: </b>{product.model} {product.sezon}</li> " +
                    $" <li style='margin-top: 5px;'><b>Dimensiune: </b> {product.latime}/{product.inaltime} {product.diametru}</li>" +
                    $" <li style='margin-top: 5px;'><b>Cantitate: </b>{product.cantitate}</li> " +
                    $" <li style='margin-top: 5px;'><b>Pret total: </b>{product.cantitate * product.pret} lei</li> " +
                    $" <a href='{produsURL}{product.id_anvelopa}' style='margin:10px 0; border-radius: 5px; text-align: center; display:block; width: 150px; text-decoration: none; padding:15px 30px; background-color: rgb(221, 221, 221); box-shadow: 1px 3px 5px; color:black'>Vezi aici produsul</a> " +
                    $"</ul>";
            }
            string _comanda = $"" +
                $" <!DOCTYPE html>" +
                $" <html lang='en'>" +
                $" <head> " +
                $" <meta charset='UTF-8'> " +
                $" <meta name='viewport' content='width=device-width, initial-scale=1.0'> " +
                $" </head> " +
                $" <body> " +
                $" <div style='background-color: rgb(221, 221, 221); border-radius: 5px; padding:10px 20px; color:black'> " +
                $" <h1>Comanda noua!</h1> " +
                $" <p style='font-size: 1.1em;'>O noua comanda a fost amplasata pe site, mai jos sunt atasate datele personale ale clientului si produsele dorite.</p> " +
                $" <hr> " +
                $" <h3><b>Date client</b></h3> " +
                $" <ul style='border-radius: 7px; list-style-type: none; padding-left: 0;'> " +
                $" <li><b>Nume: </b>{comanda.nume}</li> " +
                $" <li style='margin-top: 5px;'><b>Prenume: </b>{comanda.prenume}</li> " +
                $" <li style='margin-top: 5px;'><b>Nume companie: </b>{comanda.nume_companie}</li> " +
                $" <li style='margin-top: 5px;'><b>Judet: </b>{comanda.judet}</li> " +
                $" <li style='margin-top: 5px;'><b>Localitate: </b>{comanda.localitate}</li> " +
                $" <li style='margin-top: 5px;'><b>Strada: </b>{comanda.strada}</li> " +
                $" <li style='margin-top: 5px;'><b>Numar: </b>{comanda.numar}</li> " +
                $" <li style='margin-top: 5px;'><b>Cod postal: </b>{comanda.cod_postal}</li> " +
                $" <li style='margin-top: 5px;'><b>Telefon: </b>{comanda.telefon}</li> " +
                $" <li style='margin-top: 5px;'><b>C.U.I: </b>{comanda.cui}</li> " +
                $" <li style='margin-top: 5px;'><b>Detalii: </b>{comanda.detalii}</li> " +
                $" </ul> " +
                $" <h3 style='color:green'><b>Valoare totală comandă: {pretTotalProduse} lei</b></h3> " +
                $" <br> <h3><b>Produse comandate</b></h3> " +
                $" {listaDeProduse} " +
                $" </div> " +
                $" </body> " +
                $" </html>";

            await _mailer.SendEmailAsync(_email, $"COMANDA NOUA - {comanda.nume} {comanda.prenume}", _comanda);
         

            return Ok(new { mesaj = "Mail trimis cu succes" });
        }
    }
}