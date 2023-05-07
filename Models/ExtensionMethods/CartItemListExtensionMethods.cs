using Group7_FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

namespace Group7_FinalProject.Models
{
    public static class CartItemListExtensions
    {
        public static List<CartItemDTO> ToDTO(this List<CartItem> list) =>
            list.Select(ci => new CartItemDTO
            {
                ProductId = ci.Product.ProductId,
                Quantity = ci.Quantity
            }).ToList();
    }
}
