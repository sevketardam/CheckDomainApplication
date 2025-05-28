# Domain Status Checker

Basit bir C# konsol uygulaması. `domain.txt` dosyasındaki domain adreslerini okuyarak her birine HTTP isteği gönderir ve sitenin durumunu kontrol eder. Eğer site açıksa, sayfanın `<title>` etiketi içeriğini ekranda gösterir.

---

## Özellikler

- `domain.txt` dosyasındaki domain listesini okur.
- Her domain için HTTP GET isteği yapar.
- Başarılı bağlantılarda sayfanın başlık (`<title>`) bilgisini alır ve gösterir.
- Erişim sağlanamayan veya kapalı olan domainleri kırmızı renkte belirtir.
- Timeout süresi 4 saniye olarak ayarlanmıştır.
- İşlemler paralel olarak çalışır.
- Kullanıcıdan giriş ile döngüyü devam ettirir veya 'q' ile çıkış yapılabilir.

---

## Kullanım

1. Projeyi klonlayın veya kodları kendi projenize ekleyin.
2. Proje dizinine `domain.txt` adında bir dosya oluşturun.
3. `domain.txt` içerisine kontrol etmek istediğiniz domainleri her satıra bir tane olacak şekilde yazın. Örnek:
    ```
    google.com
    example.com
    http://github.com
    ```
4. Uygulamayı çalıştırın:
    ```bash
    dotnet run
    ```
5. Domainler kontrol edilir, sonuçlar konsola renkli olarak yazdırılır.
6. Tekrar kontrol için Enter tuşuna basın.
7. Çıkmak için 'q' yazıp Enter'a basın.

---

## Notlar

- Domainler HTTP protokolü ile kontrol edilir. Eğer domain `http` veya `https` ile başlamıyorsa, otomatik olarak `http://` eklenir.
- Timeout 4 saniyedir. Daha uzun sürebilecek isteklerde `TaskCanceledException` ile timeout mesajı verilir.
- Hatalı veya erişilemeyen domainlerde hata mesajı gösterilir.

---

## Geliştirme

- İstekler `HttpClient` kullanılarak asenkron ve paralel şekilde yapılmaktadır.
- Başlık çıkarma işlemi basit bir string arama ile yapılır.
- Daha gelişmiş HTML parsing için `HtmlAgilityPack` veya benzeri kütüphaneler entegre edilebilir.

---

## Lisans

Bu proje MIT Lisansı ile lisanslanmıştır. Detaylar için `LICENSE` dosyasına bakınız.

---

Şevket Arda tarafından geliştirilmiştir.
