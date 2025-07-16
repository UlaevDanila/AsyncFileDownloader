namespace AsyncFileDownloader;

public class FileDownloader()
{
    private int _pictureCounter = 0;

    private int PictureCounter
    {
        get
        {
            _pictureCounter += 1;
            return _pictureCounter;
        }
    }


    private readonly HttpClient _httpClient = new();

    public async Task DownloadFile(string url, CancellationToken cancellationToken, 
                                    IProgress<int> progress, string destinationPath)
    {
        if (!Directory.Exists(destinationPath))
        {
            Directory.CreateDirectory(destinationPath);
        }

        var pathToPicture = destinationPath + $"/picture_{PictureCounter}.png";

        try
        {
            var content = await _httpClient.GetByteArrayAsync(url, cancellationToken);
            await File.WriteAllBytesAsync(pathToPicture, content);
            progress?.Report(100);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine("Произошёл сбой");
            throw;
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine("Введён некорретный url");
        }
    }
}