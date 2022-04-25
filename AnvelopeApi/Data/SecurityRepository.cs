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
    public class SecurityRepository : ISecurityRepository
    {
        private readonly string _connectionString;
        private readonly MySqlConnection _conn;
        public SecurityRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _conn = new MySqlConnection(_connectionString);
        }

        public UserDb LoginAdmin(LoginUser dateLogin)
        {
            var sql = "CALL USERS_loginUser(@Username)";
            return _conn.QueryFirstOrDefault<UserDb>(sql, new { Username = dateLogin.username.Trim().ToLower() });
        }
    }
}
