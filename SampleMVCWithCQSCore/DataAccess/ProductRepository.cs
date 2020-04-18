namespace SampleMVCWithCQSCore.DataAccess
{
    using System.Threading.Tasks;
    using SampleMVCWithCQSCore.Domain;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;

    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _applicationDbContext;
            }
        }
        public ProductRepository(ApplicationDbContext applicationDbContext)
        {
            this._applicationDbContext = applicationDbContext;
        }

        public Product Add(Product product)
        {
            return _applicationDbContext.Products.Add(product).Entity;
        }

        public void Update(Product product)
        {
            _applicationDbContext.Entry(product).State = EntityState.Modified;
        }

        public async Task<Product> GetAsync(int productId)
        {
            var product = await _applicationDbContext.Products.FirstOrDefaultAsync(p => p.Id == productId);
            if (product == null)
            {
                product = _applicationDbContext.Products.Local.FirstOrDefault(p => p.Id == productId);
            }
            return product;
        }

        public Product Get(int productId)
        {
            var product = _applicationDbContext.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                product = _applicationDbContext.Products.Local.FirstOrDefault(p => p.Id == productId);
            }
            return product;
        }

        public void Remove(Product product)
        {
            _applicationDbContext.Products.Remove(product);
        }

        public bool IsNameExisted(string name)
        {
            return _applicationDbContext.Products.Any(p => p.Name == name);
        }
    }
}