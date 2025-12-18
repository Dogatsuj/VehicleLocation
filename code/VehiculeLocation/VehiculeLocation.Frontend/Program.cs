// MonProjet.WebClient/Program.cs

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// On ajoute le support pour les Pages Razor ou MVC.
builder.Services.AddRazorPages();
// Si vous utilisez l'architecture MVC (Contrôleurs et Vues):
// builder.Services.AddControllersWithViews(); 

// --- Configuration HttpClient (Pour appeler l'API Back-end) ---
builder.Services.AddHttpClient("ApiBackend", client =>
{
    // ATTENTION : Assurez-vous que l'URL corresponde au port réel 
    // de votre API (MonProjet.API). Par exemple, "http://localhost:5076".
    client.BaseAddress = new Uri("http://localhost:5076/");
    // Vous pouvez aussi ajouter des en-têtes par défaut ici, comme l'Accept pour le JSON.
    // client.DefaultRequestHeaders.Add("Accept", "application/json"); 
});

builder.Services.AddHttpClient("ApiBackend", client =>
{
    client.BaseAddress = new Uri("http://localhost:5076/");

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Mappage pour les Pages Razor / Active les routes automatiques
app.MapRazorPages();


app.Run();