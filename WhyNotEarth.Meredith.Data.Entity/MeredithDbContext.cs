namespace WhyNotEarth.Meredith.Data.Entity
{
    using System;
    using System.Linq;
    using System.Reflection;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class MeredithDbContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<Company> Companies { get; set; }
        
        public DbSet<StripeAccount> StripeAccounts { get; set; }
        
        public DbSet<StripeOAuthRequest> StripeOAuthRequests { get; set; }
        
        public MeredithDbContext(DbContextOptions<MeredithDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var configurations = typeof(MeredithDbContext).GetTypeInfo()
                .Assembly
                .GetTypes()
                .Where(t => t.GetTypeInfo().IsClass && !t.GetTypeInfo().IsAbstract
                                                    && t.GetInterfaces().Any(i => i.GetTypeInfo().IsGenericType &&
                                                                                  i.GetGenericTypeDefinition() ==
                                                                                  typeof(IEntityTypeConfiguration<>)))
                .ToList()
                .Select(Activator.CreateInstance)
                .ToList();
            foreach (dynamic configuration in configurations)
            {
                modelBuilder.ApplyConfiguration(configuration);
            }
        }
    }
}