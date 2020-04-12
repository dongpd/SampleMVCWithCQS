using SampleMVCWithCQS2Core.DataAccess;
using System;
using System.Threading.Tasks;


namespace SampleMVCWithCQS2Core.Domain
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}