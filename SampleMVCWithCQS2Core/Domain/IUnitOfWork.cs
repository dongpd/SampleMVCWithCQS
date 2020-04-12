using SampleMVCWithCQS2Core.DataAccess;
using System;
using System.Threading.Tasks;
using System.Threading;

namespace SampleMVCWithCQS2Core.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}