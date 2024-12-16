namespace AssetAtlasApi {
    using AssetAtlasApi.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class AtlasDbContext : DbContext {

        public AtlasDbContext(DbContextOptions<AtlasDbContext> options) : base(options) {
        }

        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Income> Incomes { get; set; }
    }
}
