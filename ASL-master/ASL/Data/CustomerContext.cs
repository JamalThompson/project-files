using ASL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Customers_page.Data
{
    public class CustomerContext
    { 
        public string ConnectionString { get; set; }

        public CustomerContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public async Task InsertCustomerAsync(Customer Customer)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO Customer (Id, Name, Address1, BillCycle, Contact_Name, Contact_Address, Address2, City, State, Zip, Phone_Number) VALUES (@Id, @Name, @Address1, @BillCycle, @Contact_Name, @Contact_Address, @Address2, @City, @State, @Zip, @Phone_Number);", conn);
                BindParams(cmd, Customer);
                await cmd.ExecuteNonQueryAsync();
            }
        }
        public Customer  FindOneAsync(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                Customer customer = new Customer();
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT `Id`, `Name`, `Address1`, `BillCycle`, `Contact_Name`,`Contact_Address`, `Address2`, `City`, `State`, `Zip`, `Phone_Number`  FROM `Customer` WHERE `Id` = @Id;", conn);
                
                cmd.Parameters.Add(new MySqlParameter
                {
                    ParameterName = "@Id",
                    DbType = DbType.Int32,
                    Value = Id,
                });
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    customer.Id = Convert.ToInt32(reader["Id"].ToString());
                    customer.Name = reader["Name"].ToString();
                    customer.Address1 = reader["Address1"].ToString();
                    customer.Contact_Name = reader["Contact_Name"].ToString();
                    customer.BillCycle = Convert.ToInt32(reader["BillCycle"].ToString());
                    customer.Contact_Address = reader["Contact_Address"].ToString();
                    customer.Address2 = reader["Address2"].ToString();
                    customer.City = reader["City"].ToString();
                    customer.State = reader["State"].ToString();
                    customer.Zip = reader["Zip"].ToString();
                    customer.Phone_Number = reader["Phone_Number"].ToString();

                }
                return customer;
              //await cmd.ExecuteReaderAsync();
                //return result.Count > 0 ? result[0] : null;
            }
        }

        public List<Customer> FindAll()
        {
            List<Customer> customers = new List<Customer>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `Customer`;", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Customer customer = new Customer();
                    customer.Id = Convert.ToInt32(reader["Id"].ToString());
                    customer.Name = reader["Name"].ToString();
                    customer.Address1 = reader["Address1"].ToString();
                    int billCycle;
                    Int32.TryParse(reader["BillCycle"].ToString(), out billCycle);
                    customer.BillCycle = billCycle;
                    customer.Contact_Name = reader["Contact_Name"].ToString();
                    customer.Contact_Address = reader["Contact_Address"].ToString();
                    customer.Address2 = reader["Address2"].ToString();
                    customer.City = reader["City"].ToString();
                    customer.State = reader["State"].ToString();
                    customer.Zip = reader["Zip"].ToString();
                    customer.Phone_Number = reader["Phone_Number"].ToString();
                    customers.Add(customer);
                }
                return customers;
            }
        }




        public async Task UpdateAsync(Customer customer)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(@"UPDATE `Customer` SET `Name` = @Name, `Address1` = @Address1, `BillCycle` = @BillCycle, `Contact_Name` = @Contact_Name,`Contact_Address` = @Contact_Address, `Address2` = @Address2, `City` = @City, `State` = @State,  `Zip`= @Zip WHERE `Id` = @Id, `Phone_Number` = @Phone_Number", conn);
                dataAdapter.UpdateCommand = cmd;
                BindParams(cmd, customer);
                BindId(cmd,  customer.Id);
                conn.Open();
                await dataAdapter.UpdateCommand.ExecuteNonQueryAsync();
            }

        }

        public void Delete(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                //get data for data adapter
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(@"update `Expenses` set `IsActive` = false WHERE `Id` = @Id", conn);
                dataAdapter.UpdateCommand = cmd;
                BindId(dataAdapter.UpdateCommand, Id);
                conn.Open();
                dataAdapter.UpdateCommand.ExecuteNonQuery();
            }

        }

        private void BindId(MySqlCommand cmd, int Id)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = Id,
            });
        }

        public async Task GetLastServiceDateAsync(Customer Customer)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"select End_Date from Service where Id= @Id order by End_Date desc limit(1);", conn);
                BindParams(cmd, Customer);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        private void BindParams(MySqlCommand cmd, Customer Customer )
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.String,
                Value = Customer.Id,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Name",
                DbType = DbType.String,
                Value = Customer.Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Address1",
                DbType = DbType.String,
                Value = Customer.Address1,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@BillCycle",
                DbType = DbType.String,
                Value = Customer.BillCycle,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Contact_Name",
                DbType = DbType.String,
                Value = Customer.Contact_Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Contact_address",
                DbType = DbType.String,
                Value = Customer.Contact_Address,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Address2",
                DbType = DbType.String,
                Value = Customer.Address2,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@City",
                DbType = DbType.String,
                Value = Customer.City,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@State",
                DbType = DbType.String,
                Value = Customer.State,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Zip",
                DbType = DbType.String,
                Value = Customer.Zip,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Phone_Number",
                DbType = DbType.String,
                Value = Customer.Phone_Number,
            });
           
        }
    }
}
