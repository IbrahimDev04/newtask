using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpaceDynamicProject.DataAccessLayer;
using SpaceDynamicProject.ViewModels.Card;

namespace SpaceDynamicProject.Controllers
{
    public class HomeController(SpaceContext _context) : Controller
    {
        public async Task<IActionResult> Index()
        {

            var data = await _context.cards
                .Select(s => new GetCardVM
                {
                    Title = s.Title,
                    Description = s.Description,
                    ImageUrl = s.ImageUrl,
                }).ToListAsync();

            return View(data);
        }
    }
}
