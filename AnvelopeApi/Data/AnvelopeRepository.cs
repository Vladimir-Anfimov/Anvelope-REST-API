using AnvelopeApi.Models;
using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace AnvelopeApi.Data
{
    public class AnvelopeRepository : IAnvelopeRepository
    {
        private readonly string _connectionString;
        private readonly MySqlConnection _conn;
        public AnvelopeRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _conn = new MySqlConnection(_connectionString);
        }

        public int addAnvelopa(AnvelopaForCreateWithoutId anvelopa, string imagine)
        {
            var sql = "CALL ANVELOPE_add(@Inaltime, @Latime, @Diametru, @Sezon, @Model, @Pret, @Descriere, @IndiceViteza, @Dot, @Imagine, @Stoc, @Categorie, @IndiceGreutate, @Marca);";
            return _conn.QuerySingleOrDefault<int>(sql, new {
                Inaltime = anvelopa.inaltime,
                Latime = anvelopa.latime,
                Diametru = anvelopa.diametru.ToUpper().Trim(),
                Sezon = anvelopa.sezon.ToLower().Trim(),
                Model = anvelopa.model.ToUpper().Trim(),
                Pret = anvelopa.pret,
                Descriere = anvelopa.descriere.Trim(),
                IndiceViteza = anvelopa.indice_viteza.ToUpper().Trim(),
                Dot = anvelopa.dot,
                Imagine = imagine,
                Stoc = anvelopa.stoc,
                Categorie = anvelopa.categorie.ToLower().Trim(),
                IndiceGreutate = anvelopa.indice_greutate.ToUpper().Trim(),
                Marca = anvelopa.marca.ToUpper().Trim()
            });
        }
        
        public string deleteAnvelopaById(IdAnvelopa anvelopa)
        {
            var sql = "CALL ANVELOPA_delete(@IdAnvelopa)";
            return _conn.QueryFirstOrDefault<string>(sql, new { IdAnvelopa =  anvelopa.id});
        }

        public IEnumerable<AnvelopaAllDb> getAllAnvelope(int sort)
        {
            var sql = "CALL ANVELOPE_getAllAnvelope(@Sort)";
            return _conn.Query<AnvelopaAllDb>(sql, new {
                Sort = sort    
            });
        }

        public IEnumerable<AnvelopaAllDb> getAllAnvelopePanel()
        {
            var sql = "CALL ANVELOPE_getAllPanel()";
            return _conn.Query<AnvelopaAllDb>(sql);
        }

        public IEnumerable<Measurements> getAllMeasurements(Measurements filtre)
        {
            bool paramBefore = false;
            var sql = "SELECT DISTINCT categorie, inaltime, latime, diametru, sezon FROM anvelope";
            if (filtre.inaltime != 0 || filtre.latime != 0 || filtre.diametru != "null")
                sql += " WHERE";
          
            if (filtre.inaltime != 0)
            {
                if (paramBefore == true) sql += " AND";
                sql += " inaltime=@Inaltime";
                paramBefore = true;
            }
            if (filtre.latime != 0)
            {
                if (paramBefore == true) sql += " AND";
                sql += " latime=@Latime";
                paramBefore = true;
            }
            if (filtre.diametru != "null")
            {
                if (paramBefore == true) sql += " AND";
                sql += " LOWER(TRIM(diametru))=LOWER(TRIM(@Diametru))";
                paramBefore = true;
            }
          

            return _conn.Query<Measurements>(sql, new
            {
                Categorie = filtre.categorie,
                Inaltime = filtre.inaltime,
                Latime = filtre.latime,
                Diametru = filtre.diametru,
                Sezon = filtre.sezon
            });

        }

        public AnvelopaSingleRequest getAnvelopaById(int id)
        {
            var sql = "CALL ANVELOPE_getAnvelopaById(@IdAnvelopa)";
            return _conn.QueryFirstOrDefault<AnvelopaSingleRequest>(sql, new { IdAnvelopa = id });
        }

        public AnvelopeFullDataWithNrAnvelope getAnvelopeFiltrare(Filtrare filtre)
        {
            bool paramBefore = false;
            var sql = "SELECT id_anvelopa, inaltime, latime, diametru, sezon, model, pret, imagine, categorie, stoc FROM anvelope";
            var cmd = "";
            if (filtre.categorie != "null" || filtre.inaltime != 0 || filtre.latime != 0 || filtre.diametru != "null" || filtre.sezon != "null")
                cmd += " WHERE";
            if (filtre.categorie != "null")
            {
                cmd += " LOWER(TRIM(categorie))=LOWER(TRIM(@Categorie))";
                paramBefore = true;
            }
            if (filtre.inaltime != 0)
            {
                if (paramBefore == true) cmd += " AND";
                cmd += " inaltime=@Inaltime";
                paramBefore = true;
            }
            if (filtre.latime != 0)
            {
                if (paramBefore == true) cmd += " AND";
                cmd += " latime=@Latime";
                paramBefore = true;
            }
            if (filtre.diametru != "null")
            {
                if (paramBefore == true) cmd += " AND";
                cmd += " LOWER(TRIM(diametru))=LOWER(TRIM(@Diametru))";
                paramBefore = true;
            }
            if (filtre.sezon != "null")
            {
                if (paramBefore == true) cmd += " AND";
                cmd += " LOWER(TRIM(sezon))=LOWER(TRIM(@Sezon))";
            }

            sql += cmd;
            if (filtre.order == 1) sql += " ORDER BY pret";
            else if (filtre.order == 2) sql += " ORDER BY pret DESC";

            sql += " LIMIT 24 OFFSET @RowNr";
            var _anvelope = _conn.Query<AnvelopaAllDb>(sql, new { 
                Categorie = filtre.categorie,
                Inaltime = filtre.inaltime,
                Latime = filtre.latime,
                Diametru = filtre.diametru,
                Sezon = filtre.sezon,
                Order = filtre.order,
                RowNr = filtre.pagina == 1 ? 0 : (filtre.pagina - 1) * 24
            });

            int _nr_anvelope = _conn.QuerySingleOrDefault<int>("SELECT COUNT(*) as nr_anvelope FROM anvelope" + cmd, new
            {
                Categorie = filtre.categorie,
                Inaltime = filtre.inaltime,
                Latime = filtre.latime,
                Diametru = filtre.diametru,
                Sezon = filtre.sezon,
                Order = filtre.order,
                RowNr = filtre.pagina == 1 ? 0 : (filtre.pagina - 1) * 24
            });

            AnvelopeFullDataWithNrAnvelope queryResult = new AnvelopeFullDataWithNrAnvelope
            {
                anvelope = _anvelope,
                nr_anvelope = _nr_anvelope
            };

            return queryResult;


        }

        public IEnumerable<AnvelopaAllDb> getAnvelopeRecomandate()
        {
            var sql = "CALL ANVELOPE_getRecomandate()";
            return _conn.Query<AnvelopaAllDb>(sql);
        }

        public IEnumerable<AnvelopaAllDb> getAnvelopeSimilare(string diametru, int id)
        {
            var sql = "CALL ANVELOPE_getSimilare(@Diametru, @Id)";
            return _conn.Query<AnvelopaAllDb>(sql, new { 
                Diametru = diametru,
                Id = id
            });;
        }

        public int updateAnvelopa(AnvelopaAllData anvelopa, string imagine)
        {
            var sql = "CALL ANVELOPE_update(@IdAnvelopa, @Inaltime, @Latime, @Diametru, @Sezon, @Model, @Pret, @Descriere, @IndiceViteza, @Dot, @Imagine, @Stoc, @Categorie, @IndiceGreutate, @Marca, @Recomandat)";
            return _conn.QuerySingleOrDefault<int>(sql, new
            {
                IdAnvelopa = anvelopa.id_anvelopa,
                Inaltime = anvelopa.inaltime,
                Latime = anvelopa.latime,
                Diametru = anvelopa.diametru.ToUpper().Trim(),
                Sezon = anvelopa.sezon.ToLower().Trim(),
                Model = anvelopa.model.ToUpper().Trim(),
                Pret = anvelopa.pret,
                Descriere = anvelopa.descriere.Trim(),
                IndiceViteza = anvelopa.indice_viteza.ToUpper().Trim(),
                Dot = anvelopa.dot,
                Imagine = imagine,
                Stoc = anvelopa.stoc,
                Categorie = anvelopa.categorie.ToLower().Trim(),
                IndiceGreutate = anvelopa.indice_greutate.ToUpper().Trim(),
                Marca = anvelopa.marca.ToUpper().Trim(),
                Recomandat = anvelopa.recomandat
            });
        }
    }
}
