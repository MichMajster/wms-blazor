using MagazynApp.Application.Interfaces;
using MagazynApp.Blazor.Auth;
using MagazynApp.Blazor.Components;
using MagazynApp.Infrastructure.Data;
using MagazynApp.Infrastructure.Repositories;
using MagazynApp.Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// pobranie Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Nie znaleziono ci¹gu po³¹czenia 'DefaultConnection'.");

// rejestracja AppDbContext
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IMagazynRepository, MagazynRepository>();
builder.Services.AddScoped<ILokalizacjaRepository, LokalizacjaRepository>();
builder.Services.AddScoped<IPrzedmiotRepository, PrzedmiotRepository>();
builder.Services.AddScoped<IKontrahentRepository, KontrahentRepository>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();


//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//    .AddCookie(options =>
//    {
//        options.Cookie.Name = "MagazynApp.Auth";
//        options.LoginPath = "/login"; // Œcie¿ka do strony logowania
//        options.LogoutPath = "/logout"; // Œcie¿ka do wylogowania
//        options.AccessDeniedPath = "/access-denied"; // Œcie¿ka do strony odmowy dostêpu
//    });
//builder.Services.AddAuthorizationCore();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState(); // Wymagane dla Blazor w .NET 8
builder.Services.AddScoped<AuthService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();
app.UseAuthentication();
app.UseAuthorization();

// --- SEEDING DOMYŒLNEGO ADMINISTRATORA ---
using (var scope = app.Services.CreateScope())
{
    var authService = scope.ServiceProvider.GetRequiredService<AuthService>();

    // Tworzy domyœlnego admina, jesli zadne konto jeszcze nie istnieje
    await authService.RegisterAsync("admin", "Admin123!", role: "Admin");
}

app.Run();
