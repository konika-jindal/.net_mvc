using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using products_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace products_mvc.Controllers
{
    public class ProductsController : Controller
    {
        // GET: ProductsController
        private readonly ProductContext _context;

        public ProductsController(ProductContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View( _context..ToList());
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind("Id,Name,Price,Color,ProductCategory")] Product pd)
        {

            if (ModelState.IsValid)
            { 
                _context.Add(pd);
            _context.SaveChangesAsync();
            return RedirectToAction("Index"); 
            }
            return View(pd);

            
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context..FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }


    }
}
