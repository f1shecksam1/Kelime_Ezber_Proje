### Kelime Ezber Projesi

# Tekrara Dayalı Kelime Ezber Projesi
Yazılım Yapımı Proje Ödevi
Mahmut Sami KARATEKER - Ergün ATAY - Enes Muharrem ERDOĞAN - Şeyhmus ONUR

Bu proje, Unity oyun motorunun 2022.3.13f1 versiyonu kullanılarak oluşturulmuştur.

# Projede kullanılan paketler
 - UnitySimpleFileBrowser: Kelime ekleme aşamaları için kullanılan bir dosya tarayıcı paketi.
 - TextMesh Pro: Unity'de yazı özelleştirmeleri yapılmasını sağlayan eklenti.
 - NuGetForUnity: Unity'de NuGet paketlerini kurabilmek için kullanıldı.
 - iTextSharp: Kullanıcı raporlarını PDF olarak çıktı verebilmek için kullanıldı.

# Projenin temel çalışma döngüsü
 - Proje, 6 tekrarlı ezber prensibine göre çalışmaktadır.
 - Admin ve kullanıcı olmak üzere 2 farklı giriş vardır.
 - Admin, projenin ana kelime havuzuna kelime ekleyebilir.
 - Kullanıcı, günlük sorulacak rastgele soruları ana menüde bulunan ayarlar sekmesinde kendisi belirleyebilir.
 - Kullanıcı, oyuna her gün 1 kere girebilir.
 - Kullanıcı, bildiği kelimelerin raporunu ana menüde bulunan kullanıcı analiz menüsünde görebilir.
 - Kullanıcı analiz menüsü, her aşamada bildiği toplam kelime sayılarını ayrı ayrı verir.
 - Soru sorma ekranında sorunun Türkçesi verilir ve kullanıcıdan İngilizce karşılığını girmesi beklenir.
 - Girilen verinin doğru veya yanlış olmasına bakılmaksızın kullanıcıya kelimenin örnek cümle verisi gösterilir.
 - Eğer kelimenin ses ve resim verisi veri tabanında varsa kelime tahmin yapıldıktan sonra kullanıcıya gösterilir.
 - Kullanıcı, kelimeyi doğru mu bildi yanlış mı bildiğine dair bir yazı ile bilgilendirilir.

# Arkaplanda Gerçekleşen İşlemler
 - Kullanıcı kayıt işlemlerinde 3 bilgi istenir: e-posta, şifre, kullanıcı adı.
 - Kullanıcı giriş işlemlerinde 2 bilgi istenir: kullanıcı adı, şifre.
 - Kullanıcı şifre değiştirme işlemlerinde 2 bilgi istenir ve yeni şifresini girmesi istenir: e-posta, kullanıcı adı, yeni şifre.
 - Kullanıcıları saklayan liste, kullanıcı işlemleri kodu içinde otomatik oluşturulur.
 - Kullanıcı ekleme işlemleri, kullanıcı işlemleri kodunun içinde bulunan metod sayesinde gerçekleşir ve kullanıcı ID'sini otomatik atar.
 - Kelimeleri saklayan liste, kelime işlemleri kodu içinde otomatik oluşturulur.
 - Kelime ekleme işlemleri, kelime işlemleri kodunun içinde bulunan metod sayesinde gerçekleşir ve kelime ID'sini otomatik atar.
 - Kullanıcı verileri, JSON formatında uygulamanın appdata klasörüne kaydedilir.
 - Kelime verileri, JSON formatında uygulamanın appdata klasörüne kaydedilir.
 - Kelime verileri, referans olarak ses ve resim verilerini uygulamanın appdatasına kelimenin ID adıyla birlikte kaydedilebilir.
 - Kelimeler tek bir ortak havuzda olduğu için kelimelerin ID'si, kelimenin bilinme tarihine göre kullanıcı içinde bir dictionary içinde saklanır.
 - Bu tüm kelime bilme aşamaları için geçerlidir.
 - Eğer kelime yanlış bilinmişse tüm dictionary'lere bakılır ve kelime o dictionary'lerin içindeyse kaldırılır. İçinde değilse kelimeyle ilk defa karşılaşılmıştır.

| İsterler Gerçekleştirildi | Öğrenci Beyanı                                        |
|---------------------------|-------------------------------------------------------|
| Kullanıcı Kayıt Modülü hazırladığınız yazılımda var mı?       | Evet                                  |
| Kelime ekleme modülü yazılımda var mı?                        | Evet                                  |
| Kelime sorgulama modülü (test modülü) hazırladığınız yazılımda var mı? | Evet                                  |
| Kelime sıklığı değiştirme Modülü hazırladığınız yazılımda var mı?       | Evet                                  |
| Analiz Rapor Modülü hazırladığınız yazılımda var mı?          | Evet                                  |


## Kullanıcı Kayıt Modülü Ekran Görüntüleri

![yy1](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/47eaafae-564f-4b5b-a51d-686ebb51ef87)
![yy2](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/b972543b-3a05-4b43-b80a-ed97326a4fcf)
![yy3](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/07e5d3d5-ac02-4aa4-bb39-fdf5d383a227)
![yy4](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/a12443d6-a3a2-4e61-bbd3-fe9a75684f37)

## Kelime Ekleme Modülü Ekran Görüntüleri

![yy9](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/3bbe4662-3189-4222-87fb-07caaccc4308)

## Kelime Sorgulama Modülü Ekran Görüntüleri

![yy10](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/5d8d32b1-3f95-4799-9094-9e27a0506173)

## Kelime Sıklığı Değiştirme Modülü Ekran Görüntüleri

![yy6](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/bf0205b2-82c5-458e-89f1-103f2b04227b)


## Analiz Raporu Modülü Ekran Görüntüleri

![yy7](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/3dcc9bc1-6596-4ded-a37b-1c97548753a0)
![yy11](https://github.com/f1shecksam1/Kelime_Ezber_Proje/assets/62596981/8f31de70-5d51-4cc9-80ea-80a2b7428486)


