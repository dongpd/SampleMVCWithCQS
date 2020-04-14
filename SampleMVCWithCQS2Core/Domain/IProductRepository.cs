using SampleMVCWithCQS2Core.DataAccess;
using System;
using System.Threading.Tasks;


namespace SampleMVCWithCQS2Core.Domain
{
    public interface IProductRepository : IRepository
    {
        Product Add(Product product);

        void Update(Product product);

        void Remove(Product product);

        Task<Product> GetAsync(int productId);

        Product Get(int productId);

        bool IsNameExisted(string name);
    }
}