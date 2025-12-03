using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppPedidos.Data;
using WebAppPedidos.Models;

namespace WebAppPedidos.Pages.Pedidos
{
    public class EditModel : PageModel
    {
        private readonly WebAppPedidos.Data.AppDbContext _context;

        public EditModel(WebAppPedidos.Data.AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PedidoPagamento PedidoPagamento { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidopagamento =  await _context.Pedidos.FirstOrDefaultAsync(m => m.Id == id);
            if (pedidopagamento == null)
            {
                return NotFound();
            }
            PedidoPagamento = pedidopagamento;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PedidoPagamento).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoPagamentoExists(PedidoPagamento.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool PedidoPagamentoExists(int id)
        {
            return _context.Pedidos.Any(e => e.Id == id);
        }
    }
}
