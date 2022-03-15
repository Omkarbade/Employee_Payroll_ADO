using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee_Payroll
{
    public class EmployeeRepository
    {
        public static string connectionString = "Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog =Employee_Payroll; Integrated Security = True; MultipleActiveResultSets = True";
        // public static string connectionString = "Data Source=ANIKET-PC;Initial Catalog=payroll_service;Integrated Security=True";
        readonly SqlConnection connection = new SqlConnection(connectionString);
        public void GetEmployeeRecords()
        {
            try
            {
                EmployeeModel model = new EmployeeModel();
                using (this.connection)
                {
                    string query = @"SELECT * from employee_payroll;";
                    SqlCommand cmd = new SqlCommand(query, connection);
                    this.connection.Open();
                    SqlDataReader sdr = cmd.ExecuteReader();
                    if (sdr.HasRows)
                    {
                        while (sdr.Read())
                        {
                            model.EmployeeId = sdr.GetInt32(0);
                            model.EmployeeName = sdr.GetString(1);
                            model.BasicPay = sdr.GetInt32(2);
                            model.StartDate = sdr.GetDateTime(3);
                            model.Gender = Convert.ToChar(sdr.GetString(4));
                            model.PhoneNumber = sdr.GetInt64(5);
                            model.Address = sdr.GetString(6);
                            model.Department = sdr.GetString(7);
                            model.Deductions = sdr.GetInt32(8);
                            model.TaxablePay = sdr.GetInt32(9);
                            model.IncomeTax = sdr.GetInt32(10);
                            model.NetPay = sdr.GetInt32(11);
                            //Print Record on Console
                            Console.WriteLine("{0},{1},{2},{3},{4},{5}", model.EmployeeId, model.EmployeeName, model.Gender, model.Department, model.Address, model.NetPay);
                            Console.WriteLine("\n");
                        }
                    }
                    else
                        Console.WriteLine("No Records in Database");
                    sdr.Close();
                    this.connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void AddEmployee(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spAddEmployeeDetails", this.connection);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Name", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    command.Parameters.AddWithValue("@Startdate", model.StartDate);
                    command.Parameters.AddWithValue("@Gender", model.Gender);
                    command.Parameters.AddWithValue("@phoneNumber", model.PhoneNumber);
                    command.Parameters.AddWithValue("@Address", model.Address);
                    command.Parameters.AddWithValue("@Department", model.Department);
                    command.Parameters.AddWithValue("@Deductions", model.Deductions);
                    command.Parameters.AddWithValue("@Taxable_Pay", model.TaxablePay);
                    command.Parameters.AddWithValue("@Income_Tax", model.IncomeTax);
                    command.Parameters.AddWithValue("@Net_Pay", model.NetPay);


                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    this.connection.Close();
                    if (result != 0)
                        Console.WriteLine("Data inserted in DataBase");
                    else
                        Console.WriteLine(result);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateBasicPay(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spUpdateEmp", this.connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Name", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);

                    this.connection.Open();
                    //Executes Sql statement to Update in DB
                    var result = command.ExecuteNonQuery();
                    this.connection.Close();
                    if (result != 0)
                        Console.WriteLine("Data updated in DB");
                    else
                        Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void UpdateBasicPayByPreparedStatement(EmployeeModel model)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("update employee_payroll set Base_Pay = @BasicPay where Name = @Name;", this.connection);
                    command.Prepare();
                    command.Parameters.AddWithValue("@Name", model.EmployeeName);
                    command.Parameters.AddWithValue("@BasicPay", model.BasicPay);
                    //Open Connection of Database
                    this.connection.Open();
                    //Executes Sql statement to Update in DB
                    var result = command.ExecuteNonQuery();
                    //Close Connection of database
                    this.connection.Close();
                    if (result != 0)
                        Console.WriteLine("Data Updated in DB");
                    else
                        Console.WriteLine(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void GetEmployeeDetailsByDate()
        {
            EmployeeModel employee = new EmployeeModel();
            DateTime startDate = new DateTime(2014, 06, 11);
            DateTime endDate = new DateTime(2016, 05, 08);
            try
            {
                this.connection.Open();
                SqlCommand sqlCommand = new SqlCommand("spGetDataByDate", this.connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StartDate", startDate);
                sqlCommand.Parameters.AddWithValue("@EndDate", endDate);
                SqlDataReader reader = sqlCommand.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        employee.EmployeeId = reader.GetInt32(0);
                        employee.EmployeeName = reader.GetString(1);
                        employee.BasicPay = reader.GetInt32(2);
                        employee.StartDate = reader.GetDateTime(3);
                        employee.Gender = Convert.ToChar(reader.GetString(4));
                        employee.PhoneNumber = reader.GetInt64(5);
                        employee.Address = reader.GetString(6);
                        employee.Department = reader.GetString(7);
                        employee.Deductions = reader.GetInt32(8);
                        employee.TaxablePay = reader.GetInt32(9);
                        employee.IncomeTax = reader.GetInt32(10);
                        employee.NetPay = reader.GetInt32(11);

                        //Display retrieved record
                        Console.WriteLine("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", employee.EmployeeId, employee.EmployeeName, employee.PhoneNumber, employee.Address, employee.Department, employee.Gender, employee.BasicPay, employee.Deductions, employee.TaxablePay, employee.IncomeTax, employee.NetPay);
                        Console.WriteLine("\n");
                    }
                }
                else
                {
                    Console.WriteLine("No record found");
                }
                reader.Close();
                this.connection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void DatabaseFunction()
        {
            try
            {
                DBFunctionOperations d = new DBFunctionOperations();
                using (this.connection)
                {
                    string queryDb = @"SELECT gender,COUNT(BasicPay) AS TotalCount,
                                   SUM(BasicPay) AS TotalSum, 
                                   AVG(BasicPay) AS AverageValue, 
                                   MIN(BasicPay) AS MinValue, 
                                   MAX(BasicPay) AS MaxValue
                                   FROM employee_payroll GROUP BY Gender;";
                    //define SqlCommand Object
                    SqlCommand cmd = new SqlCommand(queryDb, this.connection);
                    this.connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            d.Gender = Convert.ToString(dr["Gender"]);
                            d.Count = Convert.ToInt32(dr["TotalCount"]);
                            d.TotalSum = Convert.ToDouble(dr["TotalSum"]);
                            d.Avg = Convert.ToDouble(dr["AverageValue"]);
                            d.Min = Convert.ToDouble(dr["MinValue"]);
                            d.Max = Convert.ToDouble(dr["MaxValue"]);
                            Console.WriteLine("Gender: {0}, TotalCount: {1}, TotalSalary: {2}, AvgSalary:  {3}, MinSalary:  {4}, MaxSalary:  {5}", d.Gender, d.Count, d.TotalSum, d.Avg, d.Min, d.Max);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Rows does not exist");
                    }
                    dr.Close();
                    this.connection.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
            Console.WriteLine();
        }
        public void AddEmployeeToPayroll(Payroll payroll, EmployeeModel model, Department department)
        {
            try
            {
                using (this.connection)
                {
                    SqlCommand command = new SqlCommand("spAddEmpPayrollDetails", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@employee_id", model.EmployeeId = 6);
                    command.Parameters.AddWithValue("@employee_name", model.EmployeeName = "Hrutik");
                    command.Parameters.AddWithValue("@phone_no", model.PhoneNumber = 777765319);
                    command.Parameters.AddWithValue("@address", model.Address = "Denmark");
                    command.Parameters.AddWithValue("@gender", model.Gender = Convert.ToChar("M"));
                    command.Parameters.AddWithValue("@payroll_Id", payroll.payrollId = "#5555");
                    command.Parameters.AddWithValue("@basic_pay", payroll.basicPay = 200000);
                    command.Parameters.AddWithValue("@deduction", payroll.deductions = 10000);
                    command.Parameters.AddWithValue("@income_tax", payroll.incometax = 5000);
                    command.Parameters.AddWithValue("@taxable_pay", payroll.taxablepay = 190000);
                    command.Parameters.AddWithValue("@net_pay", payroll.NetPay = 185000);
                    command.Parameters.AddWithValue("@department_Id", department.departmentId = 105);
                    command.Parameters.AddWithValue("@department_name", department.departmentName = "Fullstack");

                    this.connection.Open();
                    int result = command.ExecuteNonQuery();
                    if (result != 0)
                    {
                        Console.WriteLine("Data added");
                    }
                    else
                    {
                        Console.WriteLine("Unsuccessful");
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.connection.Close();
            }
        }
        public void DeleteFeomAllATables(string q)
        {
            try
            {
                Payroll Payroll = new Payroll();
                using (this.connection)
                {
                    string query = q;

                    //define SqlCommand Object
                    SqlCommand cmd = new SqlCommand(query, this.connection);
                    //establish connection
                    this.connection.Open();
                    Console.WriteLine("Connected");
                    cmd.ExecuteReader();
                    Console.WriteLine("Data deleted successfully");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                //close connection
                this.connection.Close();
            }
        }
    }
}
