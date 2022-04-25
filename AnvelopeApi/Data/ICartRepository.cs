using AnvelopeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnvelopeApi.Data
{
    public interface ICartRepository
    {
        int AddToCart(CartData.CartStorageAdd cart);
        IEnumerable<CartData.CartItemDb> GetCartItems(CartData.IdCart cart);
        int DeleteCartItem(CartData.DataCartForDelete storage);
        void UpdateCart(CartData.CartStorageUpdate cart);

    }
}
