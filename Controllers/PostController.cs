using WebDev.Models;
using WebDev.Repository;
using WebDev.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace WebDev.Controllers
{
    public class PostController : Controller
    {
        //private readonly UserManager<User> _manager;
        //private readonly UserManager<IdentityUser> _userManager;
        private readonly IPost _Post;
        private DBContext _db;
        public PostController(IPost Post, DBContext db)
        //UserManager<User> manager
        {
            _Post = Post;
            _db = db;
            //_manager = manager;
            //_userManager = userManager;
        }
        public IActionResult Index()
        {
            //ViewBag.username = _manager.GetUserName(HttpContext.User);
            return View(_db.Posts.Include(a => a.Images)
                //.Include(b => b.User)
                .ToList());
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Post model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                //var user = await _userManager.GetUserAsync(HttpContext.User);
                _Post.Add(model, photo);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            else
            {
                Post model = _Post.GetPost(ID);
                return View(model);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? ID)
        {
            _Post.Remove(ID);
            return RedirectToAction("Index");
        }
        //[Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IActionResult Details(int? ID)
        {
            return View(_Post.GetPost(ID));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? ID)
        {
            var model = _Post.GetPost(ID);
            return View(model);
            //return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Post model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                //var user = await _userManager.GetUserAsync(HttpContext.User);
                _Post.Add(model, photo);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
