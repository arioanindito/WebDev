using DSS_MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DSS_MVC.Services
{
    public interface IStatus
    {
        IEnumerable<Status> GetStatuses { get; }
        Status GetStatus(int? ID);
        void Add(Status _Status);
        void Remove(int? ID);
    }
}
