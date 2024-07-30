using TesteSmartHint.Database;
using TesteSmartHint.Libraries.Login;
using TesteSmartHint.Repositories.Contracts;
using TesteSmartHint.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TesteSmartHint.Libraries.Sessao;
using TesteSmartHint.Models.ViewModels;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();


var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

// Obtenha a string de conexão do Configuration
string connectionString = configuration.GetConnectionString("TesteSmartHintContext");

// Configure o DbContext usando a string de conexão
builder.Services.AddDbContext<TesteSmartHintContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<LoginUsuario>();
builder.Services.AddScoped<LoginCliente>();
builder.Services.AddScoped<Sessao>();
builder.Services.AddSession(options => {


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
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCookiePolicy();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{


    endpoints.MapControllerRoute(
name: "areas",
pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);
    endpoints.MapControllerRoute(
        name: "areas",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.Run();
public class MysqlEntityFrameworkDesignTimeServices : IDesignTimeServices
{
    public void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
    {
        serviceCollection.AddEntityFrameworkSqlServer();

        new EntityFrameworkRelationalDesignServicesBuilder(serviceCollection)
            .TryAddCoreServices();
    }
}