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
    public class DetailsModel : PageModel
    {
        private readonly WebAppPedidos.Data.AppDbContext _context;

        public DetailsModel(WebAppPedidos.Data.AppDbContext context)
        {
            _context = context;
        }

        public PedidoPagamento PedidoPagamento { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidopagamento = await _context.Pedidos.FirstOrDefaultAsync(m => m.Id == id);

            if (pedidopagamento is not null)
            {
                PedidoPagamento = pedidopagamento;

                return Page();
            }

            return NotFound();
        }
    }
}
