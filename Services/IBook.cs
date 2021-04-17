using DSS_MVC.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Services
{
    public interface IBook
    {
        IEnumerable<Book> GetBooks { get; }
        Book GetBook(int? ID);
        void Add(Book _Book, IFormFile photo);
        void Remove(int? ID);
    }
}
