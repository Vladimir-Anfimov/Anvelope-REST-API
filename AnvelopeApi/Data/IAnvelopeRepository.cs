using AnvelopeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Data
{
    public interface IAnvelopeRepository
    {
        AnvelopaSingleRequest getAnvelopaById(int id);
        IEnumerable<AnvelopaAllDb> getAllAnvelope(int sort);
        AnvelopeFullDataWithNrAnvelope getAnvelopeFiltrare(Filtrare filtre);
        string deleteAnvelopaById(IdAnvelopa anvelopa);
        int addAnvelopa(AnvelopaForCreateWithoutId anvelopa, string filename);
        int updateAnvelopa(AnvelopaAllData anvelopa, string filename);
        IEnumerable<AnvelopaAllDb> getAnvelopeRecomandate();
        IEnumerable<AnvelopaAllDb> getAnvelopeSimilare(string diametru, int id);
        IEnumerable<Measurements> getAllMeasurements(Measurements masuriWeb);

        IEnumerable<AnvelopaAllDb> getAllAnvelopePanel();

    }
}
