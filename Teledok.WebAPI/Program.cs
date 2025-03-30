namespace Teledok.WebAPI;

/// <summary>
/// ������� ����� ���������
/// </summary>
public class Program
{
    /// <summary>
    /// ������� ������� ���������
    /// </summary>
    /// <param name="args">������ ����������</param>
    private static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        host.Run();
    }

    /// <summary>
    /// �������� ��������� �����
    /// </summary>
    /// <param name="args">������ �����������</param>
    /// <returns>��������� �����</returns>
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