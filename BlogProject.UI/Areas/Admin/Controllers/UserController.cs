using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]

    public class UserController : Controller
    {
        private readonly ICoreService<Category> cs;
        private readonly ICoreService<Post> ps;
        private readonly ICoreService<User> us;
        public UserController(ICoreService<Category> _cs, ICoreService<Post> _ps, ICoreService<User> _us)
        {

            cs = _cs;
            us = _us;
            ps = _ps;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
