
use Payroll_service;

Create procedure spGetEmployee
As 
Begin
Try
Select * from Employee_Payroll;
End Try
Begin catch 
Select 
 ERROR_NUMBER() AS ErrorNumber,
ERROR_MESSAGE() AS ErrorMessage;
End Catch

Exec spGetEmployee 


Alter procedure InsertEmployeeData(
@Name varchar(100),
@Salary Float,
@StartDate Date,
@Gender varchar(10),
@Phone varchar(50),
@address varchar(50),
@department varchar(50),
@basic_Pay Decimal,
@deduction Decimal,
@taxable_pay Decimal,
@income_tax decimal,
@net_pay decimal
)
As
begin 
try
insert into Employee_Payroll values(@Name,@Salary,@StartDate,@Gender,@Phone,@Address,@Department,@basic_pay,@Deduction,@taxable_pay,@income_tax,@net_pay)
End try
Begin catch 
Select 
 ERROR_NUMBER() AS ErrorNumber,
ERROR_MESSAGE() AS ErrorMessage;
End Catch

Exec InsertEmployeeData  @Name ='Mahesh' ,@Salary ='100000',@StartDate ='05-01-2022',@Gender ='M',@Phone ='4349343434',@Address ='Kolkata',@Department ='IT',@basic_pay ='25000',@Deduction ='2000',@taxable_pay ='1000',@income_tax ='200',@net_pay ='18000'


Create procedure DeleteEmpByName
(
	@Name varchar(50)
)
As 
Begin
Try
DELETE FROM employee_payroll where Name = @Name
End Try
Begin catch 
Select 
 ERROR_NUMBER() AS ErrorNumber,
ERROR_MESSAGE() AS ErrorMessage;
End Catch

Exec DeleteEmpByName @Name = 'Omkar'

create Procedure UpdateEmployeDetails(
@Salary float,
@Name varchar(50)
)
As
Begin
Begin try
Update employee_payroll set Salary=@Salary where Name=@Name
End try
Begin catch
select
ERROR_NUMBER() AS ErrorNumber,
ERROR_MESSAGE() AS ErrorMessage;
End catch
End
Exec UpdateEmployeDetails @Salary=55000.00 , @Name='Satish'


create procedure GetlengthBtwId
As
Begin
Try
Select * from employee_payroll where Id BETWEEN 2 and 5;
End Try
Begin catch
Select
ERROR_NUMBER() AS ErrorNumber,
ERROR_MESSAGE() AS ErrorMessage;
End Catch


Exec GetlengthBtwId














Create procedure AggregateEmp
As
Begin
begin try
select SUM(BasicPay) as "Total Salary",Gender from employee_payroll group by Gender
end try
begin catch
select
ERROR_NUMBER() AS ErrorNumber,
ERROR_MESSAGE() AS ErrorMessage;
end catch
end

Exec AggregateEmp



Create function Fun_EmployeeInformation()      
returns table       
as      
return(select * from employee_payroll )    

 select * from employee_payroll


create function fun_JoinEmpColumnInfo()  
(  
   @Name varchar(50),  
   @department varchar(50),  
   @address varchar(50) 
)  
returns varchar(100) 
as  
begin return(select @Name +' '+@department+' '+@address)
end

select dbo.fun_JoinEmpColumnInfo(name,department,phone) as INFO from employee_payroll 