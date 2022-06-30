using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogProject.UI.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]

    public class PostController : Controller
    {
        private readonly ICoreService<Category> cs;
        private readonly ICoreService<Post> ps;
        private readonly ICoreService<User> us;
        public PostController(ICoreService<Category> _cs, ICoreService<Post> _ps, ICoreService<User> _us)
        {

            cs = _cs;
            us = _us;
            ps = _ps;
        }
        public IActionResult ListPost()
        {
            return View(ps.GetAll());
        }
        public IActionResult AddPost()
        {
            //Addpost view gösterecek
            ViewBag.Categories = cs.GetActive();
            ViewBag.Users=us.GetActive();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPost(IFormCollection gelenMakale)
        {
            Post eklenecekPost = new Post();
            eklenecekPost.Title = gelenMakale["title"];
            eklenecekPost.PostDetail = gelenMakale["detail"];
            eklenecekPost.Tags = gelenMakale["tags"];
            eklenecekPost.ImagePath = gelenMakale["imagePath"];
            eklenecekPost.ViewCount = Convert.ToInt32(gelenMakale["viewCount"]);
            eklenecekPost.CategoryID = Guid.Parse(gelenMakale["categoryId"]);
            eklenecekPost.UserID = Guid.Parse(gelenMakale["userId"]);
            if (ModelState.IsValid)
            {
                
                return RedirectToAction(nameof(ListPost));
            }
            return View(gelenMakale);
        }

        public IActionResult ActivatePost(Guid id)
        {
            ps.Activate(id);
            return RedirectToAction("ListPost");
        }
    }
}
