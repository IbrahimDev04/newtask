using Microsoft.AspNetCore.Mvc;

namespace SpaceDynamicProject.Areas.Admin.ViewComponents
{
    public class NavAdminViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View();
        }
    }
}
