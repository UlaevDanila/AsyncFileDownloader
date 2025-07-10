using System;

namespace FileDownloader;

public class FileDownloader(string url)
{
    private string _destinationPath = "D:\\Programming\\C#\\Trainee\\AsyncFileDownloader\\AsyncFileDownloader\\Result";
    private string _url = url;

    private HttpClient _httpClient = new HttpClient;

    public async Task DownloadFile(CancelationToken cancelationToken, IProgress<int> progress)
    {
        try
        {
            var content = _httpClient.GetByteArrayAsync(_url, cancelationToken);
            await File.WriteAllBytesAsync(_destinationPath, content);
            progress?.report(100);
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