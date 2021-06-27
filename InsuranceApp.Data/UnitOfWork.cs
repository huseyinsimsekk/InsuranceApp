using InsuranceApp.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceApp.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        public MainContext MainContext { get; set; }
        public UnitOfWork(MainContext mainContext)
        {
            MainContext = mainContext;
        }
        public void Commit()
        {
            MainContext.SaveChanges();
        }

        public DbSet<T> GetEntity<T>() where T : class
        {
            return MainContext.Set<T>();
        }
    }
}
