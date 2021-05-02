using DSS_MVC.Models;
using DSS_MVC.Repository;
using DSS_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBook _book;
        private DBContext _db;
        public BookController(IBook book,DBContext db)
        {
            _book = book;
            _db = db;
        }
        public IActionResult Index()
        {
            return View(_db.Books.Include(a => a.Images).ToList());
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                _book.Add(model, photo);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int? ID)
        {
            if (ID == null)
            {
                return NotFound();
            }
            else
            {
                Book model = _book.GetBook(ID);
                return View(model);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? ID)
        {
            _book.Remove(ID);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public IActionResult Details(int? ID)
        {
            return View(_book.GetBook(ID));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? ID)
        {
            var model = _book.GetBook(ID);
            return View(model);
            //return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Book model, IFormFile photo)
        {
            if (ModelState.IsValid)
            {
                _book.Add(model, photo);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
