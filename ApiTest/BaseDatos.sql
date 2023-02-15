--CREATE DATABASE bank;

--use bank

CREATE TABLE PERSON(
personId int primary key identity(1,1),
name varchar(45),
gender varchar(45),
age varchar(45),
identification varchar(45),
address varchar(45),
phone varchar(45),

)

CREATE TABLE CLIENT(
clientId int primary key identity(1,1),
Password varchar(45),
State varchar(45),
personIdFK INT,
CONSTRAINT FK_IDPERSON FOREIGN KEY (personIdFK) references PERSON(personId)

)

CREATE TABLE ACCOUNT(
accountId int primary key identity(1,1),
number varchar(45),
type varchar(45),
initialBalance int,
state varchar(45),
clientIdFK INT,
CONSTRAINT FK_IDCLIENT FOREIGN KEY (clientIdFK) references CLIENT(clientId)

)

CREATE TABLE TRASNSACTION(
transactionId int primary key identity(1,1),
type varchar(45),
balance varchar(45),
value varchar(45),
dateTransaction Date,
accountIdFK INT,
CONSTRAINT FK_IDACCOUNT FOREIGN KEY (accountIdFK) references ACCOUNT(accountId)

)

insert into PERSON(name,gender,address,identification,phone) values ('MARX','MALE','25','calle 1', '1238524', '58412')
insert into CLIENT(Password, State, personIdFK) values ('123','true',1)

select * from PERSON
select * from CLIENT