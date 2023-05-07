using Group7_FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Group7_FinalProject.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private Repository<Product> data { get; set; }
        public CartController(FashionstoreContext ctx) => data = new Repository<Product>(ctx);


        private Cart GetCart()
        {
            var cart = new Cart(HttpContext);
            cart.Load(data);
            return cart;
        }

        public ViewResult Index()
        {
            var cart = GetCart();
            var builder = new ProductsGridBuilder(HttpContext.Session);

            var vm = new CartViewModel
            {
                List = cart.List,
                Subtotal = cart.Subtotal,
                ProductGridRoute = builder.CurrentRoute
            };
            return View(vm);
        }

        [HttpPost]
        public RedirectToActionResult Add(int id)
        {
            var product = data.Get(new QueryOptions<Product>
            {
                Include = "ProductBrands.Brand, Category",
                Where = b => b.ProductId == id
            });
            if (product == null)
            {
                TempData["message"] = "Unable to add product to cart.";
            }
            else
            {
                var dto = new ProductDTO();
                dto.Load(product);
                CartItem item = new CartItem
                {
                    Product = dto,
                    Quantity = 1
                };

                Cart cart = GetCart();
                cart.Add(item);
                cart.Save();

                TempData["message"] = $"{product.Name} added to cart";
            }

            var builder = new ProductsGridBuilder(HttpContext.Session);
            return RedirectToAction("List", "Product", builder.CurrentRoute);
        }

        [HttpPost]
        public RedirectToActionResult Remove(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            cart.Remove(item);
            cart.Save();

            TempData["message"] = $"{item.Product.Name} removed from cart.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public RedirectToActionResult Clear()
        {
            Cart cart = GetCart();
            cart.Clear();
            cart.Save();

            TempData["message"] = "Cart cleared.";
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            Cart cart = GetCart();
            CartItem item = cart.GetById(id);
            if (item == null)
            {
                TempData["message"] = "Unable to locate cart item";
                return RedirectToAction("List");
            }
            else
            {
                return View(item);
            }
        }

        [HttpPost]
        public RedirectToActionResult Edit(CartItem item)
        {
            Cart cart = GetCart();
            cart.Edit(item);
            cart.Save();

            TempData["message"] = $"{item.Product.Name} updated";
            return RedirectToAction("Index");
        }

        public ViewResult Checkout() => View();
    }
}
