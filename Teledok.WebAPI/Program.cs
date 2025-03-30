namespace Teledok.WebAPI;

/// <summary>
/// Главный класс программы
/// </summary>
public class Program
{
    /// <summary>
    /// Главная функция программы
    /// </summary>
    /// <param name="args">Список аргументов</param>
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }

    /// <summary>
    /// Создание строителя хоста
    /// </summary>
    /// <param name="args">Список агргументов</param>
    /// <returns>Строитель хоста</returns>
    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
                          .ConfigureWebHostDefaults(webHost =>
                          {
                              webHost.UseStartup<Startup>();
                          });
        return builder;
    }
}