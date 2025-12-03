using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebAppPedidos.Data;
using WebAppPedidos.Models;

namespace WebAppPedidos.Pages.Pedidos
{
    public class IndexModel : PageModel
    {
        private readonly WebAppPedidos.Data.AppDbContext _context;

        public IndexModel(WebAppPedidos.Data.AppDbContext context)
        {
            _context = context;
        }

        public IList<PedidoPagamento> PedidoPagamento { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PedidoPagamento = await _context.Pedidos.ToListAsync();
        }
    }
}
