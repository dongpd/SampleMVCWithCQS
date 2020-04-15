using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MediatR;

using SampleMVCWithCQS2Core.DataAccess.EntityConfiguration;
using SampleMVCWithCQS2Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace SampleMVCWithCQS2Core.DataAccess
{
    public class ApplicationDbContext : IdentityDbContext<User>, IUnitOfWork
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
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return true;
        }

        public DbSet<Product> Products { get; set; }
    }
}