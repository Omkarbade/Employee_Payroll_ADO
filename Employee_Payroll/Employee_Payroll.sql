-----CREATE DATABASE UC 1-----
CREATE DATABASE payroll_service;
use payroll_service;

-----CREATE TABLE UC2--------
CREATE TABLE employee_payroll (
ID int IDENTITY (1,1) PRIMARY KEY,
Name varchar(50),
Salary float,
StartDate Date
);

---- CREATE DATA UC3---------
INSERT into employee_payroll (name, salary, startDate) values ('Omkar',50000,'2022-01-11')
INSERT into employee_payroll (name, salary, startDate) values ('Rahul',25000,'2022-01-22')
INSERT into employee_payroll (name, salary, startDate) values ('Satish',15000,'2022-02-05')
INSERT into employee_payroll (name, salary, startDate) values ('Vishakha',45000,'2022-02-15')

-----RETRIEVE DATA UC4-------
SELECT * from employee_payroll;

-----RETRIEVE DATA FROM RANGE UC5------
SELECT salary FROM employee_payroll where name = 'Omkar'

SELECT salary FROM employee_payroll where startDate between CAST('2022-01-22' AS DATE) AND CAST('2022-02-15' AS DATE)


-----ADD GENDER UC6-----
ALTER TABLE employee_payroll ADD gender varchar(10)

UPDATE employee_payroll set gender = 'M' where id =1 
UPDATE employee_payroll set gender = 'M' where id =2 
UPDATE employee_payroll set gender = 'M' where id =3 
UPDATE employee_payroll set gender = 'F' where id =4 

-----AGGREGATE FUNCTIONS UC7----------
select sum(salary) as TotalSalary from employee_payroll;
select sum(salary) as TotalSalary,Gender from employee_payroll group by Gender;
select avg(salary) as AverageSalary,Gender from employee_payroll group by Gender;
select min(salary) as MinSalary,Gender from employee_payroll group by Gender;
select max(salary) as MaxSalary,Gender from employee_payroll where gender ='F' group by Gender;
select count(Name)as NumofEmployees,Gender from employee_payroll group by Gender;
select count(Name) as TotalEmployee,Gender from employee_payroll group by Gender;

-------CREATE ADDRESS,DEPARTMENT PHONE COLUMN UC8------
ALTER TABLE employee_payroll add phone varchar(50), address varchar(50), department varchar(50)

UPDATE employee_payroll set phone = '123456789', address = 'Mumbai', department ='Sale' where id =1
UPDATE employee_payroll set phone = '123456987', address = 'Pune', department ='Marketing' where id =2
UPDATE employee_payroll set phone = '123456879', address = 'Delhi', department ='review' where id =3
UPDATE employee_payroll set phone = '123456159', address = 'Thane', department ='Sale' where id =4

------MORE COLUMNS U9---------
ALTER TABLE employee_payroll add basic_pay decimal, deduction decimal, taxable_pay decimal, income_tax decimal, net_pay decimal 

------INSERT DATA UC10--------
insert into employee_payroll values('Mahesh','100000','05-01-2022','M','4349343434','IT','Kolkata','25000','2000','1000','200','18000');

update employee_payroll set Department = 'Marketing' where ID=5;
 
---------USING ER DIAGRAM UC11--------
 drop table employee_payroll;


 ----CREATING TABLE COMPANY-----
create table Company(
    company_id int not null PRIMARY KEY,
    company_name varchar(50) not null
);

select * from Company;

insert into Company (company_id,company_name)
 values(1001,'TCS'),(1002,'Microsoft'),(1003,'Amazon');

 -----CREATING TABLE EMPLOYEE----
 create table Employee(
  employee_id INT IDENTITY(1,1) PRIMARY KEY,
    employee_name varchar(50),
    gender char(2) not null,
    phone_no bigint,
    address VARCHAR(100)
	);
	select * from Employee;

	insert into Employee(employee_name,gender,phone_no,address)
    values('Rahul','M',739294014,'Gujrat'),('Suraj','M',5927182201,'Rajasthan'),
            ('Aaru','F',6578926910,'Lucknow'),('Vishakha','F',9134527287,'Aasam');

			------CREATE TABLE DEPARTMENT------
create table Department(
    department_id int not null PRIMARY KEY,
    department_name varchar(50) not null,
    employee_id int FOREIGN KEY REFERENCES Employee(employee_id)
	);

	insert into Department(department_id,department_name,employee_id)
    values(101,'Developer',1),(102,'IT',2),(103,'HR',3),(104,'Marketing',4);

	select * from Department;

	----CREATE TABLE PAYROLL-----
	create table Payroll(
    payroll_id varchar(50) not null PRIMARY KEY,
    basic_pay int,
    deduction int,
    taxable_pay int,
    income_tax int,
    net_pay int,
    employee_id INT FOREIGN KEY REFERENCES Employee(employee_id)
	);

	insert into Payroll(payroll_id,basic_pay,deduction,taxable_pay,income_tax,net_pay,employee_id)
    values('#11',20000,1000,19000,500,18500,1),('#22',13000,700,12300,450,11850,2),
            ('#33',35000,4000,31000,2500,28500,3),('#44',25000,1800,25200,1200,24000,4);
			select * from Payroll;

select * from Company;
select * from Department;
select * from Employee;
select * from Payroll;
------REDOING UC7--------
-------using inner joins-----------
select sum(basic_pay) as SALARY_F, avg(taxable_pay) as AVERAGE_F, 
    min(income_tax) as MINIMUM_F, max(net_pay) as MAXIMUM_F, count(payroll_id) as COUNT_PAYROLL
    from Payroll p inner join Employee e
    on p.employee_id=e.employee_id
    where e.gender = 'F' group by e.
	gender;

select sum(basic_pay) as SALARY_M, avg(taxable_pay) as AVERAGE_M, 
    min(income_tax) as MINIMUM_M, max(net_pay) as MAXIMUM_M, count(payroll_id) as COUNT_PAYROLL
    from Payroll p inner join Employee e
    on p.employee_id=e.employee_id
    where e.gender = 'M' group by e.gender;

	------UC12-------
	select * from Payroll;