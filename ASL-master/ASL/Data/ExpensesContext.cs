using ASL.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Data
{
    public class ExpensesContext
    {
        public string ConnectionString { get; set; }

        public ExpensesContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public async Task InsertExpensesAsync(Expenses Expenses)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO Expenses (Name, Description) VALUES (@Name, @Description);", conn);
                BindParams(cmd, Expenses);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task InsertExpenseTypeAsync(ExpenseType expenseType)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO ExpenseType (Type, Total, SvcType) VALUES (@Type, @Total, @SvcType);", conn);
                BindExpenseTypeParams(cmd, expenseType);
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public Expenses FindOneAsync(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
           
                MySqlCommand cmd = new MySqlCommand(@"SELECT `Id`, `Type`, `Total`, `SvcId`, `SvcType` FROM `Expenses` WHERE `Id` = @Id;", conn);
                BindId(cmd, id);
                MySqlDataReader reader = cmd.ExecuteReader();

             
                Expenses expenses = new Expenses();
                while (reader.Read())
                {
                   
                    expenses.Id = Convert.ToInt32(reader["Id"].ToString());
                    expenses.Type = reader["Type"].ToString();
                    expenses.Total = Convert.ToDecimal(reader["Total"].ToString());
                    int svcid;
                    Int32.TryParse(reader["SvcId"].ToString(), out svcid);
                    expenses.SvcId = svcid;
                    expenses.SvcType = reader["SvcType"].ToString();
                   
                }
                return expenses;
            }
        }

        public List<Expenses> FindAll()
        {
            List<Expenses> Expenses = new List<Expenses>();
      
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `Expenses`;", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Expenses expenses = new Expenses();
                    expenses.Id = Convert.ToInt32(reader["Id"].ToString());
                    expenses.Type = reader["Type"].ToString();
                    expenses.Total = Convert.ToDecimal(reader["Total"].ToString());
                    int svcid;
                    Int32.TryParse(reader["SvcId"].ToString(), out svcid);
                    expenses.SvcId = svcid;
                    expenses.SvcType = reader["SvcType"].ToString();
                    Expenses.Add(expenses);
                }
                return Expenses;
            }
        }

        public List<ExpenseType> FindAllExpenseTypes()
        {
            List<ExpenseType> expenseTypes = new List<ExpenseType>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `ExpenseType`;", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ExpenseType expenseType = new ExpenseType();
                    expenseType.Id = Convert.ToInt32(reader["Id"].ToString());
                    expenseType.Name = reader["Name"].ToString();
                    expenseType.Description = reader["Description"].ToString();
                    expenseTypes.Add(expenseType);
                }
                return expenseTypes;
            }
        }


        public async Task UpdateAsync(Expenses expenses)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(@"UPDATE `Expenses` SET `Type` = @Type, `Total` = @Total, `SvcId`= @SvcId, `SvcType` = @SvcType WHERE `Id` = @Id", conn);
                dataAdapter.UpdateCommand = cmd;
                BindParams(cmd, expenses);
                BindId(cmd, expenses.Id);
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

        private void BindId(MySqlCommand cmd, int id)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.Int32,
                Value = id,
            });
        }

        private void BindParams(MySqlCommand cmd, Expenses expenses)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Type",
                DbType = DbType.String,
                Value = expenses.Type,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Total",
                DbType = DbType.String,
                Value = expenses.Total,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@SvcId",
                DbType = DbType.String,
                Value = expenses.SvcId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@SvcType",
                DbType = DbType.String,
                Value = expenses.SvcType,
            });

        }

        private void BindExpenseTypeParams(MySqlCommand cmd, ExpenseType expenseType)
        {

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Name",
                DbType = DbType.String,
                Value = expenseType.Name,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Description",
                DbType = DbType.String,
                Value = expenseType.Description,
            });
        }
    }
}
