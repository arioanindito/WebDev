using DSS_MVC.Models;
using DSS_MVC.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DSS_MVC.Repository
{
    public class BookRepository : IBook
    {
        private DBContext db;
        private IConfiguration _configuration;
        public BookRepository(DBContext _db, IConfiguration configuration)
        {
            db = _db;
            _configuration = configuration;
        }

        public IEnumerable<Book> GetBooks => db.Books;

        public void Add(Book _Book, IFormFile photo)
        {
            if (_Book.BookId == 0)
            {
                db.Books.Add(_Book);
                db.SaveChanges();

                if (photo != null)
                {
                    string imagesPath = _configuration.GetValue<string>("PaintingPhotosLocation");

                    int newImageIndex = 0;
                    //Image lastImage = _context.Images.Where(a => a.PaintingId == painting.PaintingId).OrderBy(a => a.Index).LastOrDefault();
                    //if (lastImage != null)
                    //{
                    //    newImageIndex = lastImage.Index++;
                    //}

                    string directoryPath = Path.Combine(imagesPath, _Book.BookId.ToString());
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    string fileName = string.Format("{0}.jpg", Path.GetFileNameWithoutExtension(Path.GetRandomFileName()));
                    string filePath = Path.Combine(directoryPath, fileName);

                    //try
                    //{
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            photo.CopyTo(fileStream);
                        }

                        Image image = new Image();
                        image.BookId = _Book.BookId;
                        image.Index = newImageIndex;
                        image.FileName = fileName;
                        image.Name = _Book.BookName;

                        //db.Books.Add(_Book);
                        db.Images.Add(image);
                        
                        db.SaveChanges();
                        //db.Books.Add(image);
                        //db.SaveChanges();

                    //}
                    //catch
                    //{
                    //    //rollback
                    //    RedirectToAction("Index");
                    //}
                }
                //db.Books.Add(_Book);
                //db.SaveChanges();
        }
            else
            {
                var dbEntity = db.Books.Find(_Book.BookId);
                dbEntity.BookName = _Book.BookName;
                dbEntity.ISBN = _Book.ISBN;



                db.SaveChanges();
            }
}

        //private void RedirectToAction(string v)
        //{
        //    throw new NotImplementedException();
        //}

        public Book GetBook(int? ID)
        {
            return db.Books.Include(e => e.Loans)
                .ThenInclude(a => a.Borrowers)
                .Include(s => s.Images)
                .SingleOrDefault(a => a.BookId == ID);
        }

        public void Remove(int? ID)
        {
            Book dbEntity = db.Books.Find(ID);
            db.Books.Remove(dbEntity);
            db.SaveChanges();
        }
    }
}
