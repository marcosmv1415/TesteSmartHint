using TesteSmartHint.Models;
using Microsoft.EntityFrameworkCore;

namespace TesteSmartHint.Database
{
    public class TesteSmartHintContext : DbContext
    {
        public TesteSmartHintContext(DbContextOptions<TesteSmartHintContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

    }
}
