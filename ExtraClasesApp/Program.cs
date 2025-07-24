using ExtraClasesApp.Data;
using ExtraClasesApp.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

#region 🔧 Configuración de Servicios

// 1️⃣ Base de datos PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 2️⃣ Identity para autenticación de usuarios
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddEntityFrameworkStores<AppDbContext>();

// 3️⃣ Razor Pages y MVC
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();

// 4️⃣ Configuración de envío de correo electrónico
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddScoped<EmailSenderService>();

// 5️⃣ Servicio de notificación automática de clases
builder.Services.AddHostedService<ClaseExtraNotificationService>();

// 6️⃣ Evitar que la app se caiga por errores en el BackgroundService
builder.Services.Configure<HostOptions>(options =>
{
    options.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
});

#endregion

var app = builder.Build();

#region 🌐 Middleware y Pipeline HTTP

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

#endregion

#region 📍 Rutas

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tutores}/{action=Index}/{id?}");

app.MapRazorPages();

#endregion

app.Run();
