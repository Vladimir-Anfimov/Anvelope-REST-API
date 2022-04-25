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
    public class CartRepository : ICartRepository
    {
        private readonly string _connectionString;
        private readonly MySqlConnection _conn;
        public CartRepository(IConfiguration configuration)
        {
            _connectionString = configuration["ConnectionStrings:DefaultConnection"];
            _conn = new MySqlConnection(_connectionString);
        }

        public int AddToCart(CartData.CartStorageAdd cart)
        {
            var sql = "CALL CART_STORAGE_Add(@ID_ANVELOPA, @CANTITATE, @ID_CART);";
            return _conn.QuerySingleOrDefault<int>(sql, new
            {
                ID_ANVELOPA = cart.id_anvelopa,
                CANTITATE = cart.cantitate,
                ID_CART = cart.id_cart
            });
        }

  

        public int DeleteCartItem(CartData.DataCartForDelete storage)
        {
            var sql = "CALL CART_STORAGE_delete(@ID_STORAGE, @ID_CART);";
            return _conn.QuerySingleOrDefault<int>(sql, new
            {
                ID_STORAGE = storage.id_storage,
                ID_CART = storage.id_cart
            });
        }

        public IEnumerable<CartData.CartItemDb> GetCartItems(CartData.IdCart cart)
        {
            var sql = "CALL CART_STORAGE_getItems(@ID_CART);";
            return _conn.Query<CartData.CartItemDb>(sql, new
            {
                ID_CART = cart.id_cart
            });
        }

        public void UpdateCart(CartData.CartStorageUpdate cart)
        {
            var sql = "CALL CART_STORAGE_update(@ID_STORAGE, @ID_CART, @CANTITATE);";
             _conn.Query<CartData.CartItemDb>(sql, new
            {
                 ID_STORAGE = cart.id_storage,
                 ID_CART = cart.id_cart,
                 CANTITATE = cart.cantitate
             });
        }
    }
}