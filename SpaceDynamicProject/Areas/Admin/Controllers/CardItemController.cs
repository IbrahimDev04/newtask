using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceDynamicProject.DataAccessLayer;
using SpaceDynamicProject.Extensions;
using SpaceDynamicProject.Models;
using SpaceDynamicProject.ViewModels.Card;

namespace SpaceDynamicProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin, Member")]
    
    public class CardItemController(SpaceContext _context, IWebHostEnvironment _env) : Controller
    {
        public async Task<IActionResult> Index(int page = 0)
        {
            int PageCount = 2;
            var n = await _context.cards.CountAsync();
            ViewBag.MaxPage = Math.Ceiling((double)n / PageCount);
            ViewBag.CurrentPage = page + 1;
            ViewBag.PrewPage = page - 1;

            var data = await _context.cards
                .Skip(PageCount * page )
                .Take(PageCount)
                .Select(s => new GetCardAdminVM
                {
                    Title = s.Title,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                    CreatedTime = s.CreatedTime.ToString("dd MMM yyyy"),
                    Id = s.Id,
                    UpdatedTime = s.UpdatedTime.ToString("dd MMM yyyy")
                }).ToListAsync();

            return View(data);
        }

        public async Task<IActionResult> Create()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCardVM vm)
        {

            if (!ModelState.IsValid) return View(vm);

            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsValidType("image"))
                    ModelState.AddModelError("ImageFile", "Type Error");
                if (!vm.ImageFile.IsValidSize(200))
                    ModelState.AddModelError("ImageFile", "Size Error");
            }

            if (!ModelState.IsValid) return View(vm);

            string fileName = await vm.ImageFile.SaveMangeImage(Path.Combine(_env.WebRootPath, "imgs", "cards"));

            await _context.cards.AddAsync(new Models.Card
            {
                Title = vm.Title,
                Description = vm.Description,
                ImageUrl = Path.Combine("imgs", "cards", fileName),
            });


            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {

            if (id == null || id < 0) return BadRequest();

            var data = _context.cards.FirstOrDefault(x => x.Id == id);

            if (data == null) return NotFound();


            UpdateCardVM vM = new UpdateCardVM
            {
                Title = data.Title,
                Description = data.Description
            };

            return View(vM);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int? id, UpdateCardVM vm)
        {

            if (id == null || id < 0) return BadRequest();

            var data = _context.cards.FirstOrDefault(x => x.Id == id);

            if (data == null) return NotFound();


            if (!ModelState.IsValid) return View(vm);

            if (vm.ImageFile != null)
            {
                if (!vm.ImageFile.IsValidType("image"))
                    ModelState.AddModelError("ImageFile", "Type Error");
                if (!vm.ImageFile.IsValidSize(200))
                    ModelState.AddModelError("ImageFile", "Size Error");
            }

            if (!ModelState.IsValid) return View(vm);

            string fileName = await vm.ImageFile.SaveMangeImage(Path.Combine(_env.WebRootPath, "imgs", "cards"));

            data.Title = vm.Title;
            data.Description = vm.Description;
            data.ImageUrl = Path.Combine("imgs", "cards", fileName);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null || id < 0) return BadRequest();

            var data = _context.cards.FirstOrDefault(x => x.Id == id);

            if (data == null) return NotFound();

            data.ImageUrl.Delete(Path.Combine(_env.WebRootPath));

            _context.cards.Remove(data);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
