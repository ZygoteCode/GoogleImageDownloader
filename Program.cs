using System.Text.RegularExpressions;

class Program
{
    static async Task Main()
    {
        Console.Write("Please, insert here the link of the Google User Content to download: ");
        string url = Console.ReadLine();

        if (IsValidGoogleUserContentUrl(url, out string modifiedUrl))
        {
            Console.WriteLine("Downloading the original and the extended images, please wait a while until the operation is completed...");

            await DownloadImage(url, "original_image.png");
            await DownloadImage(modifiedUrl, "extended_image.png");
            
            Console.WriteLine("The images has been downloaded succesfully.");
        }
        else
        {
            Console.WriteLine("The inserted URL is not valid.");
        }

        Console.WriteLine("Press the ENTER key in order to exit from the program.");
        Console.ReadLine();
    }

    static bool IsValidGoogleUserContentUrl(string url, out string modifiedUrl)
    {
        modifiedUrl = string.Empty;

        if (Uri.TryCreate(url, UriKind.Absolute, out Uri uri) && uri.Host.Contains("googleusercontent.com"))
        {
            modifiedUrl = Regex.Replace(url, "s(32|64)", "s2048");
            return true;
        }

        return false;
    }

    static async Task DownloadImage(string url, string fileName)
    {
        using HttpClient client = new HttpClient();

        try
        {
            byte[] imageBytes = await client.GetByteArrayAsync(url);
            await File.WriteAllBytesAsync(fileName, imageBytes);
        }
        catch
        {

        }
    }
}
