using SampleMVCWithCQSCore.DataAccess;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace SampleMVCWithCQSCore.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}