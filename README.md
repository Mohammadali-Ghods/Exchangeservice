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
