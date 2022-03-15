using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll
{
    public class Program
    {
        public static void EmployeePayroll()
        {
            EmployeeRepository employeeRepository = new EmployeeRepository();
            //employeeRepository.GetEmployeeRecords();
            EmployeeModel Model = new EmployeeModel();
            Payroll payroll = new Payroll();
            Department department = new Department();
            Console.WriteLine("Add employee in database");
            Model.EmployeeId = 4;
            Model.EmployeeName = "Jayesh";
            Model.PhoneNumber = 34543787519;
            Model.Address = "Panjab";
            Model.Department = "Engineer";
            Model.Gender = 'M';
            Model.BasicPay = 20000;
            Model.Deductions = 100;
            Model.TaxablePay = 19900;
            Model.IncomeTax = 0;
            Model.StartDate = DateTime.Now;
            Model.NetPay = 19900;
            employeeRepository.AddEmployee(Model);
            Console.WriteLine("Update basic salary");
            Model.EmployeeName = "Satish";
            Model.BasicPay = 55000;
            employeeRepository.UpdateBasicPay(Model);
            Console.WriteLine("Update basic salary using prepared statement");
            Model.EmployeeName = "Mahesh";
            Model.BasicPay = 80000;
            employeeRepository.UpdateBasicPayByPreparedStatement(Model);
            Console.WriteLine("Fetch Records in Specified date");
            employeeRepository.GetEmployeeDetailsByDate();
            Console.WriteLine("Find SUM,MIN,MAX,AVG and COUNT from Database");
            employeeRepository.DatabaseFunction();
            Console.WriteLine("");
            employeeRepository.AddEmployeeToPayroll(payroll, Model, department);
            string deleteQuery = "delete from Payroll where employee_id=6;" + "delete from Department where employee_id = 6;" + "delete from Employee where employee_id = 6;";
            employeeRepository.DeleteFeomAllATables(deleteQuery);
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Employee Payroll program");
            EmployeePayroll();
            Console.ReadLine();
        }
    }
}
