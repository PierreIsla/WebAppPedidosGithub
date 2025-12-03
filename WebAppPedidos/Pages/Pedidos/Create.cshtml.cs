using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebAppPedidos.Data;
using WebAppPedidos.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace WebAppPedidos.Pages.Pedidos
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public CreateModel(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public PedidoPagamento PedidoPagamento { get; set; } = default!;

        // ficheiro vindo do <input type="file" name="Documento">
        [BindProperty]
        public IFormFile? Documento { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // --- UPLOAD PARA AZURE STORAGE ---
            if (Documento != null && Documento.Length > 0)
            {
                var connectionString = _configuration["AzureStorage:ConnectionString"];
                var containerName = _configuration["AzureStorage:ContainerName"];

                var containerClient = new BlobContainerClient(connectionString, containerName);

                // cria o container se ainda não existir, com acesso público aos blobs
                await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

                // nome único para o ficheiro
                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(Documento.FileName)}";

                var blobClient = containerClient.GetBlobClient(fileName);

                using var stream = Documento.OpenReadStream();
                await blobClient.UploadAsync(stream, overwrite: true);

                // guarda o URL completo na BD
                PedidoPagamento.DocumentoUrl = blobClient.Uri.ToString();
            }

            _context.Pedidos.Add(PedidoPagamento);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}