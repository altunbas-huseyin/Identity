1. Mongo DB docker da oluþturulur <br />
docker run --name mongo-server -p 27017:27017 -d mongo:2.4 --auth

2. oluþturulan mongo-server içine admin olarak bağlanılır <br />
docker exec -it mongo-server mongo admin

3. Database admin user oluşturulur. <br />
db.createUser( { user: "Huso",
          pwd: "Huso7474",
          roles: [ "userAdminAnyDatabase",
                   "dbAdminAnyDatabase",
                   "readWriteAnyDatabase"

] } )

4. Bir database oluşturulur örn. Identity ve buna bir user tanımlanır. örnek user oluþturma resmi : <br /> 
![alt text](readme/mongo-user.PNG)
<br>
<br>
5. Docker işlemleri <br>
 Örnek Dockerfile dosyasına <a href="/Identity/Identity/Dockerfile"> /Identity/Identity/Dockerfile</a> yolundan ulaşabilirsiniz. 
<br>

docker imajı hazırlar <br>
docker build -t identity-app . <br>

hazırlanan docker imajından bir örnek/container oluşturur <br>
docker run -it -p 5000:5000 identity-app <br>

ve mutlu son  <b>docker start identity-app</b> komutu ile kontainer ayağa kaldırılır.
