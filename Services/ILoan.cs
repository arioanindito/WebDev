using DSS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Services
{
    public interface ILoan
    {
        IEnumerable<Loan> GetLoans { get; }
        Loan GetLoan(int? ID);
        void Add(Loan _Loan);
        void Remove(int? ID);
    }
}
