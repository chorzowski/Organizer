using ProjektTest2.Data;
using System;

namespace ProjektTest2.Models
{
    public interface IUnitOfWork : IDisposable
    {
        ApplicationDbContext Context { get; }
        void Commit();
    }
}