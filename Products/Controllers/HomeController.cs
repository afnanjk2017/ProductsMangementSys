using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Models;
using System.Diagnostics;

namespace Products.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        /* public HomeController(ILogger<HomeController> logger)
         {
             _logger = logger;
             
         }
 */
        public HomeController(ApplicationDbContext context)
        {
          
            _context = context;
        }

        [Route("create")]
        public IActionResult Create(Product product)
        {

            try
            {
                _context.Product.Add(product);
                _context.SaveChanges();
                TempData["add"]= true;

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["add"]= false;
            }



            // /return RedirectToAction("Index");
            var products = _context.Product.ToList();
            ViewBag.Product = products;
            return View("Index");
            

        }

        public IActionResult Edit(int product)
        {
            var products = _context.Product.SingleOrDefault(x => x.Id == product);
            // return one record alwayes 
            return View(products);
        }

        public IActionResult Update(Product product)
        {

            if (ModelState.IsValid) {

               _context.Product.Update(product);
                _context.SaveChanges();
               
           }

            return RedirectToAction("Index");

        }
        public IActionResult Delete(int product)
        {
            var a = _context.Product.SingleOrDefault(x => x.Id == product);
            if (a != null)
            {
                _context.Product.Remove(a);
                _context.SaveChanges();
                TempData["del"] =true;
            }
            else
            {
                TempData["del"] =false;
            }
           


            return RedirectToAction("Index");


        }
        public IActionResult Index()
        {
            var product = _context.Product.ToList();
            ViewBag.Product = product;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
