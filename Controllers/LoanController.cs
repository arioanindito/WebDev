using DSS_MVC.Models;
using DSS_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoan _Loan;
        private readonly IBook _Book;
        private readonly IBorrower _Borrower;
        public LoanController(ILoan _ILoan, IBook _IBook, IBorrower _IBorrower)
        {
            _Loan = _ILoan;
            _Book = _IBook;
            _Borrower = _IBorrower;
        }
        public IActionResult Index()
        {
            return View(_Loan.GetLoans);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Books = _Book.GetBooks;
            ViewBag.Borrowers = _Borrower.GetBorrowers;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Loan model)
        {
            if (ModelState.IsValid)
            {
                _Loan.Add(model);
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
                Loan model = _Loan.GetLoan(ID);
                return View(model);
            }
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? ID)
        {
            _Loan.Remove(ID);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? ID)
        {
            var model = _Loan.GetLoan(ID);
            ViewBag.Books = _Book.GetBooks;
            ViewBag.Borrowers = _Borrower.GetBorrowers;
            ViewBag.Loan = _Loan.GetLoans;
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Loan model)
        {
            if (ModelState.IsValid)
            {
                _Loan.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
