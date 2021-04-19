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
        public DbSet<Home> Homes { get; set; }
        public DbSet<MyFood> MyFoods { get; set; }
        public DbSet<CleaningSchedule> CleaningSchedules { get; set; }
        public DbSet<CleaningTasksCompleted> CleaningTasksCompleteds { get; set; }
        public DbSet<CleaningRating> CleaningRatings { get; set; }
        public DbSet<CleaningRatingUser> CleaningRatingUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder is null)
                throw new ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IntelligentHabitacionContext).Assembly);
        }
    }
}
