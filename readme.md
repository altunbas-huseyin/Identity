1. Mongo DB docker da olu�turulur
docker run --name mongo-server -p 27017:27017 -d mongo:2.4 --auth

2. olu�turulan mongo-server i�ine admin olarak ba�lan�l�r
docker exec -it mongo-server mongo admin

3. Database admin user olu�turulur.
db.createUser( { user: "Huso",
          pwd: "Huso7474",
          roles: [ "userAdminAnyDatabase",
                   "dbAdminAnyDatabase",
                   "readWriteAnyDatabase"

] } )

4. Bir database olu�turulut �rn. Identity ve buna bir user tan�mlan�r.
�rnek user olu�turma resmi : 
![alt text](readme/mongo-user.PNG)