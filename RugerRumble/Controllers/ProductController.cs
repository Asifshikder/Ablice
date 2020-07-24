using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Controller;
using RugerRumble.Models;
using RugerRumble.Models.ViewModel;

namespace RugerRumble.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private AppDbContext context;
        private IWebHostEnvironment environment;

        public ProductController(AppDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            products = context.Products.ToList();
            return View(products);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductVM product)
        {
            try
            {

                IFormFile file = product.ImgUrl;
                string webrootpath = environment.WebRootPath;
                string filename = Path.GetFileNameWithoutExtension(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                string foldername = "UploadImage";
                //model.ImageName = filename = filename + extension;
                string path = Path.Combine(webrootpath, foldername, filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
                string pathName = Path.Combine(foldername, filename);
                Product model = new Product();
                model.ProductName = product.ProductName;
                model.Price = product.Price;
                model.ImgUrl = pathName;
                context.Add(model);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
               var sd = ex.Message;
            }
            return View(nameof(Index));
        }
    }
}
