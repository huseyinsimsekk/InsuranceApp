using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Core.Contracts
{
    public interface IUnitOfWork
    {
        DbSet<T> GetEntity<T>() where T : class;
        void Commit();
    }
}
