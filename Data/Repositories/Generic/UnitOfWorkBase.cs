using Data.EFContext;
using Domain.Interfaces.Repositories.Generic;
using System.Threading.Tasks;

namespace Data.Repositories.Generic
{
    public partial class UnitOfWork : IUnitOfWork
    {
        readonly SqlServerContext _context;

        public UnitOfWork(SqlServerContext context) => _context = context;

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
