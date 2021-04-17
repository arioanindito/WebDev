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
            //return View(_Book.GetBooks);
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
        [HttpGet]
        public IActionResult Details(int? ID)
        {
            //return View(_Book.GetBook(ID));

            return View(_book.GetBook(ID));

            //return View(book);

            //Book book = db.Books.Include(a => a.Images).FirstOrDefault(a => a.BookId == ID);
            //if (book == null)
            //{
            //    return RedirectToAction("Error", "Home");
            //}

            //Book book1 = new Book();
            //book1.BookId = book.BookId;
            //book1.BookName = book.BookName;

            //if (book.Images.Any())
            //{
            //    book1.BookName = book.Images.First().FileName;
            //}

            //return View(book1);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? ID)
        {
            var model = _book.GetBook(ID);
            return View("Edit", model);
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

        //public IActionResult Edit(int? ID)
        //{
        //    var edited = db.Books.Find(ID);
        //    return View(edited);
        //}
        //[HttpPost]
        //public IActionResult Edit(Book edited)
        //{
        //    var status = db.Books.FirstOrDefault();
        //    db.Books.Remove(status);
        //    db.Books.Add(edited);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}
    }
}
