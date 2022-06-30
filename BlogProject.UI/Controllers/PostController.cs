using BlogProject.CORE.Service;
using BlogProject.MODEL.Entities;
using BlogProject.UI.Models.VM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace BlogProject.UI.Controllers
{
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
        public IActionResult PostByID(Guid id)
        {
            SinglePostVM singlePostVM = new SinglePostVM();
            singlePostVM.Post = ps.GetByID(id);
            singlePostVM.User = us.GetbyDefault(x => x.ID == ps.GetByID(id).UserID);
            ViewBag.Categories = cs.GetActive();
            ViewBag.RandomPosts=ps.GetActive().Where(x => x.CategoryID == ps.GetByID(id).CategoryID).Take(3).ToList();
            return View(singlePostVM);
        }

        public IActionResult PostByAuthor(Guid id)
        {
            ViewBag.Author=us.GetByID(id).FirstName + " " + us.GetByID(id).LastName;
            return View(ps.GetDefault(x => x.UserID == id));
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult PostsByCategory(Guid id)
        {
            //Category ID'si gelecek ce o id'ye göre postlar listelenecek

            PostUserVM postUserVM = new PostUserVM();
            postUserVM.Posts = ps.GetDefault(x => x.CategoryID == id);
            postUserVM.Users = us.GetAll();
            return View(postUserVM);
        }
        [HttpPost]
        public IActionResult PostsBySearch(string query)
        {
            PostUserVM postUserVM = new PostUserVM();
            postUserVM.Posts = ps.GetDefault(x => x.Title.Contains(query));
            postUserVM.Users = us.GetAll();
            return View(postUserVM);
        }
    }
}
