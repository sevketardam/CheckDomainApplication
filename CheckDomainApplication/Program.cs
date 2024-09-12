using HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(4) };

while (true)
{


    string currentDirectory = Directory.GetCurrentDirectory();
    string filePath = Path.Combine(currentDirectory, "domain.txt");

    if (!File.Exists(filePath))
    {
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.WriteLine($"Dosya bulunamadı: {filePath}");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Dosya Bulunamadı tekrar kontrol etmek için enter'e basınız.");
        Console.ReadLine();
        continue;
    }

    string[] domains = File.ReadAllLines(filePath);

    foreach (string domain in domains)
    {
        try
        {
            string url = domain.StartsWith("http") ? domain : $"http://{domain}";

            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                string pageContent = await response.Content.ReadAsStringAsync();

                string title = ExtractTitle(pageContent);

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{domain} açık. Title: "+ title);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{domain} kapalı.");
            }
        }
        catch (HttpRequestException e)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"{domain} kapalı veya erişilemez. Hata: {e.Message}");
        }
        catch (TaskCanceledException)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            // Timeout süresi aşılırsa hata mesajı göster
            Console.WriteLine($"{domain} isteği zaman aşımına uğradı (timeout).");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            // Diğer beklenmeyen hatalar için genel hata yönetimi
            Console.WriteLine($"{domain} kontrol edilirken hata oluştu: {ex.Message}");
        }
        finally
        {
            Console.ResetColor();
        }
    }

    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Cyan;
    Console.WriteLine("Domainleri kontrol etmek için Enter tuşuna basın veya çıkmak için 'q' yazın.");
    var input = Console.ReadLine();
    Console.ResetColor();
    // 'q' tuşuna basılırsa döngüden çık ve programı sonlandır
    if (input?.Equals("q", StringComparison.OrdinalIgnoreCase) ?? true)
    {
        break;
    }
}

static string ExtractTitle(string htmlContent)
{
    // <title> etiketinin başlangıç ve bitiş indekslerini bul
    int titleStart = htmlContent.IndexOf("<title>", StringComparison.OrdinalIgnoreCase);
    int titleEnd = htmlContent.IndexOf("</title>", StringComparison.OrdinalIgnoreCase);

    // Başlangıç ve bitiş etiketleri mevcutsa title metnini çıkar
    if (titleStart != -1 && titleEnd != -1 && titleEnd > titleStart)
    {
        // Başlangıç etiketinin uzunluğunu ekleyerek title metninin başlangıcını ayarla
        int startIndex = titleStart + "<title>".Length;
        return htmlContent[startIndex..titleEnd].Trim(); // Title metnini döndür
    }

    return string.Empty; // Title bulunamazsa null döndür
}

