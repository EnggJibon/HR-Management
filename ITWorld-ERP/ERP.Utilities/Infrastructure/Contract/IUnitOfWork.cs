using System;
using System.Data.Entity;

namespace ERP.Utilities.Infrastructure.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext DbContext { get; set; }
        void Save();
        void StartTransaction();
        void Commit();
    }
}
