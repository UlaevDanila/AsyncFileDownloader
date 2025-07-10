using System;

namespace AsyncFileDownloader;

public class FileDownloader()
{
    private string _destinationPath = "D:\\Programming\\C#\\Trainee\\AsyncFileDownloader\\AsyncFileDownloader\\Result";

    private readonly HttpClient _httpClient = new();

    public async Task DownloadFile(string url, CancellationToken cancellationToken, IProgress<int> progress)
    {
        try
        {
            var content = await _httpClient.GetByteArrayAsync(url, cancellationToken);
            await File.WriteAllBytesAsync(_destinationPath, content);
            progress?.Report(100);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine("Произошёл сбой");
            throw;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine("Введён некорретный url");
            throw;
        }
    }
}