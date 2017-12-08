using System;
using MySql.Data.MySqlClient;
namespace user_eb.Models
{
    public class data
    {
        static string conn = "SERVER=localhost;UID=root;PASSWORD=root;DATABASE=UberEats;";


        public string RegisterCustomer(cus c)
        {
            string bt = "";

            using (MySqlConnection connect = new MySqlConnection())
            {
                connect.ConnectionString = conn;
                string query = "INSERT INTO UberEats.Customers(firstName,lastName,Mobile,Email,password) " +
                    "VALUES('" + c.firstName + "','" + c.lastName + "','" + c.Mobile + "','" + c.Email + "','" + c.password + "');";

                using (MySqlCommand comm = new MySqlCommand())
                {
                    try
                    {
                        comm.Connection.Open();

                        comm.Parameters.AddWithValue("@fisrtName", c.firstName);
                        comm.Parameters.AddWithValue("@lastName", c.lastName);
                        comm.Parameters.AddWithValue("@Mobile", c.Mobile);
                        comm.Parameters.AddWithValue("@Email", c.Email);
                        comm.Parameters.AddWithValue("@password", c.password);
                        int qr = comm.ExecuteNonQuery();

                        bt = qr.ToString();
                    }
                    catch (Exception ex)
                    {
                        ex.ToString();
                        comm.Connection.Close();

                    }
                }
                return null;
            }
        }
    }
}
