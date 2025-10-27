HappenCode E-Commerce API
-----------------------------------------
ASP.NET Core, Entity Framework Core ve PostgreSQL kullanılarak geliştirilmiş basit bir e-ticaret API'si.
API, Customers, Products ve Carts modellerini temel CRUD operasyonları ile destekler.
Swagger/Postman ile API test edilmiştir.

Özellikler :
-----------------------------------------
API, altı katmandan oluşur :

1) Models : Domain Katmanı / Varlıklar
Customer, Product, Cart ve CartItem veri yapılarından oluşur.
Customer, Product ve Cart için servisler ve controllerlar mevcuttur.
API uç noktaları aracılığı ile kullanıma sunulurlar.

CartItem ise, bir sepetteki tek bir ürünü temsil eder ve içinde barındırdığı ürünün ProductId'sini, ait olduğu sepetin için CartId'sini ve ürün adedini depolar. 
Sepet tarafından içindeki Ürünleri yönetmek için dahili olarak kullanılır.
API uç noktaları aracılığıyla doğrudan kullanıma sunulmaz.


2) Interfaces : Soyutlamalar
Servislerin ve Repoların test edilmesini kolaylaştırır.
İstenildiği takdirde, var olan bir Controller ile aynı arayüzü kullanan başka bir Service kullanılabilir.
Veya var olan bir Servis, aynı arayüzü kullanan başka bir Repo'yu kullanabilir.
Implementasyon detayları önemli değildir.

3) Data : EF Core Konfigürasyonu ve DB İletişimi
DbContext ve EF Core konfigürasyonunu barındırır. Veri tabanı ile olan asıl iletişimden sorumludur.
DbContext'i barındıran HappenCodeECommerceAPIContext sınıfında, Customer, Cart ve Product arasındaki ilişkiler tanımlanır.
(1 müşteri - 1 sepet)
(1 sepet - 0 veya birden çok ürün)
(1 sepet ürünü - 1 sepet)

4) Repositories : DB İletişimi
Veri tabanı ile etkileşime geçen arayüzlerin implementasyonları.
CRUD işlemlerini gerçekleştirir.
Örneğin : ProductRepository.

5) Services : İş Mantığı Katmanı
İş mantığı kurallarını implemente eder. Modellerin sahip olması gereken özelliklere sahip olduklarını garanti eder.
Örnek olarak, CustomerService, müşterinin yaşını kontrol eder, ID'nin özel olup olmadığına bakar ve bir müşteri oluşturulduğunda otomatik olarak bir sepet atar.
HTTP veya Controller ile ilgilenmez. 
Farklı repoları, gereken işlem için birlikte kullanır. Örneğin, müşteri oluştururken customerRepo ve cartRepo işbirliği yapar.

6) Controllers : API / Sunum Katmanı (Endpoint)
HTTP isteklerine yanıt verir ve istekte ne yapılacak ise işi, o servise devreder.
Servis detayları ile ilgilenmez. Sadece servisten gelen sonuca bakar.
Örneğin, ProductController'a belli bir ürünün bilgilerini güncellemek için PUT isteği gelir.
Bu PUT isteği de, isteğe uygun olan metod kullanılarak karşılanır.
ProductService'in ürün güncellemesi ile ilgilenen metodu kullanılarak ürün güncellenmeye çalışılır.
Başarılı olup olmadığına göre, istekte bulunan istemciye geribildirimde bulunur.
Ürün yoksa NotFound, ürün varsa ama bilgiler yanlış girildiyse Bad Request.
Başarılı bir şekilde ürün güncellendi ise NoContent gibi.

Daha basit bir örnek olarak, CustomerController bir GET isteği alır.
CustomerService, bir JSON döndürür. (Customer bilgilerini barındıran bir JSON)

Desteklenen İstekler
----------------------------
Customer:
- POST   : /api/Customer                - Yeni müşteri oluştur
- GET    : /api/Customer                - Tüm müşterileri getir
- GET    : /api/Customer/{id}           - Belirli müşteriyi getir
- PUT    : /api/Customer/{id}           - Müşteriyi güncelle
- DELETE : /api/Customer/{id}           - Müşteriyi sil

Product:
POST     : /api/Product                 - Yeni ürün oluştur
GET      : /api/Product                 - Tüm ürünleri getir
GET      : /api/Product/{id}            - Belirli ürünü getir
PUT      : /api/Product/{id}            - Ürünü güncelle
DELETE   : /api/Product/{id}            - Ürünü sil

Cart:
GET     : /api/Cart/{customerId}                        - Müşterinin sepetini getir
POST    : /api/Cart/{customerId}/add/{productId}        - Müşterinin sepetine ürün ekle
DELETE  : /api/Cart/{customerId}/remove/{productId}     - Müşterinin sepetinden ürün çıkar
DELETE  : /api/Cart/{customerId}/empty                  - Müşterinin sepetini boşalt

Kurulum / Kullanım
----------------------------
1) Gerekli Önkoşullar:
   - .NET 7 SDK veya üzeri
   - PostgreSQL veritabanı
   - Postman veya tarayıcı için Swagger (İsteğe bağlı, ben ikisi ile de test ettim.)

2) Veritabanı Bağlantısı:
   - appsettings.json dosyasındaki "DefaultConnection" bağlantı dizgisini kendi PostgreSQL bilgileriniz ile güncelleyin.

3) Projeyi Çalıştırma:
   - Komut satırında projenin bulunduğu klasöre gidin
   - `dotnet run` komutunu çalıştırın.
   - Varsayılan olarak API http://localhost:5127 adresinde çalışacaktır.

4) API Test Etme:
   - http://localhost:5127/ adresine giderek Swagger UI üzerinden test edebilirsiniz.
   - Swagger kullanmıyorsanız, Postman ile deneyebilirsiniz.
