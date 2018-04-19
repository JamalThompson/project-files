using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using ASL.Models;
using MySql.Data.MySqlClient;

namespace ASL.Data
{
    public class InvoiceContext
    {
        public string ConnectionString { get; set; }

        public InvoiceContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public async Task InsertInvoiceAsync(Invoice invoice)
        {
            using (MySqlConnection conn = GetConnection())
            {
                
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO invoicehdr (InvNum, CustId,Subtotal) VALUES (@InvNum, @CustId,@Subtotal);", conn);
                BindInsertParams(cmd, invoice);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }
        }

        public async Task UpdateInvoiceAsync(Invoice invoice)
        {
            using (MySqlConnection conn = GetConnection())
            {
                
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(@"Update invoicehdr Set InvNum=@InvNum, CustId=@CustId, Subtotal=@Subtotal) WHERE Id=@Id", conn);
                dataAdapter.UpdateCommand = cmd;
                BindInsertParams(cmd, invoice);
                BindId(cmd, invoice.Id);
                conn.Open();
                await dataAdapter.UpdateCommand.ExecuteNonQueryAsync();
            }
        }

        public async Task InsertInvoiceLineItemAsync(InvoiceLineItem invoiceLineItem)
        {
            using (MySqlConnection conn = GetConnection())
            {
                
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO invoiceli (InvNum, CustId,Total,SvcId) VALUES (@InvNum, @CustId,@Total,@SvcId);", conn);
                BindLIParams(cmd, invoiceLineItem);
                conn.Open();
                await cmd.ExecuteNonQueryAsync();
            }

        }


        public List<Invoice> FindAll()
        {
            List<Invoice> invoices = new List<Invoice>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT invoicehdr.*, customer.Name FROM `Invoicehdr` join customer on customer.id=invoicehdr.custid", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Invoice invoice = new Invoice();
                    int custId;
                    int.TryParse(reader["InvNum"].ToString(), out custId);
                    invoice.InvNum = custId;
                    invoice.Customer = reader["Name"].ToString();
                    int invNum;
                    int.TryParse(reader["InvNum"].ToString(), out invNum);
                    invoice.InvNum = invNum;
                    decimal subtotal;
                    Decimal.TryParse(reader["Subtotal"].ToString(), out subtotal);
                    invoice.Subtotal = subtotal;
                    decimal total;
                    Decimal.TryParse(reader["Total"].ToString(), out total);
                    invoice.Total = total;
                    int paid;
                    Int32.TryParse(reader["Paid"].ToString(), out paid);
                    invoice.Paid = paid;
                    invoices.Add(invoice);
                }
                return invoices;
            }
        }

        public void DeleteInvoice(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                //get data for data adapter
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(@"update `Invoice` set `IsActive` = false WHERE `Id` = @Id", conn);
                dataAdapter.UpdateCommand = cmd;
                BindId(dataAdapter.UpdateCommand, Id);
                conn.Open();
                dataAdapter.UpdateCommand.ExecuteNonQuery();
            }

        }

        private void BindLIParams(MySqlCommand cmd, InvoiceLineItem invoiceLineItem)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@InvNum",
                DbType = DbType.String,
                Value = invoiceLineItem.InvNum,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@CustId",
                DbType = DbType.String,
                Value = invoiceLineItem.CustId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Total",
                DbType = DbType.String,
                Value = invoiceLineItem.Total,
            });
        }

        private void BindId(MySqlCommand cmd, int id)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Id",
                DbType = DbType.String,
                Value = id,
            });

        }

        private void BindInsertParams(MySqlCommand cmd, Invoice invoice)
        {
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@InvNum",
                DbType = DbType.String,
                Value = invoice.InvNum,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@CustId",
                DbType = DbType.String,
                Value = invoice.CustId,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Subtotal",
                DbType = DbType.String,
                Value = invoice.Subtotal,
            });
        }

    }
}
