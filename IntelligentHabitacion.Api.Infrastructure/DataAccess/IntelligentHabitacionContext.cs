using IntelligentHabitacion.Api.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;

namespace IntelligentHabitacion.Api.Infrastructure.DataAccess
{
    public sealed class IntelligentHabitacionContext : DbContext
    {
        public IntelligentHabitacionContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Code> Codes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IntelligentHabitacionContext).Assembly);
        }
    }
}
