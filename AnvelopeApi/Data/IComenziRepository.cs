using AnvelopeApi.Models;

namespace AnvelopeApi.Data
{
    public interface IComenziRepository
    {
        void introduItemComanda(int id_comanda, int id_anvelopa, int cantitate);
        int salveazaComanda(MailData.Comanda comanda);
    }
}