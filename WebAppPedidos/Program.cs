using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebAppPedidos.Data;

var builder = WebApplication.CreateBuilder(args);

// ----- CONNECTION STRING DO SQL -----
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// ----- AUTENTICAÇÃO POR COOKIES -----
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";   // se não estiver autenticado, vai para /Login
        options.AccessDeniedPath = "/Login";
    });

// ----- RAZOR PAGES + REGRAS DE AUTORIZAÇÃO -----
builder.Services.AddRazorPages(options =>
{
    // tudo dentro de /Pedidos precisa de login
    options.Conventions.AuthorizeFolder("/Pedidos");

    // estas páginas podem ser acedidas sem login
    options.Conventions.AllowAnonymousToPage("/Login");
    options.Conventions.AllowAnonymousToPage("/Index");
});

// ----- DB CONTEXT -----
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// ----- PIPELINE HTTP -----
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// ordem: Authentication antes de Authorization
app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();