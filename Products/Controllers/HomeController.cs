using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Products.Data;
using Products.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Authorization;

namespace Products.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        // Constructor that initializes the ApplicationDbContext and IWebHostEnvironment dependencies
        public HomeController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // Create action method to add a new product to the database
        [Route("create")]
        public IActionResult Create(Product product)
        {
            try
            {
                // Add the product to the database context
                _context.Product.Add(product);
                // Save changes to the database
                _context.SaveChanges();
                // Set TempData flag for successful addition
                TempData["add"] = true;
                // Redirect to the Index action method
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Set TempData flag for failed addition
                TempData["add"] = false;
            }

            // If adding the product fails, retrieve all products from the database
            var products = _context.Product.ToList();
            // Store products in ViewBag
            ViewBag.Product = products;
            // Return the Index view
            return View("Index");
        }

        // CreateDetails action method to add a new product with an image to the database
        public IActionResult CreateDetails(ProductDetails product, IFormFile Images)
        {
            if (Images == null || Images.Length == 0)
            {
                // Return message if no file is selected
                return Content("File Not Selected");
            }

            // Get the path to save the uploaded image
            var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", Images.FileName);

            // Save the uploaded image to the server
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                Images.CopyTo(stream);
                stream.Close();
            }

            // Set the image filename in the product details
            product.Images = Images.FileName;
            // Add the product details to the database context
            _context.ProductDetails.Add(product);
            // Save changes to the database
            _context.SaveChanges();
            // Redirect to the ProductDetails action method
            return RedirectToAction("ProductDetails");
        }

        // Edit action method to retrieve and display a product for editing
        public IActionResult Edit(int product)
        {
            // Find the product by its Id
            var products = _context.Product.SingleOrDefault(x => x.Id == product);
            // Return the product to the view
            return View(products);
        }

        // Update action method to update a product's details in the database
        public IActionResult Update(Product product)
        {
            if (ModelState.IsValid)
            {
                // Update the product in the database context
                _context.Product.Update(product);
                // Save changes to the database
                _context.SaveChanges();
            }
            // Redirect to the Index action method
            return RedirectToAction("Index");
        }

        // Delete action method to remove a product from the database
        public IActionResult Delete(int product)
        {
            // Find the product by its Id
            var a = _context.Product.SingleOrDefault(x => x.Id == product);
            if (a != null)
            {
                // Remove the product from the database context
                _context.Product.Remove(a);
                // Save changes to the database
                _context.SaveChanges();
                // Set TempData flag for successful deletion
                TempData["del"] = true;
            }
            else
            {
                // Set TempData flag for failed deletion
                TempData["del"] = false;
            }
            // Redirect to the Index action method
            return RedirectToAction("Index");
        }


        public JsonResult GetData(int id)
        {
            var products = _context.ProductDetails.SingleOrDefault(x => x.Id == id);
            if (products != null)
            {
                return Json(products);
            }
            return Json(null);


        }

        // EditD action method to retrieve and display product details for editing
        public IActionResult EditD(int product)
        {
            // Find the product details by its Id
            var products = _context.ProductDetails.SingleOrDefault(x => x.Id == product);
            // Find the product by its Id
            var productn = _context.Product.SingleOrDefault(x => x.Id == product);
            if (productn != null)
            {
                // Set the product name in ViewData
                ViewData["name"] = productn.Name.ToString();
            }
            else
            {
                // Set a default value if the product is not found
                ViewData["name"] = "none";
            }
            // Retrieve all products from the database
            var produc = _context.Product.ToList();
            // Store products in ViewBag
            ViewBag.Products = produc;
            // Return the product details to the view
            return View(products);
        }

        // UpdateD action method to update product details including an image in the database
        public IActionResult UpdateD(ProductDetails product, IFormFile Images)
        {
            // Find the existing product details by its Id
            var existingProduct = _context.ProductDetails.Find(product.Id);
            if (Images != null)
            {
                // Get the path to save the uploaded image
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "img", Images.FileName);
                // Save the uploaded image to the server
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    Images.CopyTo(stream);
                    stream.Close();
                }
                // Set the image filename in the existing product details
                existingProduct.Images = Images.FileName;
            }

            // Update the product details if valid
            if (product.Price > 0 && product.Qty > 0 && !string.IsNullOrEmpty(product.Color))
            {
                existingProduct.Price = product.Price;
                existingProduct.Qty = product.Qty;
                existingProduct.Color = product.Color;
                existingProduct.Products_Id = product.Products_Id;
            }

            // Update the product details in the database context
            _context.ProductDetails.Update(existingProduct);
            // Save changes to the database
            _context.SaveChanges();
            // Redirect to the ProductDetails action method
            return RedirectToAction("ProductDetails");
        }

        // DeleteD action method to remove product details from the database
        public IActionResult DeleteD(int product)
        {
            // Find the product details by its Id
            var a = _context.ProductDetails.SingleOrDefault(x => x.Id == product);
            if (a != null)
            {
                // Remove the product details from the database context
                _context.ProductDetails.Remove(a);
                // Save changes to the database
                _context.SaveChanges();
            }
            // Redirect to the ProductDetails action method
            return RedirectToAction("ProductDetails");
        }

        public IActionResult CreateDamage(Damagedproducts product)
        {

            try
            {
                // Add the product to the database context
                _context.Damagedproducts.Add(product);
                // Save changes to the database
                _context.SaveChanges();
                // Set TempData flag for successful addition
                TempData["addd"] = true;
                // Redirect to the Index action method
                return RedirectToAction("Damagedproducts");
            }
            catch (Exception ex)
            {
                // Set TempData flag for failed addition
                TempData["addd"] = false;
            }

            // If adding the product fails, retrieve all products from the database
            var products = _context.Product.ToList();
            // Store products in ViewBag
            ViewBag.Product = products;
            // Return the  view


            return View("Damagedproducts");
        }

        // Index action method to retrieve and display a list of products
       [Authorize]
        public IActionResult Index()
        {

            var user = HttpContext.User.Identity.Name??null;
            //TempData["username"]= user;
            //CookieOptions cookie = new CookieOptions();
            //cookie.Expires = DateTime.Now.AddMinutes(10);
            //Response.Cookies.Append("userdata",user, cookie);
            //ViewBag.User = Request.Cookies["userdata"];
            //HttpContext.Session.SetString("userdata", user);
            Console.WriteLine(user);
            // Retrieve all products from the database
            ViewBag.User = user;
            var product = _context.Product.ToList();
            // Store products in ViewBag
            ViewBag.Product = product;
            // Return the Index view
            return View();
        }

        public IActionResult Damagedproducts()
        {

            // Join Damagedproducts and Product and ProductDetails tables to get detailed product information
            var productDetails = _context.Product
    .Join(
        _context.Damagedproducts,
        product => product.Id,
        damagedProduct => damagedProduct.Products_Id,
        (product, damagedProduct) => new { product, damagedProduct }
    )
    .Join(
        _context.ProductDetails,
        combined => combined.product.Id,
        ProductDetail => ProductDetail.Products_Id,
        (combined, ProductDetail) => new
        {
            id = combined.damagedProduct.Id,
            name = combined.product.Name,
            qty = combined.damagedProduct.Qty,
           
            color = ProductDetail.Color,
            
            
        }
    )
    .ToList();

           
            // Store detailed product information in ViewBag
            ViewBag.ProductD = productDetails;


            // Retrieve all products from the database
            var product = _context.Product.ToList();
            // Store products in ViewBag
            ViewBag.Product = product;
            // Return the  view
            return View();
        }

        // ProductDetails action method to retrieve and display detailed product information
        public IActionResult ProductDetails()
        {
            // Join ProductDetails and Product tables to get detailed product information
            var productD = _context.ProductDetails.Join(
                _context.Product,
                x => x.Products_Id,
                y => y.Id,
                (x, y) => new
                {
                    id = x.Id,
                    name = y.Name,
                    qty = x.Qty,
                    price = x.Price,
                    images = x.Images,
                    color = x.Color,
                }
            ).ToList();
            // Store detailed product information in ViewBag
            ViewBag.ProductD = productD;
            // Retrieve all product details from the database
            var product = _context.ProductDetails.ToList();
            // Retrieve all products from the database
            var products = _context.Product.ToList();
            // Store products in ViewBag
            ViewBag.Products = products;
            // Return the product details to the view
            return View(product);
        }

        // Privacy action method to display the privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Error action method to display error information
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
