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
    [Route("/cart")]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpPost("add")]
           public ActionResult<int> CartAdd(CartData.CartStorageAdd cart)
        {
            var _ID_CART = _cartRepository.AddToCart(cart);
            if (_ID_CART == -1) return BadRequest();
            return Ok(new { id_cart = _ID_CART });
        }

        [HttpPost("get")]
        public ActionResult<CartData.CartItemDb> CartGet(CartData.IdCart cart)
        {
            return Ok(new { cart = _cartRepository.GetCartItems(cart) });
        }

        [HttpPost("delete")]
        public ActionResult<CartData.CartItemDb> CartDeleteItem(CartData.DataCartForDelete storage)
        {
            return Ok(new { cart = _cartRepository.DeleteCartItem(storage) });
        }

        [HttpPost("update")]
        public ActionResult<CartData.CartItemDb> CartUpdateItem(CartData.CartStorageUpdate storage)
        {
            _cartRepository.UpdateCart(storage);
            return Ok(new {update = 1});
        }
    }
}