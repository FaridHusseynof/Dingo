using Dingo.Areas.AdminPanel.ViewModels;
using Dingo.Data;
using Dingo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Dingo.Areas.AdminPanel.Controllers
{
    [Authorize(Roles ="SuperAdmin, Admin")]
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private DingoDbContext _context { get; }
        public ProductController(DingoDbContext context)
        {
            _context=context;
        }
        public IActionResult Index()
        {
            return View(_context.products.Where(c => !c.IsDeleted));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateVM vm)
        {
            if (!ModelState.IsValid) return View();
            Product product = new Product
            {
                Title=vm.title,
                Price=vm.price,
                Description=vm.description,
                IsDeleted=false
            };
            if (product==null) return NotFound();
            if (vm.imageFile!=null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                product.ImageURL=fileName;
            }
            _context.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id) 
        {
            if (id==null) return BadRequest();
            Product? product = _context.products.Where(c => !c.IsDeleted).FirstOrDefault(i=>i.Id==id);
            if (product==null) return NotFound();
            product.IsDeleted=true;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int? id) 
        {
            if (id==null) return BadRequest();
            Product? product = _context.products.Where(c => !c.IsDeleted).FirstOrDefault(i => i.Id==id);
            if (product==null) return NotFound();
            UpdateVM vm = new UpdateVM
            {
                title=product.Title,
                price=product.Price,
                description=product.Description,
                id_=product.Id
            };
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateVM vm) 
        {
            if (!ModelState.IsValid) return View();
            Product? product = _context.products.Where(c => !c.IsDeleted).FirstOrDefault(i => i.Id==vm.id_);
            if (product==null) return NotFound();

            product.Title=vm.title;
            product.Price=vm.price;
            product.Description=vm.description;

            if (vm.imageFile!=null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(vm.imageFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    await vm.imageFile.CopyToAsync(stream);
                }
                product.ImageURL=fileName;
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
