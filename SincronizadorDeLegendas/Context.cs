using Microsoft.EntityFrameworkCore;
using SincronizadorDeLegendas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SincronizadorDeLegendas
{
    public class Context : DbContext
    {
        public DbSet<Arquivos> Arquivos { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Arquivos>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=BancoDeDados.sqlite");
        }
    }
}
