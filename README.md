# Exchange Service for digital banking
I created this microservice for every system that needs to convert currencies to each other and store transactions of this converted value in DB.
I used the techniques below and architecture for creating this microservice:
1) DDD for handling business roles for this microservice
2) CQRS
3) Caching system for better performance
4) Entity framework and SqlServer for Database
5) Logger for each part that is really sensitive
6) Unit testing for Domain part (Also using fluent assertion and Moc library for better performance)
7) MediatR
8) Fluent validation
9) Authorization and Authentication with SWT token to secure all APIs and define a role for each group of API
10) Automapper for easily mapping view models into entities and vice versa

I tried to make this microservice as clean as possible.

# How to use this system
You can use this system by setting it on docker with the command below:

docker push unipolygames/exchangeconvert:latest

After that, you can access the API swagger with this link
https://yourdomain/swagger

# Demo
You can also check the domo of this system with the link below:
https://exchange.codeandsolution.com/swagger

# Sample JWT Token for accessing APIs
Admin Role:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjczZTk3ZjJkLTRmZWQtNGU1OC1iNzdlLTc5ZWQwMmE5MDhmOCIsInJvbGUiOiJBZG1pbiIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDkvMDkvaWRlbnRpdHkvY2xhaW1zL2FjY2Vzc2lnc3RvcmVzIjoiZGNiMDA5MDUtOTAyNi00MjY2LTk0NDQtMDQ1MWU5ZWM3MzQyIiwibmJmIjoxNjY4MDg3NDE2LCJleHAiOjE2ODkxNzM4MTYsImlhdCI6MTY2ODA4NzQxNn0.h3Nmj-DfuvqymyvijJ6KB_E3Vcf_oDHSfopIA3UoTdw

Customer Role:
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjczYzg3ZjJkLTRmZWQtNGU1OC1iNzdlLTc5ZWQwMmE5MDhmOCIsInJvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDkvMDkvaWRlbnRpdHkvY2xhaW1zL2FjY2Vzc2lnc3RvcmVzIjoiZGNiMDA5MDUtOTAyNi00MjY2LTk0NDQtMDQ1MWU5ZWM3MzQyIiwibmJmIjoxNjY4MDg3NDE2LCJleHAiOjE2ODkxNzM4MTYsImlhdCI6MTY2ODA4NzQxNn0.kPmmOrgxO9V-6ISKiMTJaSDOyBc9BzHfJmYL_lO9kuw

Sharing JWT tokens, of course, is not secure but this system is just for testing and using as part of your microservices. If you have an IdentityServer or a central authorization system, you can easily replace it in code.


#Why do you use Appsetting in another location and in a specific folder?
I understand it is not normal work, but I used this word as a trick for the docker part. You can easily define volume for this specific folder and change it any time that you need, without a need to build a new image and publish it.
