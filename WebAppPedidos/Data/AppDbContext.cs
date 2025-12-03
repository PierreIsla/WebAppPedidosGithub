using Microsoft.EntityFrameworkCore;
using WebAppPedidos.Models; // vamos criar o modelo já a seguir

namespace WebAppPedidos.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<PedidoPagamento> Pedidos { get; set; } = default!;
    }
}