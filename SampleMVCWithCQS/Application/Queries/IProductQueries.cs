namespace SampleMVCWithCQS.Application.Queries 
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductQueries
    {
        Task<Product> GetProductAsync(int id);

        Task<List<Product>> GetAllProducts();
    }
}