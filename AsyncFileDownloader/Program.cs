using System;

namespace AsyncFileDownloader;

public class Programm
{
    public static async Task Main()
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        CancellationToken cancellationToken = cancellationTokenSource.Token;
        
        var resources = new SemaphoreSlim(1, 3);
        
        var fileDownloader = new FileDownloader();

        var destinationPath = "D:\\Programming\\C#\\Trainee\\AsyncFileDownloader\\AsyncFileDownloader\\Result";

        var progress = new Progress<int>(percent => Console.WriteLine($"Прогресс: {percent}%"));
        
        IEnumerable<string> urls = new List<string>()
        {
            "https://avatars.mds.yandex.net/i?id=facefb9030d166f517d8c336d12baa59_l-4928705-images-thumbs&n=13",
            "https://avatars.mds.yandex.net/i?id=e86e2a05309e87b8433f11f5400aeb48_l-9456276-images-thumbs&n=13",
            "https://surface-pro.ru/wp-content/uploads/2023/11/Windows-12-concept-art-Microsoft-2158645-wallhere.com_.png",
            "weenvivj2ivpi2vib"
        };
        
        var tasks = urls.Select<string, Task>(async url =>
        {
            await resources.WaitAsync(cancellationToken);
            try
            {
                await fileDownloader.DownloadFile(url, cancellationToken, progress, destinationPath);
            }
            finally
            {
                resources.Release();
            }
        });

        await Task.WhenAll(tasks);
    }
}