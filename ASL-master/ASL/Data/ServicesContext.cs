using ASL.Models;
using ASL.ViewModels;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace ASL.Data
{
    public class ServicesContext
    {


        public string ConnectionString { get; set; }

        public ServicesContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public async Task InsertServiceAsync(Service Services)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"INSERT INTO Service ( CustId, SvcType, Address1, BillSubtotal, SvcDescription, Sched_Date, comp_date )VALUES ( @CustId, @SvcType, @Address1, @BillSubtotal, @SvcDescription, @Sched_Date, @comp_date);", conn);
                BindParams(cmd, Services);
                await cmd.ExecuteNonQueryAsync();
            }
        }


        public async Task SetCompletionDate(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"update service set comp_date = curdate()
                where id = @Id", conn);
                BindId(cmd, id);
                await cmd.ExecuteNonQueryAsync();
            }
        }


        public List<Service> FindAll()
        {
            List<Service> services = new List<Service>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"SELECT * FROM `Service`;", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Service service = new Service();
                   
                    service.Address1 = reader["Address1"].ToString();
                    Decimal.TryParse(reader["BillSubtotal"].ToString(), out decimal billSubtotal);
                    service.BillSubtotal = billSubtotal;

                    service.Id = Convert.ToInt32(reader["Id"].ToString());
                    Int32.TryParse(reader["Cust_Id"].ToString(), out int custid);
                    service.Cust_Id = custid;

                    service.Address2 = reader["Address2"].ToString();
                    service.City = reader["City"].ToString();
                    service.State = reader["State"].ToString();
                    service.Zip = reader["Zip"].ToString();
                    DateTime.TryParse(reader["Sched_Date"].ToString(), out DateTime sched_date);
                    service.Sched_Date = sched_date;

                    service.SvcType = reader["SvcType"].ToString();
                    service.SvcDescription = reader["SvcDescription"].ToString();

                    DateTime.TryParse(reader["comp_date"].ToString(), out DateTime Comp_Date);
                    service.comp_date = Comp_Date;
                    services.Add(service);

                }
                return services;
            }
        }

        public List<IndexServiceViewModel> CopyDaily()
        {
            List<IndexServiceViewModel> services = new List<IndexServiceViewModel>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(@"select service.*, customer.Address1, Name, customer.Id as CustId, Phone_Number from service join customer on customer.Id = service.CustId ; ", conn);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    IndexServiceViewModel service = new IndexServiceViewModel();

                    service.ServiceInfo.Address1 = reader["Address1"].ToString();
                    Decimal.TryParse(reader["BillSubtotal"].ToString(), out decimal billSubtotal);
                    service.ServiceInfo.BillSubtotal = billSubtotal;

                    service.ServiceInfo.Id = Convert.ToInt32(reader["Id"].ToString());
                    Int32.TryParse(reader["CustId"].ToString(), out int custid);
                    service.ServiceInfo.Cust_Id = custid;

                    service.CustInfo.Name = reader["Name"].ToString();

                    DateTime.TryParse(reader["Sched_Date"].ToString(), out DateTime sched_date);
                    service.ServiceInfo.Sched_Date = sched_date;

                    service.ServiceInfo.SvcType = reader["SvcType"].ToString();
                    service.ServiceInfo.SvcDescription = reader["SvcDescription"].ToString();

                    DateTime.TryParse(reader["comp_date"].ToString(), out DateTime Comp_Date);
                    service.ServiceInfo.comp_date = Comp_Date;
                    services.Add(service);

                }
                return services;
            }
        }


        public Service FindOneAsync(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {

                MySqlCommand cmd = new MySqlCommand(@"SELECT `Id`, `CustId`, `BIllSubtotal`, `Sched_Date`, `comp_date`, `Address1`, `Address2`,`City`, `State`, `Zip`, SvcType, SvcDescription FROM `Service` WHERE `Id` = @Id;", conn);
                BindId(cmd, id);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                Service service = new Service();

                while (reader.Read())
                {

                    service.Id = Convert.ToInt32(reader["Id"].ToString());
                    Int32.TryParse(reader["CustId"].ToString(), out int custid);
                    service.Cust_Id = custid;

                    Decimal.TryParse(reader["BillSubtotal"].ToString(), out decimal subtotal);
                    service.BillSubtotal = subtotal;

                    DateTime.TryParse(reader["Sched_Date"].ToString(), out DateTime scheduled);
                    service.Sched_Date = scheduled;

                    DateTime.TryParse(reader["comp_date"].ToString(), out DateTime compdate);
                    service.comp_date = compdate;

                    service.Address2 = reader["Address2"].ToString();
                    service.City = reader["City"].ToString();
                    service.State = reader["State"].ToString();
                    service.Zip = reader["Zip"].ToString();
                    service.SvcType = reader["SvcType"].ToString();
                    service.SvcDescription = reader["SvcDescription"].ToString();
                }

                return service;
            }
        }

        public string FindCustName(int id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                string name="";
                MySqlCommand cmd = new MySqlCommand(@"SELECT  Customer.name FROM Customer join service on customer.Id = service.CustId where service.id = @Id;", conn);
                BindId(cmd, id);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    name = reader["Name"].ToString();
                }

                return name;
            }
        }

       


        public Service FindOneAsyncByName(int id, ref string name)
        {
            using (MySqlConnection conn = GetConnection())
            {

                MySqlCommand cmd = new MySqlCommand(@"SELECT service.Id, Customer.name, CustId, BIllSubtotal, Sched_Date, comp_date, service.Address1,service.City, service.State, service.Zip, SvcType, SvcDescription FROM service Join Customer on Customer.Id = service.CustId where service.Id = @Id;", conn);
                BindId(cmd, id);
                conn.Open();
                MySqlDataReader reader = cmd.ExecuteReader();
                Service service = new Service();

                while (reader.Read())
                {

                    service.Id = Convert.ToInt32(reader["Id"].ToString());
                    Int32.TryParse(reader["CustId"].ToString(), out int custid);
                    service.Cust_Id = custid;

                    Decimal.TryParse(reader["BillSubtotal"].ToString(), out decimal subtotal);
                    service.BillSubtotal = subtotal;

                    DateTime.TryParse(reader["Sched_Date"].ToString(), out DateTime scheduled);
                    service.Sched_Date = scheduled;

                    DateTime.TryParse(reader["comp_date"].ToString(), out DateTime compdate);
                    service.comp_date = compdate;

                    service.Address1 = reader["Address1"].ToString();
                    service.City = reader["City"].ToString();
                    service.State = reader["State"].ToString();
                    service.Zip = reader["Zip"].ToString();
                    service.SvcType = reader["SvcType"].ToString();
                    service.SvcDescription = reader["SvcDescription"].ToString();
                    name = reader["Name"].ToString();
                }

                return service;
            }
        }





        public async Task UpdateAsync(Service Service)
        {
            using (MySqlConnection conn = GetConnection())
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(@"UPDATE `Service` SET `Id` = @Id, `SvcType` = @SvcType, `Address1` = @Address1, `Address2` = @Address2, `City` = @City, `State` = @State, `Zip` = @Zip, `SvcDescription` = @SvcDescription, `Sched_Date` = @Sched_Date, `comp_date` = @comp_date, `BillSubtotal` = @BillSubtotal   WHERE `Id` = @id", conn);
                dataAdapter.UpdateCommand = cmd;
                BindParams(cmd, Service);
                BindId(cmd, Service.Id);
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
                MySqlCommand cmd = new MySqlCommand(@"DELETE FROM Service WHERE `Id` = @Id", conn);
                dataAdapter.UpdateCommand = cmd;
                BindId(dataAdapter.UpdateCommand, Id);
                conn.Open();
                dataAdapter.UpdateCommand.ExecuteNonQuery();
            }

        }


        public void Details(int Id)
        {
            using (MySqlConnection conn = GetConnection())
            {
                //get data for data adapter
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                MySqlCommand cmd = new MySqlCommand(@"DETAILS FROM Service WHERE `Id` = @Id", conn);
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

        private void BindParams(MySqlCommand cmd, Service Services)
        {




            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@CustId",
                DbType = DbType.String,
                Value = Services.Cust_Id,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Address1",
                DbType = DbType.String,
                Value = Services.Address1,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@BillSubtotal",
                DbType = DbType.String,
                Value = Services.BillSubtotal,
            });
           
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Address2",
                DbType = DbType.String,
                Value = Services.Address2,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@City",
                DbType = DbType.String,
                Value = Services.City,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@State",
                DbType = DbType.String,
                Value = Services.State,
            });
            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Zip",
                DbType = DbType.String,
                Value = Services.Zip,
            });

            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@Sched_Date",
                DbType = DbType.String,
                Value = Services.Sched_Date.ToString("yyyyMMdd"),
            });


            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@comp_date",
                DbType = DbType.String,
                Value = Services.comp_date.ToString("yyyyMMdd"),
            });



            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@SvcType",
                DbType = DbType.String,
                Value = Services.SvcType,
            });


            cmd.Parameters.Add(new MySqlParameter
            {
                ParameterName = "@SvcDescription",
                DbType = DbType.String,
                Value = Services.SvcDescription,
            });



        }
    }
}

