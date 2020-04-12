using System.Data;
using SampleMVCWithCQS2Core.Domain;
using Microsoft.EntityFrameworkCore;
using SampleMVCWithCQS2Core.DataAccess.EntityConfiguration;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading.Tasks;
using System.Threading;
using MediatR;

namespace SampleMVCWithCQS2Core.DataAccess
{
    public class ApplicationDbContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "producting";
        private readonly IMediator _mediator;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IMediator mediator) : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public DbSet<Product> Products { get; set; }
    }

    public class OrderingContextDesignFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Data Source=192.168.1.81; User Id=sa; Password=12345678; Initial Catalog=ProductDb; Connect Timeout=200000;Encrypt=False;TrustServerCertificate=False;MultipleActiveResultSets=True");

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}