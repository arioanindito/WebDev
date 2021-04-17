using DSS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Services
{
    public interface IBorrower
    {
        IEnumerable<Borrower> GetBorrowers { get; }
        Borrower GetBorrower(int? ID);
        void Add(Borrower _Borrower);
        void Remove(int? ID);
    }
}
