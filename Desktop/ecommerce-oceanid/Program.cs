using prototipo1204.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using prototipo1204.Repositorios.Interface;
using prototipo1204.Repositorios;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Microsoft.AspNetCore.Http;


var builder = WebApplication.CreateBuilder(args);

// Configurar o Entity Framework Core para usar MySQL
builder.Services.AddDbContext<oceanidDBContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("prototipoBanco"),
        new MySqlServerVersion(new Version(8, 0, 31))
    )
);



// Autenticação com cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login";
    });

// Acesso ao contexto HTTP
builder.Services.AddHttpContextAccessor();

// Sessão
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// MVC
builder.Services.AddControllersWithViews();

// Repositórios
builder.Services.AddScoped<ILoginRepositorio, LoginRepositorio>();
//builder.Services.AddScoped<ICarrinhoService, CarrinhoService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();



app.UseAuthentication();   // Autenticação
app.UseSession();          // Ativando a sessão
app.UseAuthorization();    // Autorização


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

