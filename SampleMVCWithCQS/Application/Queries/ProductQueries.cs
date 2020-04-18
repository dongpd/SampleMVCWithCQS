namespace SampleMVCWithCQS.Application.Queries
{
    using System.Collections.Generic;
    using System.Data.SQLite;
    using System.Threading.Tasks;
    using System;
    using Dapper;
    using SampleMVCWithCQSCore.Domain;
    using System.Linq;

    public class ProductQueries : IProductQueries
    {
        private string _connectionString = string.Empty;

        public ProductQueries(string constr)
        {
            _connectionString = !string.IsNullOrWhiteSpace(constr) ? constr : throw new ArgumentNullException(nameof(constr));
        }

        public async Task<List<Product>> GetAllProducts()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var results = await connection.QueryAsync<dynamic>(
                    @"SELECT Id, Name, Category, Price, Color, InStock
                     FROM Products");

                if (!results.AsList().Any())
                {
                    throw new KeyNotFoundException();
                }

                return MapOrderItems(results);
            }
        }

        public async Task<Product> GetProductAsync(int id)
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                var result = await connection.QueryAsync<dynamic>(
                    @"SELECT Id, Name, Category, Price, Color, InStock
                    FROM Products p
                    WHERE p.Id = @id", new { id });

                if (!result.AsList().Any())
                {
                    throw new KeyNotFoundException();
                }

                return MapOrderItem(result);
            }
        }

        private Product MapOrderItem(dynamic result)
        {
            var product = new Product
            {
                Id = Convert.ToInt32(result[0].Id),
                Name = result[0].Name,
                Category = result[0].Category,
                Price = Convert.ToDecimal(result[0].Price),
                Color = ((Colors)(result[0].Color)).ToString(),
                InStock = result[0].InStock != 0
            };

            return product;
        }

        private List<Product> MapOrderItems(dynamic results)
        {
            var products = new List<Product>();
            foreach (dynamic result in results)
            {
                var product = new Product
                {
                    Id = Convert.ToInt32(result.Id),
                    Name = result.Name,
                    Category = result.Category,
                    Price = Convert.ToDecimal(result.Price),
                    Color = ((Colors)(result.Color)).ToString(),
                    InStock = result.InStock != 0
                };
                products.Add(product);
            }
            return products;
        }
    }
}