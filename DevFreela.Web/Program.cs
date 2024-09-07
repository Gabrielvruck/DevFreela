using DevFreela.Web.Controllers;

var builder = WebApplication.CreateBuilder(args);

// Configurar serviços para MVC e API
builder.Services.AddControllersWithViews();  // Adiciona suporte a views (MVC)
builder.Services.AddControllers();          // Adiciona suporte para controllers de API
builder.Services.AddHttpClient("ApiVruck", client =>
{
    client.BaseAddress = new Uri("https://localhost:7028/api/");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Mapear os endpoints de API
app.MapControllers();   // Para os controllers de API

app.Run();
