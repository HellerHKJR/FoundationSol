Use tempdb
Go

-- Drop Tables and Procedure
Drop Table IF EXISTS People
Go
Drop Procedure IF EXISTS People_GetByLastName
Go
Drop Procedure IF EXISTS People_Insert
Go

-- Making temporaly DB Tables
create table People(
	id int not null,
	FirstName varchar(50) not null,
	LastName varchar(50),
	EmailAddress varchar(100),
	PhoneNumber varchar(20)
)
Go

-- insert default user in customers table
insert into People (id, firstname, lastname, emailaddress, phonenumber) values (100, N'Jilian', N'Whitaker', N'JilianW@naver.com', N'(179)376-8574')
insert into People (id, firstname, lastname, emailaddress, phonenumber) values (100, N'Brito', N'Bread', N'Brito@naver.com', N'(188)425-4523')
insert into People (id, firstname, lastname, emailaddress, phonenumber) values (100, N'Samson', N'Health', N'SamsonH@naver.com', N'(160)236-2526')
insert into People (id, firstname, lastname, emailaddress, phonenumber) values (100, N'Bo', N'Vinson', N'BoV@naver.com', N'(521)887-4613')
insert into People (id, firstname, lastname, emailaddress, phonenumber) values (100, N'Jael', N'Ramirez', N'JaelR@naver.com', N'(716)128-4568')

Go

create procedure People_GetByLastName
   @LastName nvarchar(100)
as
begin
    set nocount on;

	Select * from People where LastName = @LastName;
end
Go

create procedure People_Insert
   @FirstName nvarchar(100),
   @LastName nvarchar(100),
   @EmailAddress nvarchar(100),
   @PhoneNumber nvarchar(100)
as
begin
    set nocount on;

	Insert into (
	Select * from People where LastName = @LastName;
end
Go

select * from people;

execute dbo.People_GetByLastName 'Bread';