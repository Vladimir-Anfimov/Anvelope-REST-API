using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Utils
{
    public interface ITokenUtils
    {
        public int ExtractingIdUserFromToken(string BearerToken);
        public string GenerateToken(int idUser);
    }
}
