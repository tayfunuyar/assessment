# assessment
# Assessment Hakkında
Merhaba assessment için kullanılan teknolojiler,
 * .Net Core
 * AutoMapper
 * EPPlus (Excel Rapor için)
 * Mock - xUnit
 * PostgreSQL 
 * Docker 
 * Kubernetes (K8S)
 * RabbitMQ
# Proje Detayları 
  * Projede  ContactService (Rehber uygulaması ve detayları) ve ReportService (Rapor işlemleri) için  iki adet mikroservis bulunmaktadır.
  * Microservisler veritabanı olarak persistent volume claim ile oluşturulmuş node bağımsız çalışan PostgreSQL veritabanı bulunmaktadır. 
  * Contact Service ve ReportService projeler çalıştırıldığında  veritabanları auto migrate edilmektedir. 
  * Mikroservisler dockerhub'a push edilmiş olup pull edilip çalıştırabilir durumdadır.  https://hub.docker.com/u/tayfunuyar  (ContactService / ReportService)
  * Projede message broker olarak RabbitMQ kullanılmıştır. 
  * Projede master ve dev brachları olmak üzere iki adet brach bulunmaktadır.
  * ContactService'de rehber ve rehber detay işlemleri yapıldıktan sonra rapor isteğinde bulubilirsiniz. Proje çalıştıgında rapor için Contact ve ContactInformation tarafında SeedData proje çalıştıgında eklenecektir. Rapor isteği asenkron olarak çalışmakta olup ReportService e rapor isteğinde bulunmaktadır. Response olarak rapor servisinden rapor hakkında ReportUuid ve rapor durumu ile ilgili bilgiler dönmektedir. 
![Screenshot_5](https://user-images.githubusercontent.com/27923376/144755555-20a3525c-4d12-4695-88aa-b2b1b62a62eb.png) 
 * Report servis tarafında Rapor isteği yapılan endpoint Rapor hazırlanıyor talebi kaydedildikten sonra RabbitMQ'ye rapor talebi publish edilmektedir. BackgroundService RabbitMQ cumsume etmekte ve EventProcessor ile event tipine göre Excel formatında rapor hazırlayıp ReportFiles dosyasına kaydetmektedir ve rapor durumunu tamamlandı olarak güncellemektedir. 
 * Rapor dosyasına ReportService'te aşagıdaki görselde bulunan endpoint ile erişim sağlanabilmektedir.
![Screenshot_3](https://user-images.githubusercontent.com/27923376/144755782-f59f1a92-c15b-4735-9177-6ae546530d6f.png)

* Tüm raporlar ile ilgili bilgiler aşagıdaki görseldeki endpoint üzerinden alınabilmektedir.
![Screenshot_4](https://user-images.githubusercontent.com/27923376/144755844-88ae079c-28a9-4703-be3e-a965d5046907.png)
* Projenin tüm deploymentları kubernetes tarafında konfigure edilmiştir. 
* Proje deploymentları kubernetes tarafında deploy edildikten sonra ingress(acme.com windows tarafında etc/hosts dosyasına eklenmesi gerekmektedir.) kullanarak yada nodeportlar üzerinden çalıştırabileceğiniz bir yapı tasarlanmıştır. 
* RabbitMQ kubernetes tarafında deploy edilmiş olup arayüzüne girmek için default password olarak (guest/guest ) kullanılması gerekmektedir. 
* ![image](https://user-images.githubusercontent.com/27923376/144756105-3fedcbc6-feb1-4bf0-b001-15d300671c88.png)

# Proje Çalıştırılması Hakkında
K8S klasörünün bulunduğu dizinde aşagıdaki kubectl command çalıştırılarak proje RabbitMQ, PostgreSQL(PVC) ve tüm nodeportlar ile birlikte Kubernetes tarafında deploy edilebilmektedir. 
```
kubectl apply -f k8s 
```
Proje ingress ile erişmek için kubernetes tarafında ingress deploy edilmesi gerekmektedir. Deployment komutu; 
```
kubectl apply -f https://raw.githubusercontent.com/kubernetes/ingress-nginx/controller-v1.1.0/deploy/static/provider/aws/deploy.yaml
```
Her iki servis içinde bulunan nodeportlar üzerinden erişim sağlamak için aşagıdaki kubectl command çalıştırıldıktan sonra nodeportlar üzerinden direk request yapıbilmektedir. 
```
kubectl get services
```
![image](https://user-images.githubusercontent.com/27923376/144756850-cb8057db-46f5-424b-8aad-8ef75d7c059c.png)


UnitTest tarafında işyerimdeki yogunluktan dolayı ve iş teslimlerinden dolayı coverage(%60) kadar ekleme yapılamamıştır. 


 Saygılarımla,
 Tayfun,
