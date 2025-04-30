using System.Reflection;
using ConalepMaui2025;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ConalepMaui2025.Resources.Data;
using ConalepMaui2025.Services;
using ConalepMaui2025.Repository;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

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

        if (string.IsNullOrEmpty(mysqlconnectionString))
        {
            throw new InvalidOperationException("La cadena de conexión 'MysqlConnection' no está configurada en appsettings.json.");
        }

        // Configurar Entity Framework
        builder.Services.AddDbContext<ApplicationDbContextMySQL>(options =>
            options.UseMySQL(mysqlconnectionString));

        // Agregar servicios principales
        builder.Services.AddSingleton<IConfiguration>(config);
        builder.Services.AddTransient<IRepository, Repository>();
        builder.Services.AddSingleton(new DatabaseChecker(mysqlconnectionString));

        // Configurar Blazor y Blazorise
        // Configurar Blazor y Blazorise
        builder.Services.AddMauiBlazorWebView();

        builder.Services
            .AddBlazorise(options => { options.Immediate = true; })
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();

      
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

#if DEBUG
        builder.Services.AddBlazorWebViewDeveloperTools();
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
