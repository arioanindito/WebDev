using DSS_MVC.Models;
using DSS_MVC.Repository;
using DSS_MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Controllers
{
    public class BorrowerController : Controller
    {
        private readonly IBorrower _Borrower;
        private readonly IStatus _Status;
        private DBContext db;
        public BorrowerController(IBorrower _IBorrower, IStatus _IStatus, DBContext _db)
        {
            _Borrower = _IBorrower;
            _Status = _IStatus;
            db = _db;
        }
        public IActionResult Index()
        {
            return View(_Borrower.GetBorrowers);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Statuses = _Status.GetStatuses;
            return View();
        }
        [HttpPost]
        public IActionResult Create(Borrower model)
        {
            if (ModelState.IsValid)
            {
                _Borrower.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int? ID)
        {
            Borrower model = _Borrower.GetBorrower(ID);
            return View(model);
        }
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirm(int? ID)
        {
            _Borrower.Remove(ID);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Details(int? ID)
        {
            return View(_Borrower.GetBorrower(ID));
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int? ID)
        {
            ViewBag.Statuses = _Status.GetStatuses;
            var model = _Borrower.GetBorrower(ID);
            return View("Edit", model);
        }
        [HttpPost]
        public IActionResult Edit(Borrower model)
        {
            if (ModelState.IsValid)
            {
                _Borrower.Add(model);
                return RedirectToAction("Index");
            }
            return View(model);
        }
    }
}
