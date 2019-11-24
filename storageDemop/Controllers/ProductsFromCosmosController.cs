using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using storageDemop.Models;

namespace storageDemop.Controllers
{
    public class ProductsFromCosmosController : Controller
    {
        // GET: ProductsFromCosmos
        [ActionName("Index")]
        public async Task<ActionResult> Index()
        {
            var items = await DbRepository<ProductsFromCosmos>.GetItemsAsync();
            return View(items);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind(Include = "ProductId, Name, ProductModel, Category, Description")] ProductsFromCosmos item)
        {
            if (ModelState.IsValid)
            {
                await DbRepository<ProductsFromCosmos>.CreateItemAsync(item);
                return RedirectToAction("Index");
            }
            return View(item);
        }
    }
}