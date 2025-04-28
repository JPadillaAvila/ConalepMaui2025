using System.Reflection;
using ConalepMaui2025;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ConalepMaui2025.Resources.Data;
using ConalepMaui2025.Services;
using ConalepMaui2025.Repository;


public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // 1. Leer el recurso incrustado (el JSON)
        var a = Assembly.GetExecutingAssembly();
        using var stream = a.GetManifestResourceStream("ConalepMaui2025.appsettings.json");
        var config = new ConfigurationBuilder()
            .AddJsonStream(stream!)
            .Build();

        // 2. Agregar la configuración a los servicios
        builder.Configuration.AddConfiguration(config);

        // Obtener la cadena de conexión de la configuración
        var mysqlconnectionString = config.GetConnectionString("MysqlConnection");

        // Validar que la cadena de conexión no sea nula o vacía
        if (string.IsNullOrEmpty(mysqlconnectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'MysqlConnection' no está configurada en appsettings.json.");
        }

        builder.Services.AddDbContext<ApplicationDbContextMySQL>(options =>
            options.UseMySQL(mysqlconnectionString));

        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<IConfiguration>(config);
        builder.Services.AddMauiBlazorWebView();
        builder.Services.AddTransient<IRepository, Repository>();

        builder.Services.AddSingleton(new DatabaseChecker(mysqlconnectionString));

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
