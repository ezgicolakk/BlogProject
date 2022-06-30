using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.UI.Controllers
{
    public class CategoryController : Controller
    {
        
        private readonly ICoreService<Category> cs;
        private readonly ICoreService<Post> ps;
        private readonly ICoreService<User> us;
        public CategoryController(ICoreService<Category> _cs)
        {
            
            cs = _cs;
            
        }
        public IActionResult Index()
        {
            return View(cs.GetActive());
        }

    }
}
