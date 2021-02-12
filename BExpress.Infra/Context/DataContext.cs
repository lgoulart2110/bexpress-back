using BExpress.Core.Entidades;
using Microsoft.EntityFrameworkCore;

namespace BExpress.Infra.Context
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
    }
}
