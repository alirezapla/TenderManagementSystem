using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TenderManagementSystem.Domain.Models.Entities;
using TenderManagementSystem.Domain.Models.Entities.RBAC;
using TenderManagementSystem.Infrastructure.Abstractions;

namespace TenderManagementSystem.Infrastructure.Data
{
    public class AppDbContext : IdentityDbContext<
        User,
        Role,
        string,
        IdentityUserClaim<string>,
        UserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>    
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DbSet<Tender> Tenders { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Bid> Bids { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor httpContextAccessor) :
            base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                if (!typeof(IEntity).IsAssignableFrom(entityType.ClrType)) continue;
                var method = typeof(AppDbContext)
                    .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(null, new object[] {builder});
            }
            
            builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
            builder.Entity<User>().ToTable("Users");
            builder.Entity<Role>().ToTable("Roles");
        }

        public override int SaveChanges()
        {
            UpdateAuditFields();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditFields();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
            where TEntity : class, IEntity
        {
            modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
        }

        private void UpdateAuditFields()
        {
            var currentUser = _httpContextAccessor.HttpContext?.User?.Identity?.Name ?? "System";

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity &&
                            (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in entries)
            {
                var entity = (BaseEntity) entry.Entity;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedDate = DateTime.UtcNow;
                        entity.CreatedBy = currentUser;
                        entity.UpdatedDate = null;
                        entity.UpdatedBy = "";
                        entity.IsDeleted = false;
                        entity.DeletedDate = null;
                        entity.DeletedBy = "";
                        break;
                    case EntityState.Modified:
                        if (entity.IsDeleted)
                        {
                            entity.IsDeleted = true;
                            entity.DeletedDate = DateTime.UtcNow;
                            entity.DeletedBy = currentUser;
                            break;
                        }

                        entity.UpdatedDate = DateTime.UtcNow;
                        entity.UpdatedBy = currentUser;
                        break;
                }
            }
        }
    }
}

