using SampleMVCWithCQSCore.DataAccess;
using System;
using System.Threading.Tasks;


namespace SampleMVCWithCQSCore.Domain
{
    public interface IRepository
    {
        IUnitOfWork UnitOfWork { get; }
    }
}