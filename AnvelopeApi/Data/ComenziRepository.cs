using AnvelopeApi.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Data
{
    public class ComenziRepository : IComenziRepository
    {
        private readonly string _connectionString;
        private readonly MySqlConnection _conn;
        public ComenziRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _conn = new MySqlConnection(_connectionString);
        }

        public int salveazaComanda(MailData.Comanda comanda)
        {
            var sql = "CALL COMENZI_new(@Nume, @Prenume, @Nume_companie, @Judet, @Localitate, @Strada, @Numar, @Cod_postal, @Telefon, @Cui, @Detalii)";
            return _conn.QueryFirstOrDefault<int>(sql, new
            {
                Nume = comanda.nume.Trim(),
                Prenume = comanda.prenume.Trim(),
                Nume_companie = comanda.nume_companie.Trim(),
                Judet = comanda.judet.Trim(),
                Localitate = comanda.localitate.Trim(),
                Strada = comanda.strada.Trim(),
                Numar = comanda.numar,
                Cod_postal = comanda.cod_postal,
                Telefon = comanda.telefon.Trim(),
                Cui = comanda.cui.Trim(),
                Detalii = comanda.detalii.Trim()
            });
        }

        public void introduItemComanda(int id_comanda, int id_anvelopa, int cantitate)
        {
            var sql = "CALL ITEME_COMANDA_adauga(@Id_comanda, @Id_anvelopa, @Cantitate)";
            _conn.QueryFirstOrDefault(sql, new
            {
                Id_comanda = id_comanda,
                Id_anvelopa = id_anvelopa,
                Cantitate = cantitate
            });
        }
    }
}
